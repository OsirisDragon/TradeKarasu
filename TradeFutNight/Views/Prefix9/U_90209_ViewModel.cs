using AutoMapper;
using ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix9;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix9
{
    public class U_90209_ViewModel : ViewModelParent<UIModel_90209>
    {
        public U_90209_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_90209>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BRK, UIModel_90209>().ReverseMap();
            }));
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_90209());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_90209)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d90209 = new D_90209<UIModel_90209>(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_90209>>(d90209.List()).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_90209
    {
        public virtual string BROKER_ID { get; set; }

        public virtual int XTCS_COUNT { get; set; }

        public virtual string SYS_TYPE { get; set; }
    }
}