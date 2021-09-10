using System;
using System.Windows;

namespace TradeFutNight.Common
{
    public static class CustomProp
    {
        public static readonly DependencyProperty NotNullNotEmptyProperty = DependencyProperty.RegisterAttached("NotNullNotEmpty",
            typeof(bool), typeof(CustomProp), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ModifyMarkStyleProperty = DependencyProperty.RegisterAttached("ModifyMarkStyle",
            typeof(string), typeof(CustomProp), new FrameworkPropertyMetadata(null));

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

        public static string GetModifyMarkStyle(ContentElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            return (string)element.GetValue(ModifyMarkStyleProperty);
        }

        public static void SetModifyMarkStyle(ContentElement element, string value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            element.SetValue(ModifyMarkStyleProperty, value);
        }
    }
}