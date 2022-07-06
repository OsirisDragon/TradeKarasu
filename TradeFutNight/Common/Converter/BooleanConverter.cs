using System;
using System.Globalization;
using System.Windows.Data;

namespace TradeFutNight.Common.Converter
{
    internal class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(char))
                {
                    return value.ToString() == "Y" ? true : false;
                }
                else if (value.GetType() == typeof(bool))
                {
                    return (bool)value ? 'Y' : ' ';
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool)value;
            return v ? 'Y' : ' ';
        }
    }
}