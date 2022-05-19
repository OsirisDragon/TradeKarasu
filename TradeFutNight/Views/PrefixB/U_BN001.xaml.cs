using CrossModel;
using CrossModel.Enum;
using DataEngine;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;
using Shield.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TradeFutNight.Auth;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Specific.PrefixB;
using TradeFutNightData.Gates.Tfxm;
using TradeFutNightData.Models.Sp;
using TradeUtility;
using TradeUtility.File;

namespace TradeFutNight.Views.PrefixB
{
    /// <summary>
    /// U_BN001.xaml 的互動邏輯
    /// </summary>
    public partial class U_BN001 : UserControlParent, IViewSword
    {
        private U_BN001_ViewModel _vm;

        private int _isApplySecret = 0;

        public U_BN001()
        {
            InitializeComponent();
            _vm = (U_BN001_ViewModel)DataContext;
            _vm.View = gridView;
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

            var pgrpDspGrp = new List<ItemInfo>();
            pgrpDspGrp.Add(new ItemInfo() { Text = "1_指數類_台指", Value = 1 });
            _vm.PgrpDspGrps = pgrpDspGrp;

            var task = Task.Run(() =>
            {
                _vm.Open();
                DbLog(MessageConst.Open);
            });
            await task;

            // 預設選擇第一個
            cbPgrpDspGrp.SelectedIndex = 0;
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
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = true }, gridMain, _vm))
                return false;

            if (_vm.PgrpDspGrp == null)
            {
                MessageBoxExService.Instance().Error("請選擇下拉選單");
                return false;
            }

            var task = Task.Run(async () =>
            {
                if (!IsCanRunProgram())
                {
                    MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
                    return false;
                }

                var resultItem = new ResultItem();

                using (var das = Factory.CreateDalSession())
                {
                    var dLogf = new D_LOGF(das);
                    if (dLogf.IsCompleted("BN001", "%COMPLETED_" + _vm.PgrpDspGrp.Text + "%"))
                    {
                        MessageBoxExService.Instance().Error("群組" + _vm.PgrpDspGrp.Text + "已執行過無法再執行");
                        return false;
                    }

                    #region 檢查本日結算價的Tick

                    for (int i = 0; i < _vm.MainGridData.Count; i++)
                    {
                        var item = _vm.MainGridData[i];
                        var dPutUnit = new D_PUT(das).GetPutUnit(item.PDK_PARAM_KEY, item.FMIF_SETTLE_PRICE);

                        if (!Utility.IsValidTick(item.FMIF_SETTLE_PRICE, dPutUnit))
                        {
                            _vm.FocusRow(item, colFmifSettlePrice, true, true);
                            MessageBoxExService.Instance().Error(item.PROD_ID_OUT + " " + item.PROD_SETTLE_DATE + "的本日結算價不符合Tick跳動單位:" + dPutUnit);
                            return false;
                        }
                    }

                    #endregion 檢查本日結算價的Tick

                    #region 檢查本日結算價是否在漲停與跌停之間

                    foreach (var item in _vm.MainGridData)
                    {
                        if (item.FMIF_SETTLE_PRICE < item.PROD_FALL_PRICE || item.FMIF_SETTLE_PRICE > item.PROD_RAISE_PRICE)
                        {
                            _vm.FocusRow(item, colFmifSettlePrice, true, true);
                            MessageBoxExService.Instance().Error(item.PROD_ID_OUT + " " + item.PROD_SETTLE_DATE + "的本日結算價超出漲跌停的範圍");
                            return false;
                        }
                    }

                    #endregion 檢查本日結算價是否在漲停與跌停之間

                    #region 檢查如果是暫停交易的話，本日結算價和開盤參考價有沒有一樣

                    foreach (var item in _vm.MainGridData)
                    {
                        if (item.CATEGORY == "Z" && item.CLSPRC_SETTLE_PRICE != item.FMIF_SETTLE_PRICE)
                        {
                            _vm.FocusRow(item, colFmifSettlePrice, true, true);
                            MessageBoxExService.Instance().Error(item.PROD_ID_OUT + " " + item.PROD_SETTLE_DATE + "為暫停交易，其結算價和開盤價不同請問是否繼續 ?");
                            return false;
                        }
                    }

                    #endregion 檢查如果是暫停交易的話，本日結算價和開盤參考價有沒有一樣

                    #region 先把資料列印出來，等結算部確認之後再按繼續

                    // 如果有資料的話才要確認
                    if (_vm.MainGridData.Count != 0)
                    {
                        var report = CreateReport(_vm.MainGridData, OperationType.Confirm);
                        var reportGate = await new ReportGate(report).CreateDocumentAsync();
                        await reportGate.ExportPdf(GetExportFilePath("(check)"));
                        await reportGate.Print();

                        if (MessageBoxExService.Instance().Confirm("請檢查結算價格，資料無誤後，再按確認繼續執行") != MessageBoxResult.Yes)
                            return false;
                    }

                    #endregion 先把資料列印出來，等結算部確認之後再按繼續
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
                var items = _vm.MainGridData;

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dFmif = new D_FMIF(das);
                        var dProd = new D_PROD(das);

                        // 存檔前，先把夜盤FMIF裡面的OSW_GRP為10的都清成0
                        // 因為BN037會算出全部OSW_GRP為10的資料並寫進去，但很多是沒有用到的
                        // 最後再用我們畫面上的值寫回去
                        dFmif.ClearSettlePriceByMarketClose("10");

                        foreach (var item in items)
                        {
                            dFmif.UpdateSettlePrice(item.FMIF_PROD_ID, item.FMIF_SETTLE_PRICE);
                            dProd.UpdateSettlePrice(item.FMIF_PROD_ID, item.FMIF_SETTLE_PRICE);
                        }

                        // 更新每日結算結確認碼PGRP_DSP_CONFIRM
                        var dPgrp = new D_PGRP(das);
                        dPgrp.UpdateDspConfirm(10, _vm.PgrpDspGrp.Value.AsInt());

                        UpdateAccessPermission(ProgramID, das);

                        DbLog("結算價確認", das);

                        DbLog(MessageConst.Completed + "_" + _vm.PgrpDspGrp.Text, das);

                        das.Commit();
                    }
                    catch (Exception ex)
                    {
                        das.Rollback();
                        throw ex;
                    }
                }

                // 更新日盤期貨的資料
                using (var dasDayFut = Factory.CreateDalSession(SettingDatabaseInfo.FutDay))
                {
                    dasDayFut.Begin();

                    try
                    {
                        var dProd = new D_PROD(dasDayFut);
                        var dNfmif = new D_NFMIF(dasDayFut);

                        foreach (var item in items)
                        {
                            dProd.UpdateSettlePrice(item.FMIF_PROD_ID, item.FMIF_SETTLE_PRICE);
                            dNfmif.UpdateSettlePrice(item.FMIF_PROD_ID, item.FMIF_SETTLE_PRICE);
                        }

                        dasDayFut.Commit();
                    }
                    catch
                    {
                        dasDayFut.Rollback();
                        MessageBoxExService.Instance().Error("更新日盤PROD_SETTLE_PRICE與NFMIF_SETTLE_PRICE失敗");
                    }
                }

                var report = CreateReport(_vm.MainGridData, OperationType.Save);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath("(issue)"));
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + AppSettings.DashForTitle + ProgramName;

            bool hasHandlePerson = false;
            bool hasConfirmPerson = false;
            bool hasManagerPerson = false;

            switch (operationType)
            {
                case OperationType.Confirm:
                    reportTitle = ProgramID + AppSettings.DashForTitle + "結算價格確認表";
                    hasHandlePerson = hasConfirmPerson = hasManagerPerson = true;
                    break;

                case OperationType.Save:
                    reportTitle = ProgramID + AppSettings.DashForTitle + "結算價格公布表";
                    break;

                case OperationType.Print:
                    reportTitle += AppSettings.DashForTitle + "列印";
                    hasHandlePerson = hasConfirmPerson = hasManagerPerson = false;
                    break;

                default:
                    break;
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, hasHandlePerson, hasConfirmPerson, hasManagerPerson);
            rptSetting.HeaderColumnsFontSize = 8;
            rptSetting.ContentColumnsFontSize = 8;
            rptSetting.ContentColumnsWidthScaleFactor = 0.8f;
            rptSetting.HeaderColumnsWidthScaleFactor = 0.8f;
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

        private void CbPgrpDspGrp_EditValueChanging(object sender, EditValueChangingEventArgs e)
        {
            if (e.NewValue != null)
            {
                var pgrpDspGrp = e.NewValue.AsInt();
                VmMainUi.ShowLoadingWindow();

                using (var das = Factory.CreateDalSession())
                {
                    // 檢查BN037是否已經執行
                    var dLogf = new D_LOGF(das);
                    if (!dLogf.IsCompleted("BN037", "%COMPLETED%"))
                    {
                        MessageBoxExService.Instance().Error("BN037尚未執行");

                        // 讓下拉選單跳回去
                        e.IsCancel = true;
                        e.Handled = true;
                        return;
                    }

                    // 抓取主要資料
                    var dBn001 = new D_BN001<UIModel_BN001>(das);
                    var items = dBn001.ListByPgrpDspGrp(pgrpDspGrp, DateTime.Today, Ocf.OCF_PREV_DATE, _isApplySecret).Select(d =>
                    {
                        d.PDK_NAME = d.PDK_NAME.Substring(0, 4);
                        return d;
                    });

                    if (items.Count() == 0)
                    {
                        MessageBoxExService.Instance().Info("本日無需公布每日結算價之契約月份，仍需按存檔");
                        return;
                    }

                    var resultItem = new ResultItem();

                    // 呼叫SP，然後把SP回傳的值填入(如果主畫面的Grid有值才會填)
                    CaculateBySP(das, resultItem, items);

                    // 三階段漲跌幅
                    CaculatePlimitRaiseFall(das, items);

                    // 計算現貨遠匯欄位
                    CaculatePdkProdIdx(items);

                    // 最後一筆成交價格和成交時間
                    CaculateMtfPriceAndTime(items, _vm.MtfEveryProdLastRecords);

                    // 昨日OI
                    CaculateOI(das, items);

                    // 註1的顯示記號
                    foreach (var item in items)
                    {
                        item.REMARK_FIRST = DisplayForRemarkFirst(item);
                    }

                    // Binding to grid
                    _vm.MainGridData = items.ToList();

                    // Focus to grid and column
                    gridMain.Focus();
                    gridMain.CurrentColumn = colFmifSettlePrice;

                    // 如果畫面上有資料的話，將畫面上的原始資料存成TXT檔案
                    if (items.Count() != 0)
                        ExportElf.ToCsv(_vm.MainGridData, GetExportFilePath(FileType.Csv, "index_original"), true);

                    if (resultItem.HasError)
                    {
                        MessageBoxExService.Instance().Error(resultItem.ErrorMessage);
                        return;
                    }
                }

                VmMainUi.HideLoadingWindow();
            }
        }

        private void BtnSpecial_Click(object sender, RoutedEventArgs e)
        {
            if (new AuthGate().ShowAuthDouble(ProgramID))
            {
                if (_vm.PgrpDspGrp != null)
                {
                    _isApplySecret = 1;
                    var newArg = new EditValueChangingEventArgs(null, _vm.PgrpDspGrp.Value);
                    CbPgrpDspGrp_EditValueChanging(cbPgrpDspGrp, newArg);
                    _isApplySecret = 0;
                }
                else
                {
                    MessageBoxExService.Instance().Error("請選擇下拉選單");
                }
            }
        }

        protected override void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = ((GridViewBase)sender);
            view.CommitEditing();

            var uiModel = e.Row as UIModel_BN001;

            var gridControl = ((GridControl)(((GridViewBase)sender).Parent));

            view.CellValueChanged -= new CellValueChangedEventHandler(view_CellValueChanged);

            // 註2的修改記號
            gridControl.SetCellValue(e.RowHandle, colRemarkSecond, "※");

            // 註1的顯示記號
            gridControl.SetCellValue(e.RowHandle, colRemarkFirst, DisplayForRemarkFirst(uiModel));

            view.CellValueChanged += new CellValueChangedEventHandler(view_CellValueChanged);
        }

        private void GridMain_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    gridView.CommitEditing();
                    gridView.MoveNextRow();
                    gridMain.SelectedItem = gridMain.GetRow(gridView.FocusedRowHandle);
                    gridView.ShowEditor(true);
                }));
            }
        }

        private void CaculateBySP(DalSession das, ResultItem resultItem, IEnumerable<UIModel_BN001> items)
        {
            IEnumerable<DTO_SP_proc_AH_settle_price> resultSP;

            var sp = new D_StoredProcedure<DTO_SP_proc_AH_settle_price>(das);
            resultSP = sp.proc_AH_settle_price("Q", 10, 1);

            foreach (var item in items)
            {
                var foundItem = resultSP.Where(c => c.tmpprc_id_out == item.PROD_ID_OUT && c.tmpprc_settle_date == item.PROD_SETTLE_DATE).SingleOrDefault();

                if (foundItem != null)
                {
                    item.CLSPRC_SETTLE_PRICE = foundItem.tmpprc_clsprc_settle_price;
                    item.LAST_ONE_MIN_WEIGHT_AVG_PRICE = foundItem.tmpprc_last_price;
                    item.LAST_BUY_PRICE = foundItem.tmpprc_bo_price;
                    item.LAST_SELL_PRICE = foundItem.tmpprc_so_price;
                    item.BUY_SELL_MIDDLE = foundItem.tmpprc_bs_price;
                    item.CATEGORY = foundItem.tmpprc_adjust_type;
                }
                else
                {
                    resultItem.AppendErrorMessage($"{item.PROD_ID} {item.PROD_SETTLE_DATE} not finded in proc_AH_settle_price");
                }
            }
        }

        private void CaculatePlimitRaiseFall(DalSession das, IEnumerable<UIModel_BN001> items)
        {
            // 抓出所有的PRODLMT
            var prodlmts = new D_PRODLMT(das).ListAll();

            // 先從PRODLMT裡面抓出所有第一階段漲跌幅的資料，填回PROD的漲跌幅裡面
            foreach (var item in items)
            {
                var foundProdlmt = prodlmts.FirstOrDefault(c => c.PRODLMT_PROD_ID == item.PROD_ID && c.PRODLMT_PLIMIT_CODE == 1);
                if (foundProdlmt != null)
                {
                    item.PROD_RAISE_PRICE = foundProdlmt.PRODLMT_RAISE_PRICE.GetValueOrDefault();
                    item.PROD_FALL_PRICE = foundProdlmt.PRODLMT_FALL_PRICE.GetValueOrDefault();
                }
            }

            // 如果PLIMIT有資料代表有放寬漲跌幅
            // 2是當日放寬至2階，3是當日放寬至3階
            var plimits = new D_PLIMIT(das).ListAll();

            foreach (var plimit in plimits)
            {
                string plimitProdId = plimit.PLIMIT_PROD_ID;
                int plimitRaiseCode = plimit.PLIMIT_RAISE_CODE;
                int plimitFallCode = plimit.PLIMIT_FALL_CODE;

                var foundItem = items.FirstOrDefault(c => c.PROD_ID == plimitProdId);
                if (foundItem != null)
                {
                    var foundProdLmt = prodlmts.FirstOrDefault(c => c.PRODLMT_PROD_ID == plimitProdId && c.PRODLMT_PLIMIT_CODE == plimitRaiseCode);
                    if (foundProdLmt != null)
                    {
                        foundItem.PROD_RAISE_PRICE = foundProdLmt.PRODLMT_RAISE_PRICE.GetValueOrDefault();
                    }

                    foundProdLmt = prodlmts.FirstOrDefault(c => c.PRODLMT_PROD_ID == plimitProdId && c.PRODLMT_PLIMIT_CODE == plimitFallCode);
                    if (foundProdLmt != null)
                    {
                        foundItem.PROD_FALL_PRICE = foundProdLmt.PRODLMT_FALL_PRICE.GetValueOrDefault();
                    }
                }
            }
        }

        private void CaculatePdkProdIdx(IEnumerable<UIModel_BN001> items)
        {
            foreach (var item in items)
            {
                string prodSettleDate = item.PROD_SETTLE_DATE;
                string transferMonth = "";

                if (prodSettleDate.Length == 6)
                {
                    switch (prodSettleDate.Substring(4, 2))
                    {
                        case "01":
                            transferMonth = "JAN";
                            break;

                        case "02":
                            transferMonth = "FEB";
                            break;

                        case "03":
                            transferMonth = "MAR";
                            break;

                        case "04":
                            transferMonth = "APR";
                            break;

                        case "05":
                            transferMonth = "MAY";
                            break;

                        case "06":
                            transferMonth = "JUN";
                            break;

                        case "07":
                            transferMonth = "JUL";
                            break;

                        case "08":
                            transferMonth = "AUG";
                            break;

                        case "09":
                            transferMonth = "SEP";
                            break;

                        case "10":
                            transferMonth = "OCT";
                            break;

                        case "11":
                            transferMonth = "NOV";
                            break;

                        case "12":
                            transferMonth = "DEC";
                            break;

                        default:
                            break;
                    }

                    transferMonth = transferMonth + prodSettleDate.Substring(2, 2);
                }

                if (item.PROD_ID_OUT == "BRF")
                {
                    using (var das = Factory.CreateDalSession(SettingDatabaseInfo.TfxmDay))
                    {
                        var dCmemdSettle = new D_CMEMD_SETTLE(das);
                        var latestCmemdSettle = dCmemdSettle.GetLatest(Ocf.OCF_DATE, "LCO%", transferMonth);

                        if (latestCmemdSettle != null)
                        {
                            item.PDK_PROD_IDX = latestCmemdSettle.CMEMD_SETTLE_PX;
                        }
                    }
                }
            }
        }

        private void CaculateMtfPriceAndTime(IEnumerable<UIModel_BN001> items, IEnumerable<UIModel_BN001> mtfEveryProdLastRecords)
        {
            foreach (var item in items)
            {
                var foundMtf = mtfEveryProdLastRecords.Where(c => c.PROD_ID_OUT == item.PROD_ID_OUT && c.PROD_SETTLE_DATE == item.PROD_SETTLE_DATE).FirstOrDefault();
                if (foundMtf != null)
                {
                    item.MTF_PRICE = foundMtf.MTF_PRICE;
                    item.MTF_ORIG_TIME = foundMtf.MTF_ORIG_TIME;
                }
            }
        }

        private void CaculateOI(DalSession das, IEnumerable<UIModel_BN001> items)
        {
            var clsprcs = new D_CLSPRC(das).ListAll();

            foreach (var item in items)
            {
                var foundClsprc = clsprcs.Where(c => c.CLSPRC_PROD_ID == item.PROD_ID).FirstOrDefault();
                if (foundClsprc != null)
                {
                    item.CLSPRC_OPEN_INTEREST = foundClsprc.CLSPRC_OPEN_INTEREST;
                }
            }
        }

        private string DisplayForRemarkFirst(UIModel_BN001 uiModel)
        {
            if (Ocf.OCF_DATE == uiModel.PROD_DELIVERY_DATE) return "";

            var isLessThanBuyPrice = false;
            // 檢查本日結算價是否超出買賣價，是的話註1顯示X
            if (uiModel.LAST_BUY_PRICE != 0)
            {
                if (uiModel.FMIF_SETTLE_PRICE < uiModel.LAST_BUY_PRICE)
                {
                    isLessThanBuyPrice = true;
                    // 檢查當日無OI及無成交時，註1顯示O
                    if (uiModel.CLSPRC_OPEN_INTEREST == 0 && uiModel.FMIF_M_COUNT_TAL == 0)
                    {
                        return "O";
                    }
                    else
                    {
                        return "X";
                    }
                }
                else
                {
                    return "";
                }
            }

            if (uiModel.LAST_SELL_PRICE != 0)
            {
                if (uiModel.FMIF_SETTLE_PRICE > uiModel.LAST_SELL_PRICE)
                {
                    // 檢查當日無OI及無成交時，註1顯示O
                    if (uiModel.CLSPRC_OPEN_INTEREST == 0 && uiModel.FMIF_M_COUNT_TAL == 0)
                    {
                        return "O";
                    }
                    else
                    {
                        return "X";
                    }
                }
                else if (!isLessThanBuyPrice)
                {
                    return "";
                }
            }

            return "";
        }
    }
}