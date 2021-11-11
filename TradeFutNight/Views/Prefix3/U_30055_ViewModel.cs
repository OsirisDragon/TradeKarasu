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
    public class U_30055_ViewModel : ViewModelParent<UIModel_30055>
    {
        public IList<ItemInfo> KindIdTypeInfos
        {
            get { return GetProperty(() => KindIdTypeInfos); }
            set { SetProperty(() => KindIdTypeInfos, value); }
        }

        public U_30055_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30055>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MORD, UIModel_30055>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30055>().ToList().AsTrackable();

            using (var das = Factory.CreateDalSession())
            {
                var dMORD = new D_MORD(das);
                MainGridData = MapperInstance.Map<IList<UIModel_30055>>(dMORD.ListAll()).AsTrackable();
            }
            KindIdTypeInfos = DropDownItems.MordKindIdType();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30055());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30055)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dMORD = new D_MORD(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30055>>(dMORD.ListAll()).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30055 : MORD
    {
    }
}