using AutoMapper;
using ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixS
{
    public class U_S0090_ViewModel : ViewModelParent<UIModel_S0090>
    {
        public U_S0090_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_S0090>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ZWASH, UIModel_S0090>().ReverseMap();
            }));

            await Query();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_S0090());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_S0090)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dZWASH = new D_ZWASH(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_S0090>>(dZWASH.ListAll()).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_S0090 : ZWASH
    {
    }
}