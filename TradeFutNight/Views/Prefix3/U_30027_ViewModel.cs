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
    public class U_30027_ViewModel : ViewModelParent<UIModel_30027>
    {
        public IList<ItemInfo> IdxGroupInfos
        {
            get { return GetProperty(() => IdxGroupInfos); }
            set { SetProperty(() => IdxGroupInfos, value); }
        }

        public IList<ItemInfo> TypeInfos
        {
            get { return GetProperty(() => TypeInfos); }
            set { SetProperty(() => TypeInfos, value); }
        }

        public U_30027_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30027>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPVOL, UIModel_30027>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30027>().ToList().AsTrackable();
            IdxGroupInfos = DropDownItems.TppIndexGrp();
            TypeInfos = DropDownItems.TppType();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30027()
            {
                TPPVOL_INDEX_GRP = null,
                TPPVOL_TYPE = null
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30027)item);
        }
    }

    public class UIModel_30027 : TPPVOL
    {
    }
}