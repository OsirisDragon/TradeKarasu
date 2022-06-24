using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix8;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix8
{
    public class U_80003_ViewModel : ViewModelParent<UIModel_80003>
    {
        public XtraReport Report
        {
            get { return GetProperty(() => Report); }
            set { SetProperty(() => Report, value); }
        }

        public IEnumerable<string> ComboBoxItemsSource { get; set; }

        public U_80003_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_80003>();

            ComboBoxItemsSource = Enumerable.Range(1, 20)
                .Select(x => string.Format("Item {0}", x));
        }

        public void Open()
        {
            using (var das = Factory.CreateDalSession())
            {
                var d80003 = new D_80003<UIModel_80003>(das);
                MainGridData = d80003.GetUserAndCard("J0309").ToList();
            }
        }
    }

    [HighlightedClass]
    public class UIModel_80003 : UPF
    {
        public virtual string UPFCRD_CARD_NO { get; set; } // char(6)
    }
}