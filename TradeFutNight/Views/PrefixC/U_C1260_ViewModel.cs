using AutoMapper;
using ChangeTracking;
using CrossModel;
using Shield.File;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Tfxm;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNight.Views.PrefixC
{
    public class U_C1260_ViewModel : ViewModelParent<UIModel_C1260>
    {
        public IList<ItemInfo> FRPProdId
        {
            get { return GetProperty(() => FRPProdId); }
            set { SetProperty(() => FRPProdId, value); }
        }

        public U_C1260_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_C1260>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FRP, UIModel_C1260>().ReverseMap();
            }));

            FRPProdId = DropDownItems.FrpProdId();

            using (var das = Factory.CreateDalSession(SettingFile.Database.Tfxm_AH))
            {
                var dFRP = new D_FRP(das);
                var list = new ObservableCollection<FRP>();
                list.Add(dFRP.GetLatestByProdId("SPX_S%"));
                list.Add(dFRP.GetLatestByProdId("VIX"));
                list.Add(dFRP.GetLatestByProdId("FTSE_S%"));
                list.Add(dFRP.GetLatestByProdId("BRF_S%"));
                list.Add(dFRP.GetLatestByProdId("GDF_S%"));

                MainGridData = MapperInstance.Map<IList<UIModel_C1260>>(list).AsTrackable();
            }
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_C1260());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_C1260)item);
        }
    }

    public class UIModel_C1260 : FRP
    {
    }
}