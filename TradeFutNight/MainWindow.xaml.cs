using CrossModel;
using CrossModel.Enum;
using System;
using System.IO;
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

            DataBaseEngine.Initial();

            // 暫時設定在這邊
            MagicalHats.UserID = "Q0001";
            MagicalHats.UserName = "希耶斯塔";

            AppSettings.SysShortAliasID = "F";
            AppSettings.SysDayOrNightText = "(夜盤)";
            AppSettings.SystemType = SystemType.FutNight;
            AppSettings.LocalReportDirectoryWithoutDate = "C:\\future_night";
            AppSettings.LocalReportDirectory = Path.Combine(AppSettings.LocalReportDirectoryWithoutDate, MagicalHats.Ocf.OCF_DATE.ToString("yyyyMMdd"));

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
            }

            this.Content = mainUi;
        }

        //const double AdditionalCaptionOffset = 26d;
        //static Action<ThemedWindow, double> setCaptionHeight;
        //static MainWindow()
        //{
        //    WindowChrome.CaptionHeightProperty.OverrideMetadata(typeof(MainWindow), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCaptionHeightPropertyChanged)));
        //    setCaptionHeight = ReflectionHelper.CreateFieldSetter<ThemedWindow, double>(typeof(ThemedWindow), "captionHeight", BindingFlags.Instance | BindingFlags.NonPublic);
        //}
        //static void OnCaptionHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((MainWindow)d).OnCaptionHeightChanged((double)e.NewValue, (double)e.OldValue);
        //}
        //void OnCaptionHeightChanged(double eOldValue, double eNewValue) { setCaptionHeight(this, WindowChrome.GetCaptionHeight(this) + AdditionalCaptionOffset); }
    }
}