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
    public class U_30022_ViewModel : ViewModelParent<UIModel_30022>
    {
        public U_30022_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30022>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PMF, UIModel_30022>().ReverseMap();
            }));

            await Query();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30022());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30022)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                IList<UIModel_30022> list = null;

                using (var das = Factory.CreateDalSession())
                {
                    var d30022 = new D_30022<UIModel_30022>(das);
                    list = d30022.List().ToList();
                }

                MainGridData = list.AsTrackable();
            });

            await task;
        }
    }

    public class UIModel_30022 : PMF
    {
    }
}