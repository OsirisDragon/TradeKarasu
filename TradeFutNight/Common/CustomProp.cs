using System;
using System.Windows;

namespace TradeFutNight.Common
{
    public static class CustomProp
    {
        public static readonly DependencyProperty NotNullNotEmptyProperty = DependencyProperty.RegisterAttached("NotNullNotEmpty",
            typeof(bool), typeof(CustomProp), new FrameworkPropertyMetadata(null));

        public static bool GetNotNullNotEmpty(ContentElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            return (bool)element.GetValue(NotNullNotEmptyProperty);
        }

        public static void SetNotNullNotEmpty(ContentElement element, bool value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            element.SetValue(NotNullNotEmptyProperty, value);
        }
    }
}