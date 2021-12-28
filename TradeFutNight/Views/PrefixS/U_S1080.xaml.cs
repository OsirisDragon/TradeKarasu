using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using Shield.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.PrefixS
{
    /// <summary>
    /// U_S1080.xaml 的互動邏輯
    /// </summary>
    public partial class U_S1080 : UserControlParent, IViewSword
    {
        private U_S1080_ViewModel _vm;

        public U_S1080()
        {
            InitializeComponent();
            _vm = (U_S1080_ViewModel)DataContext;
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
            VmMainUi.IsButtonInsertEnabled = false;
            VmMainUi.IsButtonPrintEnabled = true;
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
            VmMainUi.LoadingText = MessageConst.LoadingStatusSaving;

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

        private void GridView_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ZTYPEP_PROD_TYPE")
            {
                switch (e.Value)
                {
                    case "FUT":
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_PRICE_MODEL", "");
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_VALUATION", "FUT");
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_EXERCISE", "");
                        break;

                    case "PHY":
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_PRICE_MODEL", "");
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_VALUATION", "EQTY");
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_EXERCISE", "");
                        break;

                    case "OOF":
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_PRICE_MODEL", "B");
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_VALUATION", "EQTY");
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_EXERCISE", "EURO");
                        break;

                    case "OOP":
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_PRICE_MODEL", "BS");
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_VALUATION", "EQTY");
                        gridMain.SetCellValue(e.RowHandle, "ZTYPEP_EXERCISE", "EURO");
                        break;
                }
            }
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            List<PDK> pdkFut = null;
            List<PDK> pdkOpt = null;

            //選擇權夜盤
            using (var das = Factory.CreateDalSession(SettingFile.Database.Options_AH))
            {
                pdkOpt = new D_PDK(das).ListKindId().ToList();
            }

            //期貨夜盤
            using (var das = Factory.CreateDalSession())
            {
                pdkFut = new D_PDK(das).ListKindId().ToList();
            }

            //合併期貨和選擇權的資料
            var pdkAll = pdkOpt.Concat(pdkFut);
            string kindId;
            List<string> notFoundPdk = new List<string>();
            if (pdkAll.Count() > 0)
            {
                foreach (var item in pdkAll)
                {
                    kindId = item.PDK_KIND_ID;
                    if (kindId.Right(1) == "F" || kindId.Right(1) == "O")
                    {
                        var count = _vm.MainGridData.Count(x => x.ZTYPEP_PROD == kindId);
                        if (count == 0)
                        {
                            notFoundPdk.Add(kindId);
                        }
                    }
                }
            }
            else
            {
                MessageBoxExService.Instance().Error("無期貨商品PDK資料");
                return;
            }

            if (notFoundPdk.Count > 0)
            {
                MessageBoxExService.Instance().Error("以下商品資料未建在ZTYPEP(用PDK檔的資料比對)：" + string.Join(",", notFoundPdk));
            }
            else
            {
                MessageBoxExService.Instance().Info("檢核完成");
            }
        }
    }
}