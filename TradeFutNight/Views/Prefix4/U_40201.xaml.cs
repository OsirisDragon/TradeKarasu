using ChangeTracking;
using CrossModel;
using CrossModel.Interfaces;
using DevExpress.XtraReports.UI;
using Eagle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix4
{
    /// <summary>
    /// U_40201.xaml 的互動邏輯
    /// </summary>
    public partial class U_40201 : UserControlParent, IViewSword
    {
        private U_40201_ViewModel _vm;

        public U_40201()
        {
            InitializeComponent();
            _vm = (U_40201_ViewModel)DataContext;
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
            base.Delete(gridMain, _vm, false);
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

                foreach (var item in trackableData.AddedItems)
                {
                    Dispatcher.Invoke(() =>
                    {
                        item.PHALT_TRADE_DATE = Ocf.OCF_DATE;
                        item.PHALT_TYPE = 'T';
                        item.PHALT_TRADE_PAUSE_DATE = DateTime.Today;
                        item.PHALT_TRADE_PAUSE_TIME = DateTime.Now.ToString("HHmmss");
                        item.PHALT_STOCK_ID = item.PHALT_PROD_ID;
                        item.PHALT_USER_ID = UserID;
                        item.PHALT_W_TIME = DateTime.Now;
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
                var domainData = _vm.MapperInstance.Map<IList<PHALT>>(trackableData.AddedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dPhalt = new D_PHALT(das);
                        dPhalt.Insert(domainData);

                        //每一筆都要發送mex訊息
                        IEagleGate eagleGate = new MexGate(MsgSysType.FutNight, "TFX.FUT.PROD.PDKBREAK.OP", "all");
                        foreach (var item in trackableData.AddedItems)
                        {
                            EagleArgs ea = new EagleArgs();

                            ea.AddEagleContent(new EagleContent() { Item = "TYPE", Value = "SUR_STKCOD" });
                            ea.AddEagleContent(new EagleContent() { Item = "PROD_ID", Value = item.PHALT_PROD_ID });
                            ea.AddEagleContent(new EagleContent() { Item = "MSG_TYPE", Value = "5" });
                            ea.AddEagleContent(new EagleContent() { Item = "STATUS", Value = item.PHALT_MSG_TYPE });
                            ea.AddEagleContent(new EagleContent() { Item = "TRADE_DATE", Value = "X" });
                            ea.AddEagleContent(new EagleContent() { Item = "TRADE_PAUSE_TIME", Value = "X" });

                            ea.AddEagleContent(new EagleContent() { Item = "TRADE_RESUME_DATE", Value = "1" });
                            ea.AddEagleContent(new EagleContent() { Item = "TRADE_RESUME_TIME", Value = "SUR" });
                            ea.AddEagleContent(new EagleContent() { Item = "ORDER_RESUME_TIME", Value = "X" });
                            ea.AddEagleContent(new EagleContent() { Item = "MATCH_RESUME_TIME", Value = "X" });

                            eagleGate.AddArgument(ea);

                            eagleGate.Send();
                        }
                        DbLog(MessageConst.SendMsg + ":" + eagleGate.Subject);

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
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();

                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess + "\n請注意，本功能係日夜盤分開設定，各自獨立!");
                CloseWindow();
            });

            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data)
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