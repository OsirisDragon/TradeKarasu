using CrossModel;
using DevExpress.Xpf.Core;
using System.Windows;
using TradeFutNight.Interfaces;

namespace TradeFutNight.Common
{
    public class MessageBoxExService : IMessageBoxExService
    {
        private static MessageBoxExService instance;

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

                // DevExpress暫時性的修正，等到我們裝的DevExpress的版本等於或超過20.1.12後，就可以拿掉這段
                // https://supportcenter.devexpress.com/ticket/details/t981871/themedmessagebox-in-windows-forms-loses-top-most-when-clicking-on-the-taskbar-icon
                Window.TopmostProperty.OverrideMetadata(typeof(ThemedMessageBoxWindow), new FrameworkPropertyMetadata(true));
            }

            return instance;
        }

        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(() => { result = ThemedMessageBox.Show(caption, messageBoxText, button, icon); });
            return result;
        }

        public MessageBoxResult Confirm(string content)
        {
            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(() => { result = ThemedMessageBox.Show(title: MessageConst.Attention, text: content, messageBoxButtons: MessageBoxButton.YesNo, image: MessageBoxImage.Question); });
            return result;
        }

        public MessageBoxResult Error(string content)
        {
            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(() => { result = ThemedMessageBox.Show(MessageConst.Attention, content, MessageBoxButton.OK, MessageBoxImage.Error); });
            return result;
        }

        public MessageBoxResult Info(string content)
        {
            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(() => { result = ThemedMessageBox.Show(MessageConst.ProcessResult, content, MessageBoxButton.OK, MessageBoxImage.Information); });
            return result;
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