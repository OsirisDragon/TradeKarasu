using AutoMapper;
using ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix3;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30012_ViewModel : ViewModelParent<UIModel_30012>
    {
        public U_30012_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30012>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PUT, UIModel_30012>().ReverseMap();
            }));

            await Query();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30012());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30012)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                IList<UIModel_30012> list = null;

                using (var das = Factory.CreateDalSession())
                {
                    var d30012 = new D_30012<UIModel_30012>(das);
                    list = d30012.List().ToList();
                }

                MainGridData = list.AsTrackable();
            });

            await task;
        }
    }

    public class UIModel_30012 : PUT
    {
    }
}