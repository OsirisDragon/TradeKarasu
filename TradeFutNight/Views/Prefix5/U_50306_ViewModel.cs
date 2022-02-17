using AutoMapper;
using ChangeTracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Specific.Prefix5;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50306_ViewModel : ViewModelParent<UIModel_50306>
    {
        public U_50306_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50306>();
        }

        public async void Open()
        {
            await Query();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_50306());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_50306)item);
        }

        public async Task Query()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPLMT, UIModel_50306>().ReverseMap();
            }));

            var task = Task.Run(() =>
            {
                IList<UIModel_50306> list = null;
                IList<SCP> listSCP = null;
                IList<SFD> listSFD = null;
                using (var das = Factory.CreateDalSession())
                {
                    var d50306 = new D_50306<UIModel_50306>(das);
                    list = d50306.List().ToList();
                    listSCP = new D_SCP(das).ListAll();
                    listSFD = new D_SFD(das).ListByDate(MagicalHats.Ocf.OCF_DATE);
                }

                // 現貨價
                // 非股票：SCP_CLOSE_PRICE
                // 股票：SFD_OPEN_REF
                foreach (var item in list)
                {
                    decimal? price = 0;
                    if (item.PDK_SUBTYPE != "S")
                    {
                        price = listSCP.Where(x => x.SCP_KIND_ID == item.PDK_KIND_ID || x.SCP_STOCK_ID == item.PDK_STOCK_ID)
                               .OrderByDescending(x => x.SCP_CLOSE_PRICE).FirstOrDefault()?.SCP_CLOSE_PRICE;
                    }
                    else if (item.PDK_SUBTYPE == "S")
                    {
                        price = (decimal)listSFD.Where(x => x.SFD_STOCK_ID == item.PDK_STOCK_ID).FirstOrDefault()?.SFD_OPEN_REF;
                    }
                    item.TARGET_PRICE = price.AsDecimal();
                }

                MainGridData = list.AsTrackable();
            });

            await task;
        }
    }

    public class UIModel_50306
    {
        public virtual string TPPLMT_PROD_ID { get; set; } // char(20)

        public virtual string TPPLMT_FIRST_PROD { get; set; } // char(10)
        public virtual string PDK_SUBTYPE { get; set; }

        public virtual string PDK_KIND_ID { get; set; }
        public virtual string PROD_SETTLE_DATE { get; set; }
        public virtual string PROD_SETTLE_DATE_SECOND { get; set; }
        public virtual string PDK_STOCK_ID { get; set; }

        public virtual decimal TARGET_PRICE { get; set; }

        public virtual decimal TPPINTD_UNIT { get; set; }

        public virtual decimal TPPLMT_LIMIT { get; set; } // decimal(11, 7)
    }
}