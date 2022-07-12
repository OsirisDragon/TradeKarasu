using DevExpress.Xpf.Core;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using static DevExpress.Utils.Serializing.PrintingSystemXmlSerializer;

namespace TradeOptNight
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var custompalette = new ThemePalette("CustomPalette");
            custompalette.SetColor("Backstage.Window.Background", Colors.DimGray);
            custompalette.SetColor("Window.Background", Colors.White);

            var palettetheme = Theme.CreateTheme(custompalette, Theme.Office2019Colorful);
            //var palettetheme = Theme.CreateTheme(custompalette, Theme.Office2019White);

            Theme.RegisterTheme(palettetheme);
            ApplicationThemeHelper.ApplicationThemeName = palettetheme.Name;
        }
    }
}