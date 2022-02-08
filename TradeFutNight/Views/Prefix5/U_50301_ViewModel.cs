using AutoMapper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix5;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50301_ViewModel : ViewModelParent<UIModel_50301>
    {
        public U_50301_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50301>();
        }

        public async void Open()
        {
            await Query();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_50301());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_50301)item);
        }

        public async Task Query()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FMIF, UIModel_50301>().ReverseMap();
            }));

            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d50301 = new D_50301<UIModel_50301>(das);
                    MainGridData = d50301.List().ToList();
                }
            });

            await task;
        }
    }

    public class UIModel_50301 : FMIF
    {
        public virtual decimal CLSPRC_SETTLE_PRICE { get; set; }
        public virtual string PROD_SETTLE_DATE { get; set; }
        public virtual string PROD_PC_CODE { get; set; }
        public virtual decimal PROD_STRIKE_PRICE { get; set; }
        public virtual string PROD_ID_OUT { get; set; }
        public virtual decimal BMIF_M_QNTY_TAL { get; set; }
    }
}