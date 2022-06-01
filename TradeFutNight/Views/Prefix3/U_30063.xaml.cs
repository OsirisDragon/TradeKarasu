using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using Eagle;
using Newtonsoft.Json;
using Shield.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
using TradeUtility;

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30063.xaml 的互動邏輯
    /// </summary>
    public partial class U_30063 : UserControlParent, IViewSword
    {
        private U_30063_ViewModel _vm;

        private Timer _timer;
        private Dictionary<char, string> _currencysReceive = new Dictionary<char, string>();
        private Dictionary<char, string> _currencysCompare = new Dictionary<char, string>();

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

            /* 日盤專用
            int currOpenSw = 0;
            using (var das = Factory.CreateDalSession())
            {
                var dOswcur = new D_OSWCUR(das);
                currOpenSw = dOswcur.GetCurrOpenSwByGrp(5);
            }

            if (currOpenSw >= 130)
            {
                MessageBoxExService.Instance().Info("第二盤每日結算價業已發送，如需更新匯率，請輸入另一作業人員帳號密碼");
                if (!new AuthGate().ShowAuthDouble(ProgramID))
                    return;
            }

            BtnRefresh_Click(btnRefresh, null);
            */
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

            if (!IsCanRunProgram())
            {
                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
                return false;
            }

            var resultItem = new ResultItem();

            foreach (var item in _vm.MainGridData)
            {
                // 檢查美元兌台幣不可輸入超過小數4位
                if (item.EXRT_CURRENCY_TYPE == '2' && item.EXRT_COUNT_CURRENCY == '1')
                {
                    if (Decimal.Round(item.EXRT_EXCHANGE_RATE, 4) != item.EXRT_EXCHANGE_RATE)
                    {
                        _vm.FocusRow(item);
                        MessageBoxExService.Instance().Error("美元兌台幣不可輸入資料超過小數4位");
                        return false;
                    }
                }

                // 設定EXRT_MARKET_EXCHANGE_RATE
                item.EXRT_MARKET_EXCHANGE_RATE = item.EX_MID;

                if (item.EXRT_MARKET_EXCHANGE_RATE == 0)
                {
                    item.EXRT_EXCHANGE_RATE = item.EXRT_EXCHANGE_RATE;
                }

                // 設定使用者和時間
                item.EXRT_USER_ID = UserID;
                item.EXRT_W_TIME = DateTime.Now;
            }

            if (MessageBoxExService.Instance().Confirm(MessageConst.ConfirmSave) == MessageBoxResult.No)
                return false;

            await Task.Yield();

            return true;
        }

        public async Task Save()
        {
            VmMainUi.LoadingText = MessageConst.LoadingStatusSaving;
            VmMainUi.ShowLoadingWindow();

            var task = Task.Run(async () =>
            {
                var operate = GetChanges<UIModel_30063, EXRT>(_vm.MainGridData, _vm);

                // 日盤更新
                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dExrt = new D_EXRT(das);
                        dExrt.Save(operate);

                        using (var dasOpposite = Factory.CreateDalSession(DataBaseEngine.GetOppositeDb()))
                        {
                            var dExrtOpposite = new D_EXRT(dasOpposite);
                            dExrtOpposite.Save(operate);
                        }

                        // 發送mex訊息--期貨
                        IEagleGate eagleGate = new MexGate(MsgSysType.FutDay, "TFX.FUT.PROD.EXRT", "all");
                        EagleArgs ea = new EagleArgs();
                        ea.AddEagleContent(new EagleContent() { Item = "EXRT", Value = "send" });
                        eagleGate.AddArgument(ea);
                        eagleGate.Send();

                        // 發送mex訊息--選擇權
                        eagleGate = new MexGate(MsgSysType.OptDay, "TFX.OPT.PROD.EXRT", "all");
                        ea = new EagleArgs();
                        ea.AddEagleContent(new EagleContent() { Item = "EXRT", Value = "send" });
                        eagleGate.AddArgument(ea);
                        eagleGate.Send();

                        UpdateAccessPermission(ProgramID, das);

                        das.Commit();

                        DbLog("日盤更新成功", das);
                    }
                    catch (Exception ex)
                    {
                        das.Rollback();
                        throw ex;
                    }

                    // 夜盤的更新寫這邊，因為不要讓夜盤更新的失敗影響到日盤的
                    using (var dasNight = Factory.CreateDalSession(SettingDatabaseInfo.FutNight))
                    {
                        var dExrt = new D_EXRT(dasNight);
                        dExrt.Save(operate);

                        using (var dasOpposite = Factory.CreateDalSession(SettingDatabaseInfo.OptNight))
                        {
                            var dExrtOpposite = new D_EXRT(dasOpposite);
                            dExrtOpposite.Save(operate);
                        }

                        // 發送mex訊息--期貨夜盤
                        IEagleGate eagleGate = new MexGate(MsgSysType.FutNight, "TFX.FUT.PROD.EXRT", "all");
                        EagleArgs ea = new EagleArgs();
                        ea.AddEagleContent(new EagleContent() { Item = "EXRT", Value = "send" });
                        eagleGate.AddArgument(ea);
                        eagleGate.Send();

                        // 發送mex訊息--選擇權夜盤
                        eagleGate = new MexGate(MsgSysType.OptNight, "TFX.OPT.PROD.EXRT", "all");
                        ea = new EagleArgs();
                        ea.AddEagleContent(new EagleContent() { Item = "EXRT", Value = "send" });
                        eagleGate.AddArgument(ea);
                        eagleGate.Send();

                        DbLog("夜盤更新成功", das);
                    }

                    DbLog(MessageConst.Completed, das);
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

            switch (operationType)
            {
                case OperationType.Save:
                    reportTitle += "-變更";
                    break;

                case OperationType.Print:
                    reportTitle += "-列印";
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
            VmMainUi.IsButtonSaveEnabled = false;

            // 這是比對用，比對Mex傳回來的匯率有沒有完整的這些
            _currencysCompare.Clear();
            _currencysCompare.Add('2', "美元");
            _currencysCompare.Add('3', "歐元");
            _currencysCompare.Add('4', "日幣");
            _currencysCompare.Add('5', "英鎊");
            _currencysCompare.Add('6', "澳幣");
            _currencysCompare.Add('7', "港幣");
            _currencysCompare.Add('8', "人民幣");
            _currencysCompare.Add('A', "南非幣");
            _currencysCompare.Add('G', "紐幣");

            // 即將要收到的幣別先清空
            _currencysReceive.Clear();

            var task = Task.Run(() =>
            {
                IEagleGate eagleGate = new MexGate(MsgSysType.FutNight, "TFX.FXM.TXR.QUERY", "all");
                EagleArgs ea = new EagleArgs();
                ea.AddEagleContent(new EagleContent() { Item = "QuerySingle", Value = "all" });
                eagleGate.AddArgument(ea);
                string result = eagleGate.SendAndReceiveData("FXM.TFX.TXR.RESULT", "all", 1500).ReceiveData;
                var mexReceivedData = JsonConvert.DeserializeObject<List<MexReceivedData_30063>>(result);

                Dispatcher.Invoke(() =>
                {
                    MexReceivedDataProcess(mexReceivedData);
                });
            });

            var timerState = new TimerState { Counter = 6 };
            _timer = new Timer(callback: new TimerCallback(TimerTask), state: timerState, dueTime: 0, period: 1000);
        }

        private void TimerTask(object timerState)
        {
            var state = timerState as TimerState;

            Dispatcher.Invoke(() =>
            {
                if (state.Counter == 0)
                {
                    // 停止計時器
                    _timer.Dispose();

                    btnRefresh.Content = "更新匯率";

                    string msgResult = "";

                    // 檢查mex是不是都有回傳該傳回的幣別
                    foreach (var curKey in _currencysCompare)
                    {
                        if (!_currencysReceive.ContainsKey(curKey.Key))
                        {
                            msgResult += curKey.Value + ",";
                        }
                    }

                    if (msgResult != "")
                        MessageBoxExService.Instance().Info(msgResult + "資料沒有傳回，請再按一次更新匯率按鈕");

                    btnRefresh.IsEnabled = true;
                    VmMainUi.IsButtonSaveEnabled = true;
                }
                else
                {
                    btnRefresh.Content = $"更新匯率({state.Counter})";
                }
            });

            Interlocked.Decrement(ref state.Counter);
        }

        private void MexReceivedDataProcess(List<MexReceivedData_30063> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    char mType = Convert.ToChar(item.type);
                    string mTime = item.time;
                    decimal mBid = item.bid / (decimal)10000;
                    decimal mAsk = item.ask / (decimal)10000;
                    decimal mPrice = item.price / (decimal)10000;

                    #region 檢查每一種匯率是否都有傳回來

                    // 這段是因為發生了mex訊息有漏傳回的現象，才要做這個檢查確保每一種type幣別都有回來
                    // 總共有9種type會傳回:2,3,4,5,6,7,8,A,G
                    // 傳回的筆數不一定(正式環境是一種type傳回2筆，總共傳回18筆。測試環境不一定)，因為後面現貨主機很多台，所以每次每種type會傳回很多筆
                    if (!_currencysReceive.ContainsKey(mType))
                    {
                        _currencysReceive.Add(mType, "");
                    }

                    #endregion 檢查每一種匯率是否都有傳回來

                    #region 找出對應的row

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

                    #region 現貨兩台主機資料檢查

                    // 因為mex後面的現貨主機有兩台，常常發生一台掛掉一台正常
                    // 導致匯率有時回正確的那台，有時回錯誤的那台
                    // 故改成兩台都會傳回來，同樣的幣別我會收到兩筆
                    // 取時間比較後面的那筆來顯示

                    if (!string.IsNullOrEmpty(findItem.EX_TIME))
                    {
                        DateTime newDateTime, originalDateTime;
                        if (DateTime.TryParse(DateTime.Now.ToDateStr() + " " + mTime, out newDateTime) && DateTime.TryParse(DateTime.Now.ToDateStr() + " " + findItem.EX_TIME, out originalDateTime))
                        {
                            if (newDateTime <= originalDateTime)
                            {
                                // 小於等於原本的日期就跳過此筆
                                continue;
                            }
                        }
                    }

                    #endregion 現貨兩台主機資料檢查

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
                                decimal GDEX_EX_RATE = 0;

                                using (var das = Factory.CreateDalSession(SettingDatabaseInfo.TfxmDay))
                                {
                                    var dGdex = new D_GDEX(das);
                                    var gdex = dGdex.GetTopOne(1, Ocf.OCF_DATE);

                                    if (gdex != null)
                                    {
                                        GDEX_EX_RATE = gdex.GDEX_EX_RATE.GetValueOrDefault();
                                    }
                                }

                                findItem.EXRT_EXCHANGE_RATE = Math.Round(GDEX_EX_RATE, 6);
                            }

                            break;

                        default:
                            break;
                    }

                    #endregion 設定一般的本公司匯率欄位

                    #region 設定特定的匯率

                    decimal fxUsdToTwd = 0;

                    // 取得美元兌台幣匯率
                    findItem = _vm.MainGridData.Where(c => c.EXRT_CURRENCY_TYPE == '2' && c.EXRT_COUNT_CURRENCY == '1').SingleOrDefault();

                    if (findItem != null)
                    {
                        fxUsdToTwd = findItem.EXRT_EXCHANGE_RATE;

                        #region 設定人民幣兌台幣匯率

                        decimal fxCnyToTwd = 0;

                        if (mType == '8')
                        {
                            // 人民幣兌台幣匯率 = 人民幣兌美元匯率(8->2)*美元兌台幣匯率(2->1)(四捨五入至小數後6位)
                            findItem = _vm.MainGridData.Where(c => c.EXRT_CURRENCY_TYPE == mType && c.EXRT_COUNT_CURRENCY == '2').SingleOrDefault();

                            if (findItem != null)
                            {
                                fxCnyToTwd = Math.Round(findItem.EXRT_EXCHANGE_RATE * fxUsdToTwd, 6);

                                findItem = _vm.MainGridData.Where(c => c.EXRT_CURRENCY_TYPE == mType && c.EXRT_COUNT_CURRENCY == '1').SingleOrDefault();

                                if (findItem == null)
                                {
                                    findItem = new UIModel_30063();
                                    findItem.EXRT_CURRENCY_TYPE = '8';
                                    findItem.EXRT_COUNT_CURRENCY = '1';
                                }

                                findItem.EXRT_EXCHANGE_RATE = fxCnyToTwd;
                                findItem.EX_TIME = DateTime.Now.ToString("HH:mm:ss");
                            }
                            else
                            {
                                MessageBoxExService.Instance().Info("找不到人民幣兌美元匯率(8->2)");
                            }
                        }

                        #endregion 設定人民幣兌台幣匯率

                        #region 設定日幣兌台幣匯率

                        decimal fxJpyToTwd = 0;

                        if (mType == '4')
                        {
                            // 日幣兌台幣匯率 = 日幣兌美元匯率(4->2) * 美元兌台幣匯率(2->1) (四捨五入至小數後6位)
                            findItem = _vm.MainGridData.Where(c => c.EXRT_CURRENCY_TYPE == mType && c.EXRT_COUNT_CURRENCY == '2').SingleOrDefault();

                            if (findItem != null)
                            {
                                fxJpyToTwd = Math.Round(findItem.EXRT_EXCHANGE_RATE * fxUsdToTwd, 6);

                                findItem = _vm.MainGridData.Where(c => c.EXRT_CURRENCY_TYPE == mType && c.EXRT_COUNT_CURRENCY == '1').SingleOrDefault();

                                if (findItem == null)
                                {
                                    findItem = new UIModel_30063();
                                    findItem.EXRT_CURRENCY_TYPE = '4';
                                    findItem.EXRT_COUNT_CURRENCY = '1';
                                }

                                findItem.EXRT_EXCHANGE_RATE = fxJpyToTwd;
                                findItem.EX_TIME = DateTime.Now.ToString("HH:mm:ss");
                            }
                            else
                            {
                                MessageBoxExService.Instance().Info("找不到日幣兌美元匯率(4->2)");
                            }
                        }

                        #endregion 設定日幣兌台幣匯率
                    }
                    else
                    {
                        MessageBoxExService.Instance().Info("找不到美元兌台幣匯率(2->1)");
                    }

                    #endregion 設定特定的匯率
                }
            }
        }

        private void BtnUpdateNight_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<EXRT> exrtsDay = null;

            using (var das = Factory.CreateDalSession())
            {
                var dExrt = new D_EXRT(das);
                exrtsDay = dExrt.ListAll();

                var changedData = new Operate<EXRT>();
                changedData.ChangedItems = exrtsDay;

                using (var dasNight = Factory.CreateDalSession(SettingDatabaseInfo.FutNight))
                {
                    var dExrtNight = new D_EXRT(dasNight);
                    dExrtNight.Save(changedData);

                    using (var dasOpposite = Factory.CreateDalSession(SettingDatabaseInfo.OptNight))
                    {
                        var dExrtOpposite = new D_EXRT(dasOpposite);
                        dExrtOpposite.Save(changedData);
                    }

                    // 發送mex訊息--期貨夜盤
                    IEagleGate eagleGate = new MexGate(MsgSysType.FutNight, "TFX.FUT.PROD.EXRT", "all");
                    EagleArgs ea = new EagleArgs();
                    ea.AddEagleContent(new EagleContent() { Item = "EXRT", Value = "send" });
                    eagleGate.AddArgument(ea);
                    eagleGate.Send();

                    // 發送mex訊息--選擇權夜盤
                    eagleGate = new MexGate(MsgSysType.OptNight, "TFX.OPT.PROD.EXRT", "all");
                    ea = new EagleArgs();
                    ea.AddEagleContent(new EagleContent() { Item = "EXRT", Value = "send" });
                    eagleGate.AddArgument(ea);
                    eagleGate.Send();

                    DbLog("異常時只更新夜盤", das);
                }
            }

            MessageBoxExService.Instance().Info("更新夜盤成功");
        }
    }
}