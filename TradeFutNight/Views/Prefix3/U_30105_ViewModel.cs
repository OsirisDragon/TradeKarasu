using AutoMapper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix3;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30105_ViewModel : ViewModelParent<UIModel_30105>
    {
        public U_30105_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30105>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MPD, UIModel_30105>().ReverseMap();
            }));
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30105());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30105)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d30105 = new D_30105<UIModel_30105>(das);
                    MainGridData = d30105.List().ToList();
                }
            });

            await task;
        }
    }

    public class UIModel_30105 : MPD
    {
        public virtual string PDK_NAME { get; set; }
        public virtual string FCM_NAME { get; set; }
        public virtual string PROD_DISPLAY { get; set; }
        public virtual string FCM_DISPLAY { get; set; }
    }
}