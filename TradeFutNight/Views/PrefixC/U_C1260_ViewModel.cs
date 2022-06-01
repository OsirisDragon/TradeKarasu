using AutoMapper;
using ChangeTracking;
using CrossModel;
using Shield.Mapping;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

            var ddwItemsFrpProd = DropDownItems.FrpProdId();

            using (var das = Factory.CreateDalSession(SettingDatabaseInfo.TfxmNight))
            {
                var dFRP = new D_FRP(das);
                var frps = new ObservableCollection<FRP>();
                frps.Add(dFRP.GetLatestByProdId("SPX_S%"));
                frps.Add(dFRP.GetLatestByProdId("VIX"));
                frps.Add(dFRP.GetLatestByProdId("FTSE_S%"));
                frps.Add(dFRP.GetLatestByProdId("BRF_S%"));
                frps.Add(dFRP.GetLatestByProdId("GDF_S%"));

                foreach (var frp in frps)
                {
                    if (ddwItemsFrpProd.Count(c => c.Value.ToString() == frp.FRP_PROD_ID) == 0)
                    {
                        ddwItemsFrpProd.Add(new ItemInfo() { Text = frp.FRP_PROD_ID, Value = frp.FRP_PROD_ID });
                    }
                }

                FRPProdId = ddwItemsFrpProd;
                MainGridData = MapperInstance.Map<IList<UIModel_C1260>>(frps).AsTrackable();
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