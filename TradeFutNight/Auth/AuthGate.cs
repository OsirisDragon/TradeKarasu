using System.Windows;

namespace TradeFutNight.Auth
{
    public class AuthGate
    {
        public bool ShowAuthDouble()
        {
            bool? returnValue = false;

            Application.Current.Dispatcher.Invoke(() =>
            {
                returnValue = new AuthDoubleWithoutCard().ShowDialog();
            });

            if (returnValue.HasValue)
            {
                return returnValue.Value;
            }

            return false;
        }
    }
}