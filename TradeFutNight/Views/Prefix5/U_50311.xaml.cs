using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;

namespace TradeFutNight.Views.Prefix5
{
    /// <summary>
    /// U_50311.xaml 的互動邏輯
    /// </summary>
    public partial class U_50311 : UserControlParent, IViewSword
    {
        private U_50311_ViewModel _vm;

        public U_50311()
        {
            InitializeComponent();
            _vm = (U_50311_ViewModel)DataContext;
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
            VmMainUi.IsButtonPrintEnabled = true;
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
            if (_vm.MainGridData.Count == 0)
            {
                MessageBoxExService.Instance().Info("尚無此交易資料");
            }
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
            ReportSetting rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);

            U_50311_Report report = new U_50311_Report
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
            var report = CreateReport(_vm.MainGridData, OperationType.Print);
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());
            await reportGate.Print();

            VmMainUi.HideLoadingWindow();
            MessageBoxExService.Instance().Info(MessageConst.PrintSuccess);
        }

        public async Task PrintIndex()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintStock()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }
    }
}