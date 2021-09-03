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
    /// U_30111.xaml 的互動邏輯
    /// </summary>
    public partial class U_30111 : UserControlParent, IViewSword
    {
        private U_30111_ViewModel _vm;

        public U_30111()
        {
            InitializeComponent();
            _vm = (U_30111_ViewModel)DataContext;
        }

        public void InitialSetting(string programID, string programName, MainUI_ViewModel vmMainUi, MainUI mainUi)
        {
            base.Init(programID, programName, vmMainUi, mainUi);
        }

        public async Task<bool> IsCanRun()
        {
            var task = Task.Run(() =>
            {
                var isCanRun = IsCanRunProgram();
                MagicalHats.LogToDb(UserID, ProgramID, MessageConst.IsCanRun + ":" + isCanRun.ToString().ToUpper());
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
                MagicalHats.LogToDb(UserID, ProgramID, MessageConst.Open);
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
            VmMainUi.LoadingText = MessageConst.LoadingStatusChecking;

            gridView.CloseEditor();

            if (!CheckNotNullNotEmpty(gridMain, _vm))
                return false;

            var task = Task.Run(() =>
            {
                var resultItem = new ResultItem();

                if (!IsCanRunProgram())
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
                    return false;
                }

                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();

                if (trackableData.AddedItems.Count() == 0)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NoAddedData);
                    return false;
                }

                foreach (var item in trackableData.AddedItems)
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
                var domainData = _vm.MapperInstance.Map<IList<SLT>>(trackableData.AddedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dSlt = new D_SLT(das);
                        dSlt.Insert(domainData);

                        UpdateAccessPermission(ProgramID, das);

                        DbLog(ProgramID, UserID, MessageConst.Completed, das);

                        das.Commit();
                    }
                    catch (Exception ex)
                    {
                        das.Rollback();
                        throw ex;
                    }
                }

                var report = CreateReport(domainData);
                var reportGate = await new ReportGate(report).CreateDocument();
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
            Dispatcher.Invoke(() =>
            {
                memo = txtMemo.Text;
            });
            var rptSetting = ReportNormal.CreateSetting(ProgramID, ProgramID + "–" + ProgramName, UserName, memo, Ocf.OCF_DATE, true, false, true);
            var reportCommon = ReportNormal.CreateCommonLandscape(data, gridMain.Columns, rptSetting);

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