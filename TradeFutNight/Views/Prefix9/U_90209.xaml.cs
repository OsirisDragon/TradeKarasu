using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeUtility.File;

namespace TradeFutNight.Views.Prefix9
{
    /// <summary>
    /// U_90209.xaml 的互動邏輯
    /// </summary>
    public partial class U_90209 : UserControlParent, IViewSword
    {
        private U_90209_ViewModel _vm;

        public U_90209()
        {
            InitializeComponent();
            _vm = (U_90209_ViewModel)DataContext;
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
            VmMainUi.IsButtonInsertEnabled = false;
            VmMainUi.IsButtonSaveEnabled = false;
            VmMainUi.IsButtonDeleteEnabled = false;
            VmMainUi.IsButtonPrintEnabled = false;
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
            await _vm.Query();
            await Save();
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

                return true;
            });
            await task;

            return task.Result;
        }

        public async Task Save()
        {
            VmMainUi.LoadingText = MessageConst.LoadingStatusSaving;
            try
            {
                string sysName = "";

                switch (AppSettings.SystemType)
                {
                    case SystemType.FutDay:
                    case SystemType.FutNight:
                        sysName = "futures";
                        break;

                    case SystemType.OptDay:
                    case SystemType.OptNight:
                        sysName = "options";
                        break;
                }

                string fileName = $"{sysName}_PVC.txt";
                string dir = "";

                SaveFileDialog open = new SaveFileDialog();
                open.Filter = $"*.txt (*.txt)|*.txt";
                open.Title = "請點選儲存檔案之目錄";
                open.FileName = fileName;
                DialogResult openResult = open.ShowDialog();

                if (openResult == DialogResult.OK)
                {
                    dir = Path.GetDirectoryName(open.FileName);
                    gridView.PrintColumnHeaders = false;
                    gridView.ExportToText(open.FileName);

                    string tcpFilePath = Path.Combine(dir, "futures_tcp.txt");
                    gridView.ExportToText(tcpFilePath);

                    MessageBoxExService.Instance().Info($"存檔於{tcpFilePath}");

                    using (var das = Factory.CreateDalSession())
                    {
                        D_XFCM dXFCM = new D_XFCM(das);
                        var data = dXFCM.ListAll();
                        string xfcmFilePath = Path.Combine(dir, "futures_xfcm.txt");
                        ExportElf.ToTxt(data, xfcmFilePath, true);

                        UpdateAccessPermission(ProgramID, das);
                    }

                    MessageBoxExService.Instance().Info($"共下載資料 {_vm.MainGridData.Count} 筆");
                }
                else
                {
                    MessageBoxExService.Instance().Info("儲存不成功，請重新操作");
                }
            }
            catch (Exception ex)
            {
                MessageBoxExService.Instance().Info("儲存不成功，請重新操作");
                throw ex;
            }
            finally
            {
                VmMainUi.HideLoadingWindow();
                CloseWindow();
            }
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
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
    }
}