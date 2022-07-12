using System.Windows;

namespace CrossModel.Interfaces
{
    public interface IMessageBoxExService
    {
        MessageBoxResult Confirm(string content, MessageBoxResult defaultButton = MessageBoxResult.No);

        MessageBoxResult Error(string content);

        MessageBoxResult Info(string content);
    }
}