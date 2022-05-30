using CrossModel;
using CrossModel.Enum;
using LinqToDB.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using TradeFutNight.Common;
using TradeFutNight.Views;
using TradeFutNightData;

namespace TradeFutNight
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : DevExpress.Xpf.Core.ThemedWindow
    {
        private bool _isAlarmPrintFileAlreadyUsed = false;

        public MainWindow()
        {
            InitializeComponent();

            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

            // AseClient add custom charset support
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.RegisterProvider(new EncodingProviderForBig5());

            DataBaseEngine.Initial();

            // 打開linq2db的偵錯用顯示轉換的SQL的功能
            DataConnection.TurnTraceSwitchOn();
            DataConnection.WriteTraceLine = (o, c, t) => Debug.WriteLine(o, c);

            // 暫時設定在這邊
            MagicalHats.UserID = "Q0001";
            MagicalHats.UserAD = "helloWorld";
            MagicalHats.UserName = "希耶斯塔";

            AppSettings.SysShortAliasID = "F";
            AppSettings.SysDayOrNightText = "(夜盤)";
            AppSettings.SystemType = SystemType.FutNight;
            AppSettings.LocalReportDirectoryWithoutDate = "C:\\future_night";
            AppSettings.LocalRoutineDataDirectory = "C:\\RoutineData";
            AppSettings.LocalReportDirectory = Path.Combine(AppSettings.LocalReportDirectoryWithoutDate, MagicalHats.Ocf.OCF_DATE.ToString("yyyyMMdd"));
            AppSettings.DashForTitle = "–";

            if (!Directory.Exists(AppSettings.LocalReportDirectory))
            {
                Directory.CreateDirectory(AppSettings.LocalReportDirectory);
            }

            var mainUi = new MainUI();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                MagicalHats.UserID = args[1];
                MagicalHats.UserName = args[2];
                string directOpenProgramID = args[3];
                string directOpenProgramName = args[4];

                mainUi.IsExeCallMode = true;
                mainUi.OpenProgram(directOpenProgramID, directOpenProgramName).GetAwaiter();
                mainUi.CloseLeftPanel();
                this.WindowState = System.Windows.WindowState.Normal;
                this.Title = "期貨夜盤" + "-" + directOpenProgramID + "-" + directOpenProgramName;
                //this.Topmost = true;
                //this.WindowStyle = System.Windows.WindowStyle.ToolWindow;
                //this.ShowInTaskbar = false;
            }

            this.Content = mainUi;
        }

        private void ThemedWindow_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxExService.Instance().Error(e.Exception.Message);
            e.Handled = true;
        }

        /// <summary>
        /// 這個事件可以抓到很多平常抓不到的事件，但是也會抓到很多奇怪的事件，而且會重複抓到
        /// </summary>
        private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Console.WriteLine("FirstChanceException event raised in {0}: {1}",
             AppDomain.CurrentDomain.FriendlyName, e.Exception.Message);

            // 目前發現，如果列印到虛擬印表機時，儲存的虛擬印表PDF檔案如果已經被開啟的話，就會出現這個錯誤「An error occurred during printing a document」
            // 這個錯誤在列印那邊的try catch是抓不到的，只有在這邊抓的到
            if (e.Exception.TargetSite.Name == "OnStartPrint")
            {
                if (e.Exception.Message.Contains("程序無法存取檔案，因為檔案正由另一個程序使用") && !_isAlarmPrintFileAlreadyUsed)
                {
                    // 因為這個事件會觸發兩次，但只要跳一次就好
                    _isAlarmPrintFileAlreadyUsed = true;
                    MessageBoxExService.Instance().Error(e.Exception.Message);
                }
            }
        }
    }

    /// <summary>
    /// 加入不同語系
    /// https://github.com/DataAction/AdoNetCore.AseClient/wiki/Add-custom-charset-support
    /// </summary>
    internal class EncodingProviderForBig5 : EncodingProvider
    {
        public override Encoding GetEncoding(int codepage)
        {
            return null; // we're only matching on name, not codepage
        }

        public override Encoding GetEncoding(string name)
        {
            // 這裡不判斷name是不是cp950，因為Sybase的server回傳的name會是ISO-8859-1，我覺得應該是資料庫的人設定錯誤的關係
            return Encoding.GetEncoding(950);
        }
    }
}