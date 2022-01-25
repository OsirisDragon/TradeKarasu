using AutoMapper;
using CrossModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixB
{
    public class U_BN001_ViewModel : ViewModelParent<UIModel_BN001>
    {
        public ItemInfo PgrpDspGrp
        {
            get { return GetProperty(() => PgrpDspGrp); }
            set { SetProperty(() => PgrpDspGrp, value); }
        }

        public IList<ItemInfo> PgrpDspGrps
        {
            get { return GetProperty(() => PgrpDspGrps); }
            set { SetProperty(() => PgrpDspGrps, value); }
        }

        public IEnumerable<UIModel_BN001> MtfEveryProdLastRecords { get; set; }

        public U_BN001_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_BN001>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPINTD, UIModel_BN001>().ReverseMap();
            }));

            using (var das = Factory.CreateDalSession())
            {
                var dMtf = new D_MTF<UIModel_BN001>(das);
                MtfEveryProdLastRecords = dMtf.ListEveryProdLastRecord();
            }
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_BN001());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_BN001)item);
        }

        public async Task Query(string firstKindId, string secondKindId)
        {
            var task = Task.Run(() =>
            {
            });

            await task;
        }
    }

    public class UIModel_BN001 : FMIF
    {
        public string PDK_NAME { get; set; }
        public string PDK_STOCK_ID { get; set; }
        public string PDK_PARAM_KEY { get; set; }
        public decimal PDK_PROD_IDX { get; set; }
        public string PDK_QUOTE_CODE { get; set; }
        public decimal CLSPRC_SETTLE_PRICE { get; set; }
        public decimal LAST_ONE_MIN_WEIGHT_AVG_PRICE { get; set; }
        public decimal LAST_BUY_PRICE { get; set; }
        public decimal LAST_SELL_PRICE { get; set; }
        public decimal BUY_SELL_MIDDLE { get; set; }
        public string ADJUST_TYPE { get; set; }
        public decimal CLSPRC_OPEN_INTEREST { get; set; }
        public decimal? INITIAL_SETTLE_PRICE { get; set; }
        public decimal FLUCTUATION { get; set; }
        public string PROD_ID { get; set; }
        public string PROD_ID_OUT { get; set; }
        public string PROD_SETTLE_DATE { get; set; }
        public decimal? PROD_THERICAL_P { get; set; }
        public decimal PROD_RAISE_PRICE { get; set; }
        public decimal PROD_RAISE_PRICE1 { get; set; }
        public decimal PROD_RAISE_PRICE2 { get; set; }
        public decimal PROD_FALL_PRICE { get; set; }
        public decimal PROD_FALL_PRICE1 { get; set; }
        public decimal PROD_FALL_PRICE2 { get; set; }
        public DateTime PROD_DELIVERY_DATE { get; set; }
        public decimal MTF_PRICE { get; set; }
        public DateTime? MTF_ORIG_TIME { get; set; }
        public decimal? ACTUALS_CLOSE_PRICE_FLUCTUATION { get; set; }
        public decimal? PERCENT { get; set; }
        public string REMARK_FIRST { get; set; }
        public string REMARK_SECOND { get; set; }
        public string CATEGORY { get; set; }
        public decimal SLT_SPREAD_NEAR_MONTH { get; set; }
    }
}