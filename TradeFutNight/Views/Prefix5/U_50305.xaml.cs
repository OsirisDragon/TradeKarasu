using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;

namespace TradeFutNight.Views.Prefix5
{
    /// <summary>
    /// U_50305.xaml 的互動邏輯
    /// </summary>
    public partial class U_50305 : UserControlParent, IViewSword
    {
        private U_50305_ViewModel _vm;

        public U_50305()
        {
            InitializeComponent();
            _vm = (U_50305_ViewModel)DataContext;
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
            await reportGate.ExportPdf(GetExportFilePath());
            await reportGate.Print();

            gridView.ExportToCsv(GetExportFilePath(FileType.Csv));

            MessageBoxExService.Instance().Info(MessageConst.PrintSuccess);
        }

        public async Task PrintIndex()
        {
            gridView.CloseEditor();
            var printData = _vm.MainGridData.Where(x => x.PDK_SUBTYPE != "S").ToList();
            XtraReport report = CreateReport(printData, OperationType.PrintIndex);

            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());
            await reportGate.Print();
            MessageBoxExService.Instance().Info(MessageConst.PrintSuccess);
        }

        public async Task PrintStock()
        {
            gridView.CloseEditor();
            var printData = _vm.MainGridData.Where(x => x.PDK_SUBTYPE == "S").ToList();
            XtraReport report = CreateReport(printData, OperationType.PrintIndex);

            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());
            await reportGate.Print();
            MessageBoxExService.Instance().Info(MessageConst.PrintSuccess);
        }
    }
}