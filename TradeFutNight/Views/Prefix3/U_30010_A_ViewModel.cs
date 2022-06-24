using AutoMapper;
using ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30010_A_ViewModel : ViewModelParent<UIModel_30010_A>
    {
        public U_30010_A_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30010_A>();
        }

        public void Open()
        {
            MainGridData = new ObservableCollection<UIModel_30010_A>().ToList().AsTrackable();

            using (var das = Factory.CreateDalSession())
            {
                MapperInstance = new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SDI, UIModel_30010_A>().ReverseMap();
                }));

                var dSDI = new D_SDI(das);
                MainGridData = MapperInstance.Map<IList<UIModel_30010_A>>(dSDI.ListByDate(MagicalHats.Ocf.OCF_DATE)).AsTrackable();
            }
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30010_A());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30010_A)item);
        }
    }

    public class UIModel_30010_A : SDI
    {
    }
}