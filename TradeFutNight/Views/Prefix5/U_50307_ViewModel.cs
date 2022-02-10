using AutoMapper;
using ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix5;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50307_ViewModel : ViewModelParent<UIModel_50307>
    {
        public U_50307_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50307>();
        }

        public async void Open()
        {
            await Query();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_50307());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_50307)item);
        }

        public async Task Query()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PROD, UIModel_50307>().ReverseMap();
            }));

            var task = Task.Run(() =>
            {
                IList<UIModel_50307> list = null;

                using (var das = Factory.CreateDalSession())
                {
                    var d50307 = new D_50307<UIModel_50307>(das);
                    list = d50307.List().ToList();
                }

                MainGridData = list.AsTrackable();
            });

            await task;
        }
    }

    public class UIModel_50307 : PROD
    {
    }
}