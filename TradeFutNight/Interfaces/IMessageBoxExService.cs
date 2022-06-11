using System.Windows;
using TradeFutNight.Views;

namespace TradeFutNight.Interfaces
{
    public interface IMessageBoxExService
    {
        MainUI_ViewModel VmMainUi { get; set; }

        MessageBoxResult Confirm(string content, MessageBoxResult defaultButton = MessageBoxResult.No);

        MessageBoxResult Error(string content);

        MessageBoxResult Info(string content);
    }
}