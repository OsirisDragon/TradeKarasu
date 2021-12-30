using AutoMapper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix5;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50304_ViewModel : ViewModelParent<UIModel_50304>
    {
        public U_50304_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50304>();
        }

        public async void Open()
        {
            await Query();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_50304());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_50304)item);
        }

        public async Task Query()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MWPLMT, UIModel_50304>().ReverseMap();
            }));

            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d50304 = new D_50304<UIModel_50304>(das);
                    MainGridData = d50304.List().ToList();
                }
            });

            await task;
        }
    }

    public class UIModel_50304 : MPR
    {
        public virtual string PROD_ID_OUT { get; set; }
        public virtual string PROD_SETTLE_DATE { get; set; }
    }
}