using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30036.xaml 的互動邏輯
    /// </summary>
    public partial class U_30036 : UserControlParent, IViewSword
    {
        private U_30036_ViewModel _vm;

        public U_30036()
        {
            InitializeComponent();
            _vm = (U_30036_ViewModel)DataContext;
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

        public override void ToolButtonSetting()
        {
            base.ToolButtonSetting();
            VmMainUi.IsButtonSaveEnabled = false;
            VmMainUi.IsButtonDeleteEnabled = false;
        }

        public async Task Open()
        {
            ToolButtonSetting();

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

            var task = Task.Run(() =>
            {
                if (!IsCanRunProgram())
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
                    return false;
                }

                var resultItem = new ResultItem();
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();

                if (trackableData.DeletedItems.Count() == 0)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NoDeletedData);
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
                var domainData = _vm.MapperInstance.Map<IList<CCM>>(trackableData.DeletedItems);
                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dCCM = new D_CCM(das);
                        dCCM.Delete(domainData);

                        var dPCM = new D_PCM(das);

                        var dPDK = new D_PDK(das);
                        var pdkSubtypeData = dPDK.ListDistinctKindIdAndSubtypeForPcm();

                        foreach (var item in domainData)
                        {
                            var subtype = item.CCM_SUBTYPE;
                            foreach (var p in pdkSubtypeData)
                            {
                                if (p.PDK_SUBTYPE == subtype)
                                {
                                    dPCM.Delete(item.CCM_FCM_NO, p.PDK_KIND_ID);
                                }
                            }
                        }

                        UpdateAccessPermission(ProgramID, das);

                        DbLog(MessageConst.Completed, das);

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
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            switch (operationType)
            {
                case OperationType.Save:
                    reportTitle = reportTitle.Replace("查詢", "刪除");
                    break;

                default:
                    break;
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, "", Ocf.OCF_DATE, true, false, true);
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
            await reportGate.ExportPdf(GetExportFilePath());
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

        private async void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            button.IsEnabled = false;

            var cmNo = txtCmNo.EditValue.AsString();
            await _vm.Query();

            button.IsEnabled = true;
        }
    }
}