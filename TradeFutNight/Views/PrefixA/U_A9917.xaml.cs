using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using Eagle;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeUtility;

namespace TradeFutNight.Views.PrefixA
{
    /// <summary>
    /// U_A9917.xaml 的互動邏輯
    /// </summary>
    public partial class U_A9917 : UserControlParent, IViewSword
    {
        private U_A9917_ViewModel _vm;

        public U_A9917()
        {
            InitializeComponent();
            _vm = (U_A9917_ViewModel)DataContext;
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
        }

        public async Task Open()
        {
            ControlSetting();
            DateTime now = DateTime.Now;

            //起始日期時間(當日14:50:00)
            _vm.StartDateTime = new DateTime(now.Year, now.Month, now.Day, 14, 50, 0);

            //結束日期時間(當下時間)
            _vm.EndDateTime = now;

            var task = Task.Run(() =>
            {
                _vm.Open();
                DbLog(MessageConst.Open);
            });
            await task;
        }

        public void Insert()
        {
        }

        public void Delete()
        {
        }

        public async Task<bool> CheckField()
        {
            var task = Task.Run(() => true);
            await task;

            return task.Result;
        }

        public async Task Save()
        {
            var task = Task.Run(() => { });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            return null;
        }

        public async Task Export()
        {
            var task = Task.Run(() => { });
            await task;
        }

        public async Task Print()
        {
            var task = Task.Run(() => { });
            await task;
        }

        public async Task PrintIndex()
        {
            var task = Task.Run(() => { });
            await task;
        }

        public async Task PrintStock()
        {
            var task = Task.Run(() => { });
            await task;
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            var button = ((Button)sender);
            button.IsEnabled = false;

            if (cbKindIdTwoChar.SelectedItem == null)
            {
                MessageBoxExService.Instance().Error("請選擇商品");
                button.IsEnabled = true;
                return;
            }

            if (!CheckCanDoQuery())
            {
                if (MessageBoxExService.Instance().Confirm("上一次的查詢尚未處理完成，是否再次查詢?") == MessageBoxResult.No)
                {
                    button.IsEnabled = true;
                    return;
                }
            }

            SendMsgToServer();

            button.IsEnabled = true;
        }

        /// <summary>
        /// 發送mex
        /// </summary>
        private void SendMsgToServer()
        {
            IEagleGate eagleGate = new MexGate(MsgSysType.FutNight, "TFX.FUT.TPRICE.QRY", "all");

            string tranTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            DbLog("UI_QUERY@" + tranTime);

            EagleArgs ea = new EagleArgs();

            ea.AddEagleContent(new EagleContent() { Item = "PDK", Value = ((ItemInfo)cbKindIdTwoChar.SelectedItem).Value.ToString() });
            ea.AddEagleContent(new EagleContent() { Item = "HMSFROM", Value = _vm.StartDateTime.ToString("yyyyMMddHHmmss") });
            ea.AddEagleContent(new EagleContent() { Item = "HMSTO", Value = _vm.EndDateTime.ToString("yyyyMMddHHmmss") });
            ea.AddEagleContent(new EagleContent() { Item = "TRAN_TIME", Value = tranTime });

            eagleGate.AddArgument(ea);

            eagleGate.Send();

            DbLog(MessageConst.SendMsg + ":" + eagleGate.Subject);

            MessageBoxExService.Instance().Info("查詢發送成功，請1分鐘後至OA環境檢視檔案");
        }

        /// <summary>
        ///檢查LOGF裡面的執行狀態
        ///確認Server端是否處理好上次的mex訊息
        /// </summary>
        /// <returns></returns>
        private bool CheckCanDoQuery()
        {
            string lastUiQuery = "";
            string lastServerQuery = "";
            using (var das = Factory.CreateDalSession())
            {
                var dLOGF = new D_LOGF(das);
                lastUiQuery = dLOGF.GetKeyData(ProgramID, "UI_QUERY%");

                //沒有的話代表第一次上線時什麼都還沒有的狀態
                if (string.IsNullOrEmpty(lastUiQuery))
                {
                    return true;
                }
                else
                {
                    //抓取LOGF裡面的記號，格式為yyyyMMddHHmmss
                    string confirmToken = lastUiQuery.Split('@')[1].ToString().Left(14);

                    string serverOkKeyData = $"SERVER_OK@{confirmToken}%";
                    string serverFailKeyData = $"SERVER_FAIL@{confirmToken}%";
                    //找尋SERVER_OK/SERVER_FAIL記號看後端處理好了沒
                    lastServerQuery = dLOGF.GetKeyData(ProgramID, serverOkKeyData, serverFailKeyData);

                    return !string.IsNullOrEmpty(lastServerQuery);
                }
            }
        }
    }
}