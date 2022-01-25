using CrossModel;
using DevExpress.Xpf.Core;
using System.Threading;
using System.Windows;
using TradeFutNight.Interfaces;
using TradeFutNight.Views;

namespace TradeFutNight.Common
{
    public class MessageBoxExService : IMessageBoxExService
    {
        private static MessageBoxExService instance;

        public MainUI_ViewModel VmMainUi { get; set; }

        // Constructor is 'protected'
        protected MessageBoxExService()
        {
        }

        public static IMessageBoxExService Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (instance == null)
            {
                instance = new MessageBoxExService();
                DXMessageBoxLocalizer.Active = new CustomeDxMessageBox();

                // DevExpress暫時性的修正跳出的視窗在點擊工具列後會跑到後面的問題，等到我們裝的DevExpress的版本等於或超過20.1.12後，就可以拿掉這段
                // 我後來發現呼叫Show函式裡面那邊加入owner的設定的話就不會有問題，就不用加入這段
                // https://supportcenter.devexpress.com/ticket/details/t981871/themedmessagebox-in-windows-forms-loses-top-most-when-clicking-on-the-taskbar-icon
                //Window.TopmostProperty.OverrideMetadata(typeof(ThemedMessageBoxWindow), new FrameworkPropertyMetadata(true));
            }

            return instance;
        }

        public MessageBoxResult Confirm(string content)
        {
            // 等待一些時間，讓前面的UI有些動作可以先執行，不然前面的某些UI動作像是Focus到Grid之類的還沒作完，會被後面的跳視窗的程式蓋掉
            Thread.Sleep(300);

            HideLoadingWindow();

            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(() =>
            {
                // 加owner防止視窗跑到主視窗後面就看不到他了
                result = ThemedMessageBox.Show(title: MessageConst.Attention, text: content, messageBoxButtons: MessageBoxButton.YesNo, icon: MessageBoxImage.Question, owner: Application.Current.MainWindow);
            });
            return result;
        }

        public MessageBoxResult Error(string content)
        {
            // 等待一些時間，讓前面的UI有些動作可以先執行，不然前面的某些UI動作像是Focus到Grid之類的還沒作完，會被後面的跳視窗的程式蓋掉
            Thread.Sleep(300);

            HideLoadingWindow();

            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(() =>
            {
                result = ThemedMessageBox.Show(title: MessageConst.Attention, text: content, messageBoxButtons: MessageBoxButton.OK, icon: MessageBoxImage.Error, owner: Application.Current.MainWindow);
            });

            return result;
        }

        public MessageBoxResult Info(string content)
        {
            // 等待一些時間，讓前面的UI有些動作可以先執行，不然前面的某些UI動作像是Focus到Grid之類的還沒作完，會被後面的跳視窗的程式蓋掉
            Thread.Sleep(300);

            HideLoadingWindow();

            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(() =>
            {
                result = ThemedMessageBox.Show(title: MessageConst.ProcessResult, text: content, messageBoxButtons: MessageBoxButton.OK, icon: MessageBoxImage.Information, owner: Application.Current.MainWindow);
            });
            return result;
        }

        private void HideLoadingWindow()
        {
            if (VmMainUi != null)
                VmMainUi.HideLoadingWindow();
        }
    }

    public class CustomeDxMessageBox : DXMessageBoxLocalizer
    {
        protected override void PopulateStringTable()
        {
            base.PopulateStringTable();
            AddString(DXMessageBoxStringId.Yes, "是");
            AddString(DXMessageBoxStringId.No, "否");
        }
    }
}