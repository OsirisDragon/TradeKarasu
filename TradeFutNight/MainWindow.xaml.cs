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
        public MainWindow()
        {
            InitializeComponent();

            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;

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