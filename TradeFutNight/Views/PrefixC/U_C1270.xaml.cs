using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.PrefixC
{
    /// <summary>
    /// U_C1270.xaml 的互動邏輯
    /// </summary>
    public partial class U_C1270 : UserControlParent, IViewSword
    {
        private U_C1270_ViewModel _vm;

        private bool _canSave = false;

        public U_C1270()
        {
            InitializeComponent();
            _vm = (U_C1270_ViewModel)DataContext;
        }

        public async Task<bool> IsCanRun()
        {
            var task = Task.Run(() =>
            {
                var isCanRun = IsCanRunProgram();
                DbLog(MessageConst.IsCanRun + ":" + isCanRun.ToString().ToUpper());
                return isCanRun;
            });
            await task;

            return task.Result;
        }

        public override void ControlSetting()
        {
            base.ControlSetting();
            VmMainUi.IsButtonPrintEnabled = true;
        }

        public async Task Open()
        {
            ControlSetting();

            var task = Task.Run(() =>
            {
                _vm.Open();
                DbLog(MessageConst.Open);
            });
            await task;
        }

        public void Insert()
        {
            gridView.CloseEditor();
            _vm.Insert();
        }

        public void Delete()
        {
            bool isNeedConfirm = true;
            var selectedItem = gridMain.SelectedItem;
            if (selectedItem != null)
            {
                if (isNeedConfirm)
                {
                    if (MessageBoxExService.Instance().Confirm(MessageConst.ConfirmDelete) == MessageBoxResult.Yes)
                        _vm.Delete(selectedItem);
                }
                else
                {
                    _vm.Delete(selectedItem);
                }
            }
        }

        public async Task<bool> CheckField()
        {
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = false }, gridMain, _vm))
                return false;

            //if (!_canSave)
            //{
            //    MessageBoxExService.Instance().Error("此盤已開盤無法儲存");
            //    return false;
            //}

            var task = Task.Run(() =>
            {
                if (!IsCanRunProgram())
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
                    return false;
                }

                return true;
            });
            await task;

            return task.Result;
        }

        public async Task Save()
        {
            VmMainUi.LoadingText = MessageConst.LoadingStatusSaving;

            var task = Task.Run(async () =>
            {
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();
                var domainData = CustomMapper(trackableData.ChangedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dTPPBP = new D_TPPBP(das);
                        dTPPBP.Update(domainData);

                        //更新交易系統狀態
                        UpdateAccessPermission(ProgramID, das);

                        var grpSelectItem = cbGrp.EditValue.AsInt();

                        var keyData = 0;

                        switch (grpSelectItem)
                        {
                            case 10:
                                keyData = 1;
                                break;

                            case 11:
                                keyData = 2;
                                break;
                        }

                        DbLog($"{MessageConst.Completed}_{keyData}", das);

                        das.Commit();
                    }
                    catch (Exception ex)
                    {
                        das.Rollback();
                        throw ex;
                    }
                }

                var report = CreateReport(domainData, OperationType.Save);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(ExportFilePath);
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private IList<TPPBP> CustomMapper(IEnumerable<UIModel_C1270> items)
        {
            var listResult = new List<TPPBP>();
            foreach (var item in items)
            {
                var newItem = _vm.MapperInstance.Map<TPPBP>(item);

                var trackItem = item.CastToIChangeTrackable();
                newItem.OriginalData = trackItem.GetOriginal();
                listResult.Add(newItem);
            }

            return listResult;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);
            var reportCommon = ReportNormal.CreateCommonPortrait(data, gridMain, rptSetting);

            return reportCommon;
        }

        public async Task Export()
        {
            gridView.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task Print()
        {
            gridView.CloseEditor();

            var report = CreateReport(_vm.MainGridData, OperationType.Print);
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(ExportFilePath);
            await reportGate.Print();
        }

        public async Task PrintIndex()
        {
            gridView.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintStock()
        {
            gridView.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        private async void CbGrp_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            VmMainUi.ShowLoadingWindow();

            var grpSelectItem = cbGrp.EditValue.AsInt();
            var currOpenSw = 0;
            using (var das = Factory.CreateDalSession())
            {
                currOpenSw = new D_OSWCUR(das).ListCurrOpenSW(grpSelectItem);
            }

            // 10是開始收單
            //_canSave = !(currOpenSw >= 10);
            //if (currOpenSw >= 10)
            //{
            //    MessageBoxExService.Instance().Error("此盤已經開盤，無法再設定");
            //    VmMainUi.HideLoadingWindow();
            //    return;
            //}

            await _vm.Query(grpSelectItem);

            VmMainUi.HideLoadingWindow();
        }
    }
}