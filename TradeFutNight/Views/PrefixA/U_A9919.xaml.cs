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
using TradeUtility;

namespace TradeFutNight.Views.PrefixA
{
    /// <summary>
    /// U_A9919.xaml 的互動邏輯
    /// </summary>
    public partial class U_A9919 : UserControlParent, IViewSword
    {
        private U_A9919_ViewModel _vm;

        public U_A9919()
        {
            InitializeComponent();
            _vm = (U_A9919_ViewModel)DataContext;
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
            VmMainUi.IsButtonSaveEnabled = false;
            VmMainUi.IsButtonDeleteEnabled = false;
            VmMainUi.IsButtonPrintEnabled = true;

            _vm.StartDate = _vm.EndDate = Ocf.OCF_DATE;
            _vm.ProdId = "";
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
            if (_vm.MainGridData.Count == 0)
            {
                MessageBoxExService.Instance().Info("本日無造市者盤中報價設定商品");
            }
        }

        public void Insert()
        {
        }

        public void Delete()
        {
        }

        public async Task<bool> CheckField()
        {
            var task = Task.Run(() => true);
            await task;

            return task.Result;
        }

        public async Task Save()
        {
            var task = Task.Run(() => { });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            switch (operationType)
            {
                case OperationType.PrintIndex:
                    reportTitle += "(指數類)";
                    break;

                case OperationType.PrintStock:
                    reportTitle += "(股票類)";
                    break;
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, "", Ocf.OCF_DATE, true, true, true);

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

            var report = CreateReport(_vm.MainGridData, OperationType.Print);
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(ExportFilePath);
            await reportGate.Print();
        }

        public async Task PrintIndex()
        {
            gridView.CloseEditor();

            var printData = _vm.MainGridData.Where(x => x.PDK_SUBTYPE == "I").ToList();
            XtraReport report = CreateReport(printData, OperationType.PrintIndex);

            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(ExportFilePath);
            await reportGate.Print();
        }

        public async Task PrintStock()
        {
            gridView.CloseEditor();

            var printData = _vm.MainGridData.Where(x => x.PDK_SUBTYPE == "S").ToList();
            var report = CreateReport(printData, OperationType.PrintStock);
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(ExportFilePath);
            await reportGate.Print();
        }

        private async void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            button.IsEnabled = false;

            DateTime startDate = txtStartDate.EditValue.AsDateTime();
            DateTime endDate = txtEndDate.EditValue.AsDateTime();

            await _vm.Query();

            if (_vm.MainGridData.Count == 0)
            {
                MessageBoxExService.Instance().Info("查無造市者盤中報價設定商品");
            }

            button.IsEnabled = true;
        }
    }
}