using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using Eagle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradeFutNight.Auth;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.PrefixA
{
    /// <summary>
    /// U_A9920.xaml 的互動邏輯
    /// </summary>
    public partial class U_A9920 : UserControlParent, IViewSword
    {
        private U_A9920_ViewModel _vm;

        public U_A9920()
        {
            InitializeComponent();
            _vm = (U_A9920_ViewModel)DataContext;
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

            if (!MagicalHats.CheckMsgServerConnection()) CloseWindow();
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
                    MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
                    return false;
                }

                var resultItem = new ResultItem();

                var countIsChecked = _vm.MainGridData.Count(c => c.IsChecked == true);
                if (countIsChecked == 0)
                {
                    MessageBoxExService.Instance().Error("請勾選契約代碼");
                    return false;
                }

                if (_vm.IsReadOnlyTPPADJ_M_PRICE_LIMIT && _vm.IsReadOnlyTPPADJ_M_PRICE_LIMIT_F && _vm.IsReadOnlyTPPADJ_M_INTERVAL && _vm.IsReadOnlyTPPADJ_ACCU_QNTY
                && _vm.IsReadOnlyTPPADJ_M_PRICE_FILTER && _vm.IsReadOnlyTPPADJ_BS_PRICE_FILTER && _vm.IsReadOnlyTPPADJ_THERICAL_P_REF && _vm.IsReadOnlyTPPADJ_SPREAD)
                {
                    MessageBoxExService.Instance().Error("請勾選要更改的欄位");
                    return false;
                }

                // 需第2組帳號及密碼覆核
                // 如果是早上07:30到08:30，就不用第二人，因為那時太早可能只有一人值班
                // 如果不是這段時間，就要雙重驗證
                if (!(DateTime.Now.TimeOfDay >= new TimeSpan(7, 30, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(8, 30, 0)))
                {
                    if (!new AuthGate().ShowAuthDouble(ProgramID))
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
                var mainData = _vm.MainGridData.Where(c => c.IsChecked == true).ToList();

                // 先列印確認表
                var report = CreateReport(mainData, OperationType.Confirm);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                if (MessageBoxExService.Instance().Confirm("請確認資料無誤後，再按是繼續執行") != MessageBoxResult.Yes)
                    return;

                // 將沒有勾選的欄位的值設為Null
                foreach (var item in mainData)
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (_vm.IsReadOnlyTPPADJ_M_PRICE_LIMIT) item.TPPADJ_M_PRICE_LIMIT = null;
                        if (_vm.IsReadOnlyTPPADJ_M_PRICE_LIMIT_F) item.TPPADJ_M_PRICE_LIMIT_F = null;
                        if (_vm.IsReadOnlyTPPADJ_M_INTERVAL) item.TPPADJ_M_INTERVAL = null;
                        if (_vm.IsReadOnlyTPPADJ_ACCU_QNTY) item.TPPADJ_ACCU_QNTY = null;
                        if (_vm.IsReadOnlyTPPADJ_M_PRICE_FILTER) item.TPPADJ_M_PRICE_FILTER = null;
                        if (_vm.IsReadOnlyTPPADJ_BS_PRICE_FILTER) item.TPPADJ_BS_PRICE_FILTER = null;
                        if (_vm.IsReadOnlyTPPADJ_THERICAL_P_REF) item.TPPADJ_THERICAL_P_REF = null;
                        if (_vm.IsReadOnlyTPPADJ_SPREAD) item.TPPADJ_SPREAD = null;

                        item.TPPADJ_USER_ID = UserID;
                        item.TPPADJ_W_TIME = DateTime.Now;
                    });
                }

                if (!MagicalHats.CheckMsgServerConnection()) return;

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var operate = GetChanges<UIModel_A9920, TPPADJ>(_vm.MainGridData, _vm);

                        var dTPPADJ = new D_TPPADJ(das);
                        dTPPADJ.Insert(operate.ChangedItems);

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

                SendMsgToServer(mainData);

                report = CreateReport(mainData, OperationType.Save);
                reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private void SendMsgToServer(List<UIModel_A9920> mainData)
        {
            string subject = "";
            if (AppSettings.SystemType == SystemType.FutDay || AppSettings.SystemType == SystemType.FutNight)
                subject = "TFX.FUT.PROD.PDKBREAK.OP";
            else if (AppSettings.SystemType == SystemType.OptDay || AppSettings.SystemType == SystemType.OptNight)
                subject = "TFX.OPT.PROD.PDKBREAK.OP";

            IEagleGate eagleGate = new MexGate(MsgSysType.FutNight, subject, "all");

            // 每一筆都要發送mex訊息
            foreach (var item in mainData)
            {
                string nodeMsgType = "9";
                string nodeStatus = "0";
                string nodeOrderResumeTime = "";
                var nodeMatchResumeTime = new Dictionary<string, string>();

                // 若為選擇權就填月份，其他填0
                if (AppSettings.SystemType == SystemType.FutDay || AppSettings.SystemType == SystemType.FutNight)
                    nodeOrderResumeTime = "0";
                else if (AppSettings.SystemType == SystemType.OptDay || AppSettings.SystemType == SystemType.OptNight)
                    nodeOrderResumeTime = item.TPPADJ_SETTLE_DATE;

                // 組成各項參數的JSON
                if (!_vm.IsReadOnlyTPPADJ_M_PRICE_LIMIT && item.TPPADJ_M_PRICE_LIMIT.HasValue) nodeMatchResumeTime.Add("TPPINTD_M_PRICE_LIMIT", item.TPPADJ_M_PRICE_LIMIT.ToString().Trim());
                if (!_vm.IsReadOnlyTPPADJ_M_PRICE_LIMIT_F && item.TPPADJ_M_PRICE_LIMIT_F.HasValue) nodeMatchResumeTime.Add("TPPINTD_M_PRICE_LIMIT_F", item.TPPADJ_M_PRICE_LIMIT_F.ToString().Trim());
                if (!_vm.IsReadOnlyTPPADJ_M_INTERVAL && item.TPPADJ_M_INTERVAL.HasValue) nodeMatchResumeTime.Add("TPPINTD_M_INTERVAL", item.TPPADJ_M_INTERVAL.ToString().Trim());
                if (!_vm.IsReadOnlyTPPADJ_ACCU_QNTY && item.TPPADJ_ACCU_QNTY.HasValue) nodeMatchResumeTime.Add("TPPINTD_ACCU_QNTY", item.TPPADJ_ACCU_QNTY.ToString().Trim());
                if (!_vm.IsReadOnlyTPPADJ_M_PRICE_FILTER && item.TPPADJ_M_PRICE_FILTER.HasValue) nodeMatchResumeTime.Add("TPPINTD_M_PRICE_FILTER", item.TPPADJ_M_PRICE_FILTER.ToString().Trim());
                if (!_vm.IsReadOnlyTPPADJ_BS_PRICE_FILTER && item.TPPADJ_BS_PRICE_FILTER.HasValue) nodeMatchResumeTime.Add("TPPINTD_BS_PRICE_FILTER", item.TPPADJ_BS_PRICE_FILTER.ToString().Trim());
                if (!_vm.IsReadOnlyTPPADJ_THERICAL_P_REF && item.TPPADJ_THERICAL_P_REF.HasValue) nodeMatchResumeTime.Add("TPPBP_THERICAL_P_REF", item.TPPADJ_THERICAL_P_REF.ToString().Trim());
                if (!_vm.IsReadOnlyTPPADJ_SPREAD && item.TPPADJ_SPREAD.HasValue) nodeMatchResumeTime.Add("SPREAD_PRICE", item.TPPADJ_SPREAD.ToString().Trim());

                EagleArgs ea = new EagleArgs();

                ea.AddEagleContent(new EagleContent() { Item = "TYPE", Value = item.TPPADJ_TYPE });
                ea.AddEagleContent(new EagleContent() { Item = "PROD_ID", Value = item.TPPADJ_PROD_ID });
                ea.AddEagleContent(new EagleContent() { Item = "MSG_TYPE", Value = nodeMsgType });
                ea.AddEagleContent(new EagleContent() { Item = "STATUS", Value = nodeStatus });

                // 這個是預計生效日期，空值就是立即生效
                ea.AddEagleContent(new EagleContent() { Item = "TRADE_DATE", Value = "" });

                // 雖然這個欄位是TRADE_PAUSE_TIME，但後端伺服器MEX制定人說要傳生效時間這個欄位，空值就是立即生效
                ea.AddEagleContent(new EagleContent() { Item = "TRADE_PAUSE_TIME", Value = "" });

                // 雖然這個欄位是ORDER_RESUME_TIME，但後端伺服器MEX制定人說如果tpphalt_type為M(目前只有選擇權會有M)的話，要傳月份這個欄位
                ea.AddEagleContent(new EagleContent() { Item = "ORDER_RESUME_TIME", Value = nodeOrderResumeTime });

                // 雖然這個欄位是MATCH_RESUME_TIME，但後端伺服器MEX制定人說要傳參數的JSON
                // 且這裡的JSON的值的部分，全部都要用雙引號包起來，就算是數字也一樣，這是因為後端伺服器MEX制定人他們要這樣
                // 但因為這裡是用string的dictionart產生JSON，所以都會有雙引號，另外再透過呼叫function把雙引號改成3個雙引號，傳入到exe的參數才會留者這個雙引號
                ea.AddEagleContent(new EagleContent() { Item = "MATCH_RESUME_TIME", Value = JsonHelper.ToJsonStringWithQuoteForExeArguments(nodeMatchResumeTime) });

                eagleGate.AddArgument(ea);
            }

            eagleGate.Send();

            DbLog(MessageConst.SendMsg + ":" + eagleGate.Subject);
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string memo = "";
            string reportTitle = ProgramID + "–" + ProgramName;

            switch (operationType)
            {
                case OperationType.Confirm:
                    reportTitle += "–" + "確認表";
                    break;

                case OperationType.Save:

                    break;

                default:
                    break;
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, memo, Ocf.OCF_DATE, true, true, true);
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

        private void ComboCategorys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selectedItem = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            comboDetails.Items.Clear();

            IEnumerable<PDK> pdks;
            using (var das = Factory.CreateDalSession())
            {
                pdks = new D_PDK(das).ListNotExpire();
            }

            if (selectedItem == "全部")
            {
            }
            else if (selectedItem == "上市指數")
            {
                pdks = pdks.Where(c => c.PDK_SUBTYPE == 'I' && c.PDK_UNDERLYING_MARKET == '1');
            }
            else if (selectedItem == "上櫃指數")
            {
                pdks = pdks.Where(c => c.PDK_SUBTYPE == 'I' && c.PDK_UNDERLYING_MARKET == '2');
            }
            else if (selectedItem == "國外指數")
            {
                pdks = pdks.Where(c => c.PDK_SUBTYPE == 'I' && (c.PDK_UNDERLYING_MARKET == '3' || c.PDK_UNDERLYING_MARKET == '5'));
            }
            else if (selectedItem == "匯率")
            {
                pdks = pdks.Where(c => c.PDK_SUBTYPE == 'E');
            }
            else if (selectedItem == "ETF")
            {
                pdks = pdks.Where(c => c.PDK_PARAM_KEY == "ETF");
            }

            comboDetails.Items.Add("全部");

            if (selectedItem != "全部")
            {
                foreach (var item in pdks)
                {
                    comboDetails.Items.Add(item.PDK_KIND_ID);
                }
            }

            comboDetails.Text = "全部";
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckHelper(true);
        }

        private void BtnUnCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckHelper(false);
        }

        private void CheckHelper(bool isCheck)
        {
            // 勾或不勾
            string selectedItemCategory, selectedItemDetail;

            selectedItemCategory = ((ComboBoxItem)comboCategorys.SelectedItem).Content.ToString();
            selectedItemDetail = comboDetails.SelectedItem.ToString();

            // 如果第二層選全部，就把第二層的每一個商品組成字串
            if (selectedItemDetail == "全部")
            {
                foreach (var item in comboDetails.Items)
                {
                    selectedItemDetail += item.ToString();
                }
            }

            bool hasAlreadyScroll = false;

            foreach (var item in _vm.MainGridData)
            {
                // 如果第一層選全部就是全部都要勾或不勾
                // 如果選其他的就判斷PROD_ID有沒有符合
                if (selectedItemCategory == "全部" || selectedItemDetail.IndexOf(item.TPPADJ_PROD_ID.Substring(0, 3)) >= 0)
                {
                    item.IsChecked = isCheck;

                    // 把卷軸捲到那筆去，如果有多筆只要捲到第一筆就好，不然每一筆都捲速度太慢
                    if (!hasAlreadyScroll)
                    {
                        _vm.FocusRow(item);
                        hasAlreadyScroll = true;
                    }
                }
            }

            MessageBoxExService.Instance().Info("OK");
        }
    }
}