using AutoMapper;
using ChangeTracking;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30024_ViewModel : ViewModelParent<UIModel_30024>
    {
        public U_30024_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30024>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPINTD, UIModel_30024>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30024>().ToList().AsTrackable();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30024()
            {
                TPPINTD_FIRST_KIND_ID = "",
                TPPINTD_SECOND_KIND_ID = "",
                TPPINTD_KIND_ID_FUT = ""
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30024)item);
        }
    }

    public class UIModel_30024 : TPPINTD
    {
    }
}