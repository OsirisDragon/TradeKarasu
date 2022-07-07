using AutoMapper;
using ChangeTracking;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30041_ViewModel : ViewModelParent<UIModel_30041>
    {
        public U_30041_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30041>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPST, UIModel_30041>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30041>().ToList().AsTrackable();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30041()
            {
                TPPST_INDEX_ID = "",
                TPPST_UNDERLYING_MARKET = ' '
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30041)item);
        }
    }

    public class UIModel_30041 : TPPST
    {
    }
}