using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.Xpf.Editors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using CrossModel.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeUtility;

namespace TradeFutNight.Views.Prefix5
{
    /// <summary>
    /// U_50333.xaml 的互動邏輯
    /// </summary>
    public partial class U_50333 : UserControlParent, IViewSword
    {
        private U_50333_ViewModel _vm;

        public U_50333()
        {
            InitializeComponent();
            _vm = (U_50333_ViewModel)DataContext;
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
            VmMainUi.IsButtonPrintIndexEnabled = true;
        }

        public async Task Open()
        {
            ToolButtonSetting();
            _vm.MarketClose = DropDownItems.MarketClose();

            var task = Task.Run(() =>
            {
                _vm.Open();
                DbLog(MessageConst.Open);
            });
            await task;

            cbMarketClose.SelectedIndex = 0;
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
            var task = Task.Run(() =>
           {
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

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, false, false, false);
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
            MessageBoxExService.Instance().Info(MessageConst.PrintSuccess);
        }

        public async Task PrintIndex()
        {
            gridView.CloseEditor();
            var printData = _vm.MainGridData.Where(x => x.PDK_SUBTYPE != "S").ToList();
            XtraReport report = CreateReport(printData, OperationType.PrintIndex);

            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());
            MessageBoxExService.Instance().Info(MessageConst.PrintSuccess);
        }

        public async Task PrintStock()
        {
            gridView.CloseEditor();
            var printData = _vm.MainGridData.Where(x => x.PDK_SUBTYPE == "S").ToList();
            XtraReport report = CreateReport(printData, OperationType.PrintIndex);

            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());
            MessageBoxExService.Instance().Info(MessageConst.PrintSuccess);
        }

        private async void CbMarketClose_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            var cbEditor = (ComboBoxEdit)sender;
            var grpSelectItem = cbEditor.EditValue.AsInt();
            var currOpenSw = 0;

            await _vm.Query();

            if (grpSelectItem != 0)
            {
                using (var das = Factory.CreateDalSession())
                {
                    currOpenSw = new D_OSWCUR(das).GetCurrOpenSwByGrp(grpSelectItem);
                }

                if (currOpenSw < 110 && currOpenSw > 0)
                {
                    MessageBoxExService.Instance().Error($"群組{grpSelectItem}尚未收盤");
                }
                _vm.MainGridData = _vm.MainGridData.Where(x => x.PDK_MARKET_CLOSE == grpSelectItem.ToString()).ToList();
            }
            MessageBoxExService.Instance().Info("資料查詢完成");
        }
    }
}