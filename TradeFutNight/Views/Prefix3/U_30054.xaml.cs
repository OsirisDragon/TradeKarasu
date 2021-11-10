using ChangeTracking;
using CrossModel;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30054.xaml 的互動邏輯
    /// </summary>
    public partial class U_30054 : UserControlParent, IViewSword
    {
        private U_30054_ViewModel _vm;

        public U_30054()
        {
            InitializeComponent();
            _vm = (U_30054_ViewModel)DataContext;
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

        public async Task Open()
        {
            var task = Task.Run(() =>
            {
                _vm.Open();
                DbLog(MessageConst.Open);
                Dispatcher.Invoke(() =>
                {
                    Insert();
                });
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
            bool isNeedConfirm = false;
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

                if (trackableData.AddedItems.Count() == 0)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NoAddedData);
                    return false;
                }

                List<PDK> pdkNotStock = null;
                List<PDK> pdkStock = null;
                using (var das = Factory.CreateDalSession())
                {
                    var dPdk = new D_PDK(das);
                    // 非股票類
                    pdkNotStock = dPdk.ListDistinctKindIdNotStock().ToList();
                    // 股票關聯KEY值
                    pdkStock = dPdk.ListDistinctParamKeyStock().ToList();
                }

                foreach (var item in trackableData.AddedItems)
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (item.MORD_KIND_ID_TYPE == 'K')
                        {
                            var count = pdkNotStock.Where(x => x.PDK_KIND_ID == item.MORD_KIND_ID).Count();
                            if (count == 0)
                            {
                                resultItem.AppendErrorMessage($"{item.MORD_KIND_ID}不是非股票類的代碼");
                            }
                        }
                        else if (item.MORD_KIND_ID_TYPE == 'P')
                        {
                            var count = pdkStock.Where(x => x.PDK_PARAM_KEY == item.MORD_KIND_ID).Count();
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
                var domainData = _vm.MapperInstance.Map<IList<MORD>>(trackableData.AddedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dMORD = new D_MORD(das);
                        dMORD.Insert(domainData);

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

                var report = CreateReport(domainData);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(ExportFilePath);
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();

                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });

            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data)
        {
            string memo = "";

            var rptSetting = ReportNormal.CreateSetting(ProgramID, ProgramID + "–" + ProgramName, UserName, memo, Ocf.OCF_DATE, true, false, true);
            var reportCommon = ReportNormal.CreateCommonPortrait(data, gridMain.Columns, rptSetting);

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
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
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