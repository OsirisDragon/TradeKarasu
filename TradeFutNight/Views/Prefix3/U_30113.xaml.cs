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

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30113.xaml 的互動邏輯
    /// </summary>
    public partial class U_30113 : UserControlParent, IViewSword
    {
        private U_30113_ViewModel _vm;

        public U_30113()
        {
            InitializeComponent();
            _vm = (U_30113_ViewModel)DataContext;
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
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = true }, gridMain, _vm))
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
                var checkItems = trackableData.ChangedItems;

                if (checkItems.Count() == 0)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NoChangedData);
                    return false;
                }

                foreach (var item in checkItems)
                {
                    if (item.SLT_MAX < item.SLT_MIN)
                    {
                        resultItem.AppendErrorMessage($"{item.SLT_KIND_ID}的權利金上限不能小於權利金下限");
                    }

                    using (var das = Factory.CreateDalSession())
                    {
                        var dPDK = new D_PDK(das);
                        var pdk = dPDK.getByParamKey(item.SLT_KIND_ID);
                        if (pdk != null && pdk.PDK_SUBTYPE == 'E')
                        {
                            if (pdk.PDK_PRICE_FLUC != 'F')
                            {
                                resultItem.AppendErrorMessage("匯率類的商品僅可選擇「固定點數」");
                            }
                        }
                    }
                }

                if (resultItem.HasError)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(resultItem.ErrorMessage);
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
                        var dSlt = new D_SLT(das);
                        dSlt.Update(domainData);

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
                await reportGate.ExportPdf(ExportFilePath);
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private IList<SLT> CustomMapper(IEnumerable<UIModel_30113> items)
        {
            var listResult = new List<SLT>();
            foreach (var item in items)
            {
                var newItem = _vm.MapperInstance.Map<SLT>(item);

                var trackItem = item.CastToIChangeTrackable();
                newItem.OriginalData = trackItem.GetOriginal();
                listResult.Add(newItem);
            }

            return listResult;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string memo = "";
            Dispatcher.Invoke(() =>
            {
                memo = txtMemo.Text;
            });

            string reportTitle = ProgramID + "–" + ProgramName;

            switch (operationType)
            {
                case OperationType.Save:

                    break;

                case OperationType.Print:

                    break;

                default:
                    break;
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, memo, Ocf.OCF_DATE, true, false, true);
            var reportCommon = ReportNormal.CreateCommonLandscape(data, gridMain, rptSetting);

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

        private async void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            button.IsEnabled = false;

            if (cbKindId.SelectedItem != null)
            {
                var selectedItem = (ItemInfo)cbKindId.SelectedItem;

                var task = Task.Run(async () =>
                {
                    await _vm.Query(selectedItem.Value.ToString());
                });

                await task;
            }

            button.IsEnabled = true;
        }
    }
}