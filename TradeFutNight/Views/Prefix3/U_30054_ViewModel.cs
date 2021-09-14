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
    public class U_30054_ViewModel : ViewModelParent<UIModel_30054>
    {
        public IList<ItemInfo> KindIdTypeInfos
        {
            get { return GetProperty(() => KindIdTypeInfos); }
            set { SetProperty(() => KindIdTypeInfos, value); }
        }

        public U_30054_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_30054>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MORD, UIModel_30054>().ReverseMap();
            }));

            MainGridData = new ObservableCollection<UIModel_30054>().ToList().AsTrackable();
            KindIdTypeInfos = DropDownItems.MordKindIdType();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_30054()
            {
                MORD_KIND_ID_TYPE = ' ',
                MORD_KIND_ID = null
            });
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_30054)item);
        }
    }

    public class UIModel_30054 : MORD
    {
    }
}