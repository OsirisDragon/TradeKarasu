using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TradeFutNight.Interfaces
{
    public interface IMessageBoxExService
    {
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);

        MessageBoxResult Confirm(string content);

        MessageBoxResult Error(string content);

        MessageBoxResult Info(string content);
    }
}