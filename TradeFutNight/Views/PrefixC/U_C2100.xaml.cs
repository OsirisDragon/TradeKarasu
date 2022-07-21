using CrossModel;
using CrossModel.Enum;
using CrossModel.Interfaces;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility.File;

namespace TradeFutNight.Views.PrefixC
{
    /// <summary>
    /// U_C2100.xaml 的互動邏輯
    /// </summary>
    public partial class U_C2100 : UserControlParent, IViewSword
    {
        private U_C2100_ViewModel _vm;

        private bool _canSave = false;

        public U_C2100()
        {
            InitializeComponent();
            _vm = (U_C2100_ViewModel)DataContext;
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
            VmMainUi.IsButtonInsertEnabled = false;
            VmMainUi.IsButtonDeleteEnabled = false;
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
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = false }, gridMain, _vm))
                return false;

            if (!_canSave)
            {
                MessageBoxExService.Instance().Error("此盤已開盤無法儲存");
                return false;
            }

            var task = Task.Run(() =>
            {
                if (!IsCanRunProgram())
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
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
                var operate = GetChanges<UIModel_C2100, TPPBP>(_vm.MainGridData, _vm);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dTPPBP = new D_TPPBP(das);
                        dTPPBP.Update(operate.ChangedItems);

                        //更新交易系統狀態
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

                var report = CreateReport(operate.ChangedItems.ToList(), OperationType.Save);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);
            rptSetting.HeaderColumnsFontSize = 10;
            rptSetting.ContentColumnsFontSize = 10;
            rptSetting.ContentColumnsWidthScaleFactor = 0.94f;
            rptSetting.HeaderColumnsWidthScaleFactor = 0.94f;
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

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            var exportData = _vm.MainGridData.Select(c => new { c.TPPBP_PROD_ID, c.TPPBP_THERICAL_P_REF });
            string filePath = GetExportFilePath(FileType.Csv);
            ExportElf.ToCsv(exportData, filePath, false);
            MessageBoxExService.Instance().Info($"成功產生檔案至{filePath}");
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}