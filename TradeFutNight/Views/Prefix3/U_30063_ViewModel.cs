using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30063_ViewModel : ViewModelParent<UIModel_30063>
    {
        public IList<ItemInfo> ExrtCurrencyType
        {
            get { return GetProperty(() => ExrtCurrencyType); }
            set { SetProperty(() => ExrtCurrencyType, value); }
        }

        public U_30063_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30063>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EXRT, UIModel_30063>().ReverseMap();
            }));

            await Query();

            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "臺幣", Value = '1' });
            result.Add(new ItemInfo() { Text = "美元", Value = '2' });
            result.Add(new ItemInfo() { Text = "歐元", Value = '3' });
            result.Add(new ItemInfo() { Text = "日幣", Value = '4' });
            result.Add(new ItemInfo() { Text = "英鎊", Value = '5' });
            result.Add(new ItemInfo() { Text = "澳幣", Value = '6' });
            result.Add(new ItemInfo() { Text = "港幣", Value = '7' });
            result.Add(new ItemInfo() { Text = "人民幣", Value = '8' });
            result.Add(new ItemInfo() { Text = "南非幣", Value = 'A' });
            result.Add(new ItemInfo() { Text = "紐幣", Value = 'G' });

            ExrtCurrencyType = result;
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30063());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30063)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dExrt = new D_EXRT(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_30063>>(dExrt.ListAll()).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_30063 : EXRT
    {
        public virtual decimal EX_OK { get; set; }
        public virtual decimal EX_BID { get; set; }
        public virtual decimal EX_ASK { get; set; }
        public virtual string EX_TIME { get; set; }
        public virtual decimal EX_MID { get; set; }
    }
}