using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix9
{
    public class U_90002_ViewModel : ViewModelParent<UIModel_90002>
    {
        public IList<ItemInfo> TxnDefaultInfos
        {
            get { return GetProperty(() => TxnDefaultInfos); }
            set { SetProperty(() => TxnDefaultInfos, value); }
        }

        public U_90002_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_90002>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TXN, UIModel_90002>().ReverseMap();
            }));

            await Query();
            TxnDefaultInfos = DropDownItems.TxnDefault();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_90002()
            {
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_90002)item);
        }

        public async Task Query()
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dTXN = new D_TXN(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_90002>>(dTXN.ListAll()).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_90002 : TXN
    {
    }
}