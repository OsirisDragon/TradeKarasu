using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix9
{
    public class U_90001_ViewModel : ViewModelParent<UIModel_90001>
    {
        public IList<ItemInfo> TxnTypeInfos
        {
            get { return GetProperty(() => TxnTypeInfos); }
            set { SetProperty(() => TxnTypeInfos, value); }
        }

        public IList<ItemInfo> TxnDefaultInfos
        {
            get { return GetProperty(() => TxnDefaultInfos); }
            set { SetProperty(() => TxnDefaultInfos, value); }
        }

        public U_90001_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_90001>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TXN, UIModel_90001>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_90001>().ToList().AsTrackable();
            TxnTypeInfos = DropDownItems.TxnType();
            TxnDefaultInfos = DropDownItems.TxnDefault();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_90001()
            {
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_90001)item);
        }
    }

    public class UIModel_90001 : TXN
    {
    }
}