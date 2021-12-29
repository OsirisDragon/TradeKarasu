using AutoMapper;
using ChangeTracking;
using CrossModel;
using Shield.File;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.PrefixC;
using TradeFutNightData.Gates.Tfxm;
using TradeFutNightData.Models.Common;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNight.Views.PrefixC
{
    public class U_C1270_ViewModel : ViewModelParent<UIModel_C1270>
    {
        public IList<ItemInfo> OswGrp
        {
            get { return GetProperty(() => OswGrp); }
            set { SetProperty(() => OswGrp, value); }
        }

        public U_C1270_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_C1270>();
        }

        public void Open()
        {
            OswGrp = DropDownItems.OswGrpAH();
        }

        public async Task Query(int oswGrp)
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FRP, UIModel_C1270>().ReverseMap();
            }));
            var task = Task.Run(() =>
            {
                IList<UIModel_C1270> list = null;

                using (var das = Factory.CreateDalSession())
                {
                    var dC1270 = new D_C1270<UIModel_C1270>(das);
                    list = dC1270.ListByGrp(oswGrp).ToList();
                }

                // 第二盤只顯示商品類
                if (oswGrp == 11)
                {
                    list = list.Where(x => x.PDK_SUBTYPE == "C").ToList();
                }

                using (var das = Factory.CreateDalSession(SettingFile.Database.Tfxm))
                {
                    var dRSFD = new D_RSFD(das);

                    foreach (var item in list)
                    {
                        // 1和2是上市和上櫃，交易部表示國外的商品都不要顯示現貨相關資料
                        if (item.PDK_UNDERLYING_MARKET == "1" || item.PDK_UNDERLYING_MARKET == "2")
                        {
                            var listRSFD = dRSFD.GetLatestByStockId(MagicalHats.Ocf.OCF_DATE, item.PDK_STOCK_ID);
                            if (listRSFD != null)
                            {
                                item.ACTUALS_CLOSE_PRICE = (decimal)listRSFD.RSFD_CLOSE_PRICE;
                                item.ACTUALS_CLOSE_PRICE_DATE = ((DateTime)listRSFD.RSFD_DATE).ToString("yyyy/MM/dd");
                            }
                        }
                    }
                }
                MainGridData = list.AsTrackable();
            });

            await task;
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_C1270());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_C1270)item);
        }
    }

    public class UIModel_C1270 : TPPBP
    {
        public virtual string PDK_KIND_ID { get; set; }
        public virtual string PDK_STOCK_ID { get; set; }

        public virtual string PDK_UNDERLYING_MARKET { get; set; }
        public virtual string PDK_SUBTYPE { get; set; }
        public virtual string PROD_SETTLE_DATE { get; set; }

        public virtual decimal? ACTUALS_CLOSE_PRICE { get; set; }

        public virtual string ACTUALS_CLOSE_PRICE_DATE { get; set; }
        //public virtual decimal COMPUTE_SUBTRACT { get; set; }
    }
}