using System;
using CrossModel;

namespace TradeFutNightData.Models.Specific.PrefixB
{
    public class DTO_BN001 : DtoParent<DTO_BN001>
    {
        public string FMIF_PROD_ID { get; set; }
        public int FMIF_M_COUNT_TAL { get; set; }
        public int FMIF_M_QNTY_TAL { get; set; }
        public decimal FMIF_SETTLE_PRICE { get; set; }
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