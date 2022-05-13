using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix3;
using TradeUtility;
using TradeUtility.File;

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30105.xaml 的互動邏輯
    /// </summary>
    public partial class U_30105 : UserControlParent, IViewSword
    {
        private U_30105_ViewModel _vm;

        public U_30105()
        {
            InitializeComponent();
            _vm = (U_30105_ViewModel)DataContext;
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
            VmMainUi.IsButtonSaveEnabled = true;
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
            await _vm.Query();
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
                    MessageBoxExService.Instance().Error($"(盤中){MessageConst.NotAllowedExcute},視窗即將關閉");
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

            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    try
                    {
                        var d30105 = new D_30105<UIModel_30105>(das);
                        var pdkList = d30105.ListPDK().ToList();

                        DataTable dt = new DataTable();
                        DataColumn col = new DataColumn();
                        col.ColumnName = col.Caption = "No";
                        col.DataType = typeof(int);
                        dt.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "MPD_FCM_NO";
                        col.Caption = "期貨商代號";
                        dt.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "FCM_NAME";
                        col.Caption = "造市者名稱";
                        dt.Columns.Add(col);

                        foreach (var item in pdkList)
                        {
                            col = new DataColumn();
                            col.ColumnName = item.MPD_PROD_ID;
                            col.Caption = item.PDK_NAME;
                            dt.Columns.Add(col);
                        }
                        string fcmNo = "";
                        int rowIndex = 0;
                        DataRow dr = null;
                        foreach (var item in _vm.MainGridData)
                        {
                            if (fcmNo != item.MPD_FCM_NO.AsString())
                            {
                                rowIndex++;
                                dr = dt.NewRow();
                                fcmNo = item.MPD_FCM_NO.AsString();
                                dr["No"] = rowIndex;
                                dr["MPD_FCM_NO"] = fcmNo;
                                dr["FCM_NAME"] = item.FCM_NAME;
                                dt.Rows.Add(dr);
                            }
                            dr[item.MPD_PROD_ID] = "◎";
                        }

                        //產生檔案
                        string saveFilePath = GetExportFilePath(FileType.Xlsx);
                        string routinePath = Path.Combine(AppSettings.LocalRoutineDataDirectory, "FUT_30105.xls");

                        ExportElf.ToXlsx(dt, saveFilePath, true);
                        File.Copy(saveFilePath, routinePath, true);
                        MessageBoxExService.Instance().Info($"{routinePath}和\n{saveFilePath} 下載完成!");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                VmMainUi.HideLoadingWindow();
            });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

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