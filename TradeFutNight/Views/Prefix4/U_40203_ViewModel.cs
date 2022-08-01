using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix4
{
    public class U_40203_ViewModel : ViewModelParent<UIModel_40203>
    {
        public IList<ItemInfo> PhaltTypeTMsgTypeInfos
        {
            get { return GetProperty(() => PhaltTypeTMsgTypeInfos); }
            set { SetProperty(() => PhaltTypeTMsgTypeInfos, value); }
        }

        public U_40203_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_40203>();
        }

        public async void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PHALT, UIModel_40203>().ReverseMap();
            }));

            using (var das = Factory.CreateDalSession())
            {
                var dPHALT = new D_PHALT(das);
                MainGridData = MapperInstance.Map<IList<UIModel_40203>>(dPHALT.ListNotResume()).AsTrackable();
            }

            PhaltTypeTMsgTypeInfos = DropDownItems.PhaltTypeTMsgType();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_40203());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_40203)item);
        }
    }

    public class UIModel_40203 : PHALT
    {
        public virtual bool IS_RESUME { get; set; }
    }
}