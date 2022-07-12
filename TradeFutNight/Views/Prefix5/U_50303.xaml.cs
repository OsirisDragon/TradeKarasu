using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using CrossModel.Interfaces;
using TradeFutNight.Reports;

namespace TradeFutNight.Views.Prefix5
{
    /// <summary>
    /// U_50303.xaml 的互動邏輯
    /// </summary>
    public partial class U_50303 : UserControlParent, IViewSword
    {
        private U_50303_ViewModel _vm;

        public U_50303()
        {
            InitializeComponent();
            _vm = (U_50303_ViewModel)DataContext;
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
            VmMainUi.IsButtonPrintStockEnabled = true;
            VmMainUi.IsButtonSaveEnabled = false;
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

            var report = CreateReport(_vm.MainGridData, OperationType.Query);

            _vm.Report = report;
        }

        public void Insert()
        {
        }

        public void Delete()
        {
        }

        public async Task<bool> CheckField()
        {
            var task = Task.Run(() =>
            {
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
            string reportTitle = ProgramID + AppSettings.DashForTitle + ProgramName;
            ReportSetting rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, false, false, false);

            U_50303_Report report = new U_50303_Report
            {
                DataSource = data,
                HasHandlePerson = rptSetting.HasHandlePerson,
                HasConfirmPerson = rptSetting.HasConfirmPerson,
                HasManagerPerson = rptSetting.HasManagerPerson,
                PageHeaderVisible = true,
                TableFooterVisible = true
            };

            switch (operationType)
            {
                case OperationType.Query:
                    report.HasHandlePerson = false;
                    report.HasConfirmPerson = false;
                    report.HasManagerPerson = false;
                    report.PageHeaderVisible = false;
                    report.TableFooterVisible = false;
                    report.HasGroupProdIdOut = false;
                    break;

                case OperationType.PrintIndex:
                    rptSetting.ReportTitle += AppSettings.DashForTitle + "指數類";
                    break;

                case OperationType.PrintStock:
                    rptSetting.ReportTitle += AppSettings.DashForTitle + "股票類";
                    break;

                default:
                    break;
            }

            ReportNormal.SetReportHeaderParameters(report, rptSetting);

            return report;
        }

        public async Task Export()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task Print()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintIndex()
        {
            var report = CreateReport(_vm.MainGridData.Where(c => c.PDK_SUBTYPE != "S").ToList(), OperationType.PrintIndex);
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());

            MessageBoxExService.Instance().Info("存檔完成，如欲印相關報表，請利用PDF檔列印");
        }

        public async Task PrintStock()
        {
            var report = CreateReport(_vm.MainGridData.Where(c => c.PDK_SUBTYPE == "S").ToList(), OperationType.PrintStock);
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());

            MessageBoxExService.Instance().Info("存檔完成，如欲印相關報表，請利用PDF檔列印");
        }
    }
}