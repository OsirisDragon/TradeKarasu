using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using Shield.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Tfxm;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30063.xaml 的互動邏輯
    /// </summary>
    public partial class U_30063 : UserControlParent, IViewSword
    {
        private U_30063_ViewModel _vm;

        public U_30063()
        {
            InitializeComponent();
            _vm = (U_30063_ViewModel)DataContext;
        }

        public async Task<bool> IsCanRun()
        {
            var task = Task.Run(() =>
            {
                var isCanRun = true;
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
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = true }, gridMain, _vm))
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
                var checkItems = trackableData.ChangedItems;

                if (checkItems.Count() == 0)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NoChangedData);
                    return false;
                }

                foreach (var item in trackableData.ChangedItems)
                {
                    //if (item.TPPINTD_SECOND_MONTH > 0 && string.IsNullOrEmpty(item.TPPINTD_SECOND_KIND_ID))
                    //{
                    //    resultItem.AppendErrorMessage($"請輸入第{trackableData.AsEnumerable().IndexOf(item) + 1}筆的第二支腳契約代碼");
                    //}

                    //if (!string.IsNullOrEmpty(item.TPPINTD_SECOND_KIND_ID) && item.TPPINTD_SECOND_MONTH <= 0)
                    //{
                    //    resultItem.AppendErrorMessage($"請輸入第{trackableData.AsEnumerable().IndexOf(item) + 1}筆的第二支腳月份序號");
                    //}

                    //Dispatcher.Invoke(() =>
                    //{
                    //    item.TPPINTD_USER_ID = UserID;
                    //    item.TPPINTD_W_TIME = DateTime.Now;
                    //});
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
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();
                var domainData = CustomMapper<TPPINTD>(trackableData.ChangedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dTppintd = new D_TPPINTD(das);
                        dTppintd.Update(domainData);

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

                var report = CreateReport(domainData, OperationType.Save);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private IList<T> CustomMapper<T>(IEnumerable<UIModel_30063> items) where T : TPPINTD
        {
            var listResult = new List<T>();

            Dispatcher.Invoke(() =>
            {
                foreach (var item in items)
                {
                    var newItem = _vm.MapperInstance.Map<T>(item);

                    var trackItem = item.CastToIChangeTrackable();
                    //newItem.OriginalData = trackItem.GetOriginal();
                    listResult.Add(newItem);
                }
            });

            return listResult;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            switch (operationType)
            {
                case OperationType.Save:

                    break;

                case OperationType.Print:

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

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            button.IsEnabled = false;

            using (var das = Factory.CreateDalSession(SettingDatabaseInfo.TfxmDay))
            {
                var dGdex = new D_GDEX(das);
                var gdex = dGdex.GetTopOne(1, Ocf.OCF_DATE);
            }

            //IEagleGate eagleGate = new MexGate(MsgSysType.FutNight, "TFX.FXM.TXR.QUERY", "all");
            //EagleArgs ea = new EagleArgs();
            //ea.AddEagleContent(new EagleContent() { Item = "QuerySingle", Value = "all" });
            //eagleGate.AddArgument(ea);
            //string result = eagleGate.SendAndReceiveData("FXM.TFX.TXR.RESULT", "all", 3000).ReceiveData;
            //var mexReceivedData = JsonConvert.DeserializeObject<List<MexReceivedData_30063>>(result);

            button.IsEnabled = true;
        }

        private void MexReceivedDataProcess(List<MexReceivedData_30063> items)
        {
            foreach (var item in items)
            {
                char mType = Convert.ToChar(item.type);
                string mTime = item.time;
                decimal mBid = item.bid / (decimal)10000;
                decimal mAsk = item.ask / (decimal)10000;
                decimal mPrice = item.price / (decimal)10000;

                #region 找出對應的row

                string filterStr = "";
                UIModel_30063 findItem = null;

                if (mType == '2')
                {
                    findItem = _vm.MainGridData.Where(c => c.EXRT_CURRENCY_TYPE == mType && c.EXRT_COUNT_CURRENCY == '1').SingleOrDefault();
                }
                else
                {
                    findItem = _vm.MainGridData.Where(c => c.EXRT_CURRENCY_TYPE == mType && c.EXRT_COUNT_CURRENCY == '2').SingleOrDefault();
                }

                if (findItem == null)
                {
                    findItem = new UIModel_30063();
                }

                findItem.EXRT_CURRENCY_TYPE = mType;
                findItem.EX_OK = mPrice;
                findItem.EX_BID = mBid;
                findItem.EX_ASK = mAsk;
                findItem.EX_TIME = mTime;

                #endregion 找出對應的row

                #region 設定一般的本公司匯率欄位

                // 市場均價
                findItem.EX_MID = Math.Round(((mBid + mAsk) / 2), 6);

                switch (mType)
                {
                    // 日,港,人民幣,南非幣<比美元小>
                    case '4':
                    case '7':
                    case '8':
                    case 'A':
                        // 設定換匯後幣別
                        findItem.EXRT_COUNT_CURRENCY = '2';

                        if (findItem.EX_MID != 0)
                        {
                            findItem.EXRT_EXCHANGE_RATE = Math.Round(1 / findItem.EX_MID, 6);
                        }
                        else
                        {
                            MessageBoxExService.Instance().Info(mType + "均價為0請手動輸入");
                        }
                        break;

                    // 歐元,英鎊,澳幣,紐幣
                    case '3':
                    case '5':
                    case '6':
                    case 'G':
                        // 設定換匯後幣別
                        findItem.EXRT_COUNT_CURRENCY = '2';
                        findItem.EXRT_EXCHANGE_RATE = findItem.EX_MID;
                        break;

                    // 美金
                    case '2':
                        // 設定換匯後幣別
                        findItem.EXRT_COUNT_CURRENCY = '1';

                        if (mPrice > 0)
                        {
                            findItem.EXRT_EXCHANGE_RATE = mPrice;
                        }
                        else if (mPrice == 0)
                        {
                            // 早上路透社未能收美元商品，用昨天日期
                            double GDEX_EX_RATE = 0;

                            using (var das = Factory.CreateDalSession(SettingDatabaseInfo.TfxmDay))
                            {
                                var dGdex = new D_GDEX(das);
                                var gdex = dGdex.GetTopOne(1, Ocf.OCF_DATE);

                                if (gdex != null)
                                {
                                    findItem.EXRT_EXCHANGE_RATE = Math.Round(gdex.GDEX_EX_RATE, 6);
                                }
                            }
                        }

                        break;

                    default:
                        break;
                }

                #endregion 設定一般的本公司匯率欄位

                #region 設定特定的匯率

                double fxUsdToTwd = 0;

                // 取得美元兌台幣匯率
                dv = gridViewMain.Find("EXRT_CURRENCY_TYPE='2' AND EXRT_COUNT_CURRENCY  ='1' ");

                if (dv.Count > 0)
                {
                    fxUsdToTwd = dv[0]["EXRT_EXCHANGE_RATE"].AsDouble();

                    #region 設定人民幣兌台幣匯率

                    double fxCnyToTwd = 0;

                    if (mType == "8")
                    {
                        // 人民幣兌台幣匯率 = 人民幣兌美元匯率(8->2)*美元兌台幣匯率(2->1)(四捨五入至小數後6位)
                        filterStr = "EXRT_CURRENCY_TYPE='" + mType + "' AND EXRT_COUNT_CURRENCY='2' ";

                        dv = gridViewMain.Find(filterStr);

                        if (dv.Count > 0)
                        {
                            row = dv[0].Row;

                            fxCnyToTwd = Math.Round(row["EXRT_EXCHANGE_RATE"].AsDouble() * fxUsdToTwd, 6);

                            filterStr = "EXRT_CURRENCY_TYPE='" + mType + "' AND EXRT_COUNT_CURRENCY='1' ";

                            dv = gridViewMain.Find(filterStr);

                            if (dv.Count > 0)
                            {
                                row = dv[0].Row;
                            }
                            else
                            {
                                row = gridViewMain.AddRow();
                            }

                            row["EXRT_EXCHANGE_RATE"] = fxCnyToTwd;
                            row["EXRT_CURRENCY_TYPE"] = "8";
                            row["EXRT_COUNT_CURRENCY"] = "1";
                            row["EX_TIME"] = DateTime.Now.ToString("HH:mm:ss");
                        }
                        else
                        {
                            MessageDisplay.Error("找不到人民幣兌美元匯率(8->2)");
                        }
                    }

                    #endregion 設定人民幣兌台幣匯率

                    #region 設定日幣兌台幣匯率

                    double fxJpyToTwd = 0;

                    if (mType == "4")
                    {
                        // 日幣兌台幣匯率 = 日幣兌美元匯率(4->2) * 美元兌台幣匯率(2->1) (四捨五入至小數後6位)
                        filterStr = "EXRT_CURRENCY_TYPE='" + mType + "' AND EXRT_COUNT_CURRENCY='2' ";

                        dv = gridViewMain.Find(filterStr);

                        if (dv.Count > 0)
                        {
                            row = dv[0].Row;

                            fxJpyToTwd = Math.Round(row["EXRT_EXCHANGE_RATE"].AsDouble() * fxUsdToTwd, 6);

                            filterStr = "EXRT_CURRENCY_TYPE='" + mType + "' AND EXRT_COUNT_CURRENCY='1' ";

                            dv = gridViewMain.Find(filterStr);

                            if (dv.Count > 0)
                            {
                                row = dv[0].Row;
                            }
                            else
                            {
                                row = gridViewMain.AddRow();
                            }

                            row["EXRT_EXCHANGE_RATE"] = fxJpyToTwd;
                            row["EXRT_CURRENCY_TYPE"] = "4";
                            row["EXRT_COUNT_CURRENCY"] = "1";
                            row["EX_TIME"] = DateTime.Now.ToString("HH:mm:ss");
                        }
                        else
                        {
                            MessageDisplay.Error("找不到日幣兌美元匯率(4->2)");
                        }
                    }

                    #endregion 設定日幣兌台幣匯率
                }
                else
                {
                    MessageDisplay.Error("找不到美元兌台幣匯率(2->1)");
                }

                #endregion 設定特定的匯率
            }
        }
    }
}