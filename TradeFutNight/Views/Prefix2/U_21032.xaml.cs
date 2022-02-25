using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeUtility;

namespace TradeFutNight.Views.Prefix2
{
    /// <summary>
    /// U_21032.xaml 的互動邏輯
    /// </summary>
    public partial class U_21032 : UserControlParent, IViewSword
    {
        private U_21032_ViewModel _vm;

        public U_21032()
        {
            InitializeComponent();
            _vm = (U_21032_ViewModel)DataContext;
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
            VmMainUi.IsButtonDeleteEnabled = false;
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

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);
            rptSetting.HeaderColumnsFontSize = 9;
            rptSetting.ContentColumnsFontSize = 8.5f;
            rptSetting.ContentColumnsWidthScaleFactor = 0.77f;
            rptSetting.HeaderColumnsWidthScaleFactor = 0.77f;
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

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            try
            {
                VmMainUi.ShowLoadingWindow();

                button.IsEnabled = false;

                var result = _vm.Query();
                string routinePath = Path.Combine(AppSettings.LocalRoutineDataDirectory, "21032.csv");
                result.ToCsv(routinePath, true);

                button.IsEnabled = true;
                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info($"存於{routinePath}");
            }
            catch (Exception ex)
            {
                button.IsEnabled = true;
                throw ex;
            }
        }
    }
}