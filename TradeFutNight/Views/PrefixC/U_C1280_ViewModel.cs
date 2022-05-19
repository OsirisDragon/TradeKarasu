using AutoMapper;
using ChangeTracking;
using CrossModel;
using Shield.Mapping;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Tfxm;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNight.Views.PrefixC
{
    public class U_C1280_ViewModel : ViewModelParent<UIModel_C1280>
    {
        public IList<ItemInfo> FRPProdId
        {
            get { return GetProperty(() => FRPProdId); }
            set { SetProperty(() => FRPProdId, value); }
        }

        public U_C1280_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_C1280>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FRP, UIModel_C1280>().ReverseMap();
            }));

            FRPProdId = DropDownItems.FrpProdId();

            using (var das = Factory.CreateDalSession(SettingDatabaseInfo.TfxmNight))
            {
                var dFRP = new D_FRP(das);
                var list = new ObservableCollection<FRP>();
                list.Add(dFRP.GetLatestByProdId("STWNam%"));

                MainGridData = MapperInstance.Map<IList<UIModel_C1280>>(list).AsTrackable();
            }
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_C1280());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_C1280)item);
        }
    }

    public class UIModel_C1280 : FRP
    {
    }
}