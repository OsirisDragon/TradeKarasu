namespace TradeFutNight.Common
{
    public class Utility
    {
        public static bool IsValidTick(decimal? inputValue, decimal unit)
        {
            bool result = false;

            if (inputValue % unit == 0)
                result = true;

            return result;
        }
    }
}