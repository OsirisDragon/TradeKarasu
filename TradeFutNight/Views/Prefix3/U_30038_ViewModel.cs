using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    public class U_30038_ViewModel : ViewModelParent<UIModel_30038>
    {
        public IList<ItemInfo> IdxGroupInfos
        {
            get { return GetProperty(() => IdxGroupInfos); }
            set { SetProperty(() => IdxGroupInfos, value); }
        }

        public U_30038_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30038>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPDK, UIModel_30038>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30038>().ToList().AsTrackable();
            IdxGroupInfos = DropDownItems.TppIndexGrp();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30038()
            {
                TPPDK_INDEX_GRP = null
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30038)item);
        }
    }

    public class UIModel_30038 : TPPDK
    {
    }
}