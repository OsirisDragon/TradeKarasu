using System.Collections.ObjectModel;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixB
{
    public class U_BN037_ViewModel : ViewModelParent<UIModel_BN037>
    {
        public U_BN037_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_BN037>();
        }

        public void Open()
        {
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_BN037());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_BN037)item);
        }
    }

    public class UIModel_BN037 : DEFAULT
    {
    }
}