using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix4
{
    public class U_40201_ViewModel : ViewModelParent<UIModel_40201>
    {
        public IList<ItemInfo> PhaltTypeTMsgTypeInfos
        {
            get { return GetProperty(() => PhaltTypeTMsgTypeInfos); }
            set { SetProperty(() => PhaltTypeTMsgTypeInfos, value); }
        }

        public U_40201_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_40201>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PHALT, UIModel_40201>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_40201>().ToList().AsTrackable();

            PhaltTypeTMsgTypeInfos = DropDownItems.PhaltTypeTMsgType();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_40201());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_40201)item);
        }
    }

    public class UIModel_40201 : PHALT
    {
    }
}