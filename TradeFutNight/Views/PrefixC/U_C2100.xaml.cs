using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using CrossModel.Interfaces;
using DevExpress.DataProcessing;
using DevExpress.XtraReports.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;
using TradeUtility.File;

namespace TradeFutNight.Views.PrefixC
{
    /// <summary>
    /// U_C2100.xaml 的互動邏輯
    /// </summary>
    public partial class U_C2100 : UserControlParent, IViewSword
    {
        private U_C2100_ViewModel _vm;

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

                foreach (var item in trackableData)
                {
                    if (item.TPPBP_THERICAL_P_REF == null || item.TPPBP_THERICAL_P_REF <= 0)
                    {
                        resultItem.AppendErrorMessage($"{item.TPPBP_PROD_ID}的夜盤期貨開盤基準價不得為空值或小於等於0");
                    }

                    using (var das = Factory.CreateDalSession())
                    {
                        var putUnit = new D_PUT(das).GetPutUnit(item.PDK_PARAM_KEY, item.TPPBP_THERICAL_P_REF);

                        // 取至最接近tick
                        if (!Utility.IsValidTick(item.TPPBP_THERICAL_P_REF, putUnit))
                        {
                            resultItem.AppendErrorMessage($"{item.TPPBP_PROD_ID}的夜盤期貨開盤基準價不符合TICK值{putUnit}的檢查");
                        }
                    }
                }

                if (resultItem.HasError)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(resultItem.ErrorMessage);
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

                var report = CreateReport(_vm.MainGridData, OperationType.Save);
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

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, true, true);
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
            ExportElf.ToCsv(exportData, filePath, false, false);
            MessageBoxExService.Instance().Info($"成功產生檔案至{filePath}");
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            // CSV檔案內容格式:
            // 商品代號,次日期貨開盤基準價
            // 例:NYFA0,99

            var open = new OpenFileDialog
            {
                Filter = $"*.csv (*.csv)|*.csv",
                Title = "請點選資料來源檔案"
            };

            bool? openResult = open.ShowDialog();

            if (openResult == true)
            {
                var importData = ImportElf.FileToDataTable(open.FileName, FileType.Csv, false).AsEnumerable()
                                        .Select(c => new
                                        {
                                            PROD_ID = c.ItemArray[0].AsString(),
                                            TPPBP_THERICAL_P_REF = c.ItemArray[1].AsDecimal()
                                        });

                foreach (var item in importData)
                {
                    var foundRow = _vm.MainGridData.Where(c => c.TPPBP_PROD_ID == item.PROD_ID).SingleOrDefault();

                    if (foundRow != null)
                    {
                        if (foundRow.TPPBP_THERICAL_P_REF == null || foundRow.TPPBP_THERICAL_P_REF != item.TPPBP_THERICAL_P_REF)
                        {
                            foundRow.TPPBP_THERICAL_P_REF = item.TPPBP_THERICAL_P_REF;
                            foundRow.ModifyMark = "*";
                        }
                    }
                }
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var foundRow = _vm.MainGridData.Where(c => c.PDK_KIND_ID == _vm.PdkKindId).FirstOrDefault();

            if (foundRow != null)
            {
                gridMain.CurrentColumn = gridMain.Columns[nameof(foundRow.TPPBP_THERICAL_P_REF)];
                gridView.FocusedRowHandle = _vm.MainGridData.IndexOf(foundRow);
                gridMain.CurrentItem = gridMain.SelectedItem = foundRow;
            }
            else
            {
                MessageBoxExService.Instance().Error($"找不到這個商品");
            }
        }
    }
}