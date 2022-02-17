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

        public IList<ItemInfo> ExrtCountCurrency
        {
            get { return GetProperty(() => ExrtCountCurrency); }
            set { SetProperty(() => ExrtCountCurrency, value); }
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
            result.Add(new ItemInfo() { Text = "星期日", Value = '0' });
            result.Add(new ItemInfo() { Text = "星期一", Value = '1' });
            result.Add(new ItemInfo() { Text = "星期二", Value = '2' });
            result.Add(new ItemInfo() { Text = "星期三", Value = '3' });
            result.Add(new ItemInfo() { Text = "星期四", Value = '4' });
            result.Add(new ItemInfo() { Text = "星期五", Value = '5' });
            result.Add(new ItemInfo() { Text = "星期六", Value = '6' });

            ExrtCurrencyType = result;
            //TPPINTDSecondKindId = DropDownItems.TppintdSecondKindId();
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
    }
}