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
            Application.Current.Dispatcher.Invoke(() => { result = ThemedMessageBox.Show(MessageConst.Attention, content, MessageBoxButton.YesNo, MessageBoxImage.Question); });
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