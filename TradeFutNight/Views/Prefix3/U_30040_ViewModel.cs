using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30040_ViewModel : ViewModelParent<UIModel_30040>
    {
        public IList<ItemInfo> IdxGroupInfos
        {
            get { return GetProperty(() => IdxGroupInfos); }
            set { SetProperty(() => IdxGroupInfos, value); }
        }

        public U_30040_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30040>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPDK, UIModel_30040>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30040>().ToList().AsTrackable();

            using (var das = Factory.CreateDalSession())
            {
                var dTPPDK = new D_TPPDK(das);
                MainGridData = MapperInstance.Map<IList<UIModel_30040>>(dTPPDK.ListAll()).AsTrackable();
            }
            IdxGroupInfos = DropDownItems.TppIndexGrp();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30040());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30040)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dTPPDK = new D_TPPDK(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30040>>(dTPPDK.ListAll()).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30040 : TPPDK
    {
    }
}