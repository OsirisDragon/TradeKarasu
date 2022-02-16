using System.Windows;

namespace TradeFutNight.Auth
{
    public class AuthGate
    {
        public bool ShowAuthDouble(string programID)
        {
            bool? returnValue = false;

            Application.Current.Dispatcher.Invoke(() =>
            {
                returnValue = new AuthDoubleWithoutCard(programID).ShowDialog();
            });

            if (returnValue.HasValue)
            {
                return returnValue.Value;
            }

            return false;
        }
    }
}