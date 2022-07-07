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
    public class U_30010_B_ViewModel : ViewModelParent<UIModel_30010_B>
    {
        public U_30010_B_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30010_B>();
        }

        public void Open()
        {
            MainGridData = new ObservableCollection<UIModel_30010_B>().ToList().AsTrackable();

            using (var das = Factory.CreateDalSession())
            {
                MapperInstance = new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CADJ, UIModel_30010_B>().ReverseMap();
                }));

                var dCADJ = new D_CADJ(das);
                MainGridData = MapperInstance.Map<IList<UIModel_30010_B>>(dCADJ.ListByDate(MagicalHats.Ocf.OCF_DATE)).AsTrackable();
            }
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30010_B());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30010_B)item);
        }
    }

    public class UIModel_30010_B : CADJ
    {
    }
}