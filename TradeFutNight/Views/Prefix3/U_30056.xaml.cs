using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30056.xaml 的互動邏輯
    /// </summary>
    public partial class U_30056 : UserControlParent, IViewSword
    {
        private U_30056_ViewModel _vm;

        public U_30056()
        {
            InitializeComponent();
            _vm = (U_30056_ViewModel)DataContext;
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
            base.Delete(gridMain, _vm);
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

                IList<PDK> pdkNotStock = null;
                IList<PDK> pdkStock = null;
                using (var das = Factory.CreateDalSession())
                {
                    var dPdk = new D_PDK(das);
                    // 非股票類
                    pdkNotStock = dPdk.ListKindIdNotStock();
                    // 股票關聯KEY值
                    pdkStock = dPdk.ListDistinctParamKeyStock();
                }

                foreach (var item in trackableData.ChangedItems)
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (item.MORD_KIND_ID_TYPE == 'K')
                        {
                            var count = pdkNotStock.Count(x => x.PDK_KIND_ID == item.MORD_KIND_ID);
                            if (count == 0)
                            {
                                resultItem.AppendErrorMessage($"{item.MORD_KIND_ID}不是非股票類的代碼");
                            }
                        }
                        else if (item.MORD_KIND_ID_TYPE == 'P')
                        {
                            var count = pdkStock.Count(x => x.PDK_PARAM_KEY == item.MORD_KIND_ID);
                            if (count == 0)
                            {
                                resultItem.AppendErrorMessage($"{item.MORD_KIND_ID}不是股票關聯KEY值的代碼，期貨應是STF或ETF，選擇權應是STC或ETC");
                            }
                        }
                        item.MORD_USER_ID = UserID;
                        item.MORD_W_TIME = DateTime.Now;
                    });
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
                var domainData = CustomMapper<MORD>(trackableData.ChangedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dMORD = new D_MORD(das);
                        dMORD.Update(domainData);

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

        private IList<T> CustomMapper<T>(IEnumerable<UIModel_30056> items) where T : MORD
        {
            var listResult = new List<T>();

            Dispatcher.Invoke(() =>
            {
                foreach (var item in items)
                {
                    var newItem = _vm.MapperInstance.Map<T>(item);

                    var trackItem = item.CastToIChangeTrackable();
                    newItem.OriginalData = trackItem.GetOriginal();
                    listResult.Add(newItem);
                }
            });

            return listResult;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
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
    }
}