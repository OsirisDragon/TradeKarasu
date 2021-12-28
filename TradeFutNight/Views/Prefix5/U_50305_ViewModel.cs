using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Specific.Prefix5;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50305_ViewModel : ViewModelParent<UIModel_50305>
    {
        public IList<ItemInfo> PriceFlucItemInfos
        {
            get { return GetProperty(() => PriceFlucItemInfos); }
            set { SetProperty(() => PriceFlucItemInfos, value); }
        }

        public U_50305_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50305>();
        }

        public async void Open()
        {
            await Query();

            PriceFlucItemInfos = DropDownItems.PriceFlucItem();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_50305());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_50305)item);
        }

        public async Task Query()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MWPLMT, UIModel_50305>().ReverseMap();
            }));

            var task = Task.Run(() =>
            {
                IList<UIModel_50305> list = null;
                IList<SCP> listSCP = null;
                IList<SFD> listSFD = null;
                using (var das = Factory.CreateDalSession())
                {
                    var d50305 = new D_50305<UIModel_50305>(das);
                    list = d50305.List().ToList();
                    listSCP = new D_SCP(das).ListAll();
                    listSFD = new D_SFD(das).ListByDate(MagicalHats.Ocf.OCF_DATE);
                }

                // 現貨價
                // 非股票：SCP_CLOSE_PRICE
                // 股票：SFD_OPEN_REF
                foreach (var item in list)
                {
                    decimal price = 0;
                    if (item.PDK_SUBTYPE != "S")
                    {
                        price = listSCP.Where(x => x.SCP_KIND_ID == item.MWPLMT_KIND_ID || x.SCP_STOCK_ID == item.PDK_STOCK_ID)
                               .OrderByDescending(x => x.SCP_CLOSE_PRICE).FirstOrDefault().SCP_CLOSE_PRICE;
                    }
                    else if (item.PDK_SUBTYPE == "S")
                    {
                        price = (decimal)listSFD.Where(x => x.SFD_STOCK_ID == item.PDK_STOCK_ID).FirstOrDefault().SFD_OPEN_REF;
                    }
                    item.ACTUALS_PRICE = price;
                }

                MainGridData = list.AsTrackable();
            });

            await task;
        }
    }

    public class UIModel_50305 : MWPLMT
    {
        public virtual string PDK_SUBTYPE { get; set; }
        public virtual char PDK2ND_PRICE_FLUC { get; set; }
        public virtual decimal PDK2ND_UNIT { get; set; }
        public virtual decimal PDK2ND_UNIT_SPREAD { get; set; }
        public virtual decimal ACTUALS_PRICE { get; set; }
        public virtual string PDK_STOCK_ID { get; set; }
    }
}