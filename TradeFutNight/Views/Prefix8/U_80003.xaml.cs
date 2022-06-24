using CrossModel;
using CrossModel.Enum;
using DevExpress.Xpf.DocumentViewer;
using DevExpress.Xpf.Printing;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;

namespace TradeFutNight.Views.Prefix8
{
    /// <summary>
    /// U_80003.xaml 的互動邏輯
    /// </summary>
    public partial class U_80003 : UserControlParent, IViewSword
    {
        private U_80003_ViewModel _vm;

        public U_80003()
        {
            InitializeComponent();
            _vm = (U_80003_ViewModel)DataContext;
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

            var report = (U_80003_Report)CreateReport(_vm.MainGridData, OperationType.Query);

            EditingFieldExtensions.Instance.RegisterEditorInfo("ComboBoxEditor", "Custom", "ComboBox Editor");
            report.xrLabel1.EditOptions.Enabled = true;
            report.xrLabel1.EditOptions.EditorName = "ComboBoxEditor";

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

            U_80003_Report report = new U_80003_Report
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
            CloseEditor(docPreviewControl);

            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintStock()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            //var data = _vm.ListForGenerateFile();
            //ExportElf.ToTxt(data, Path.Combine(AppSettings.LocalRoutineDataDirectory, "80003.txt"), false);

            MessageBoxExService.Instance().Info("下載完成");
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var paramsText = new TextSearchParameter() { Text = txtProdId.Text };

            if (docPreviewControl.FindTextCommand.CanExecute(paramsText))
                docPreviewControl.FindTextCommand.Execute(paramsText);
        }
    }
}