using System.Windows;

namespace TradeFutNight.Interfaces
{
    public interface IMessageBoxExService
    {
        MessageBoxResult Confirm(string content);

        MessageBoxResult Error(string content);

        MessageBoxResult Info(string content);
    }
}