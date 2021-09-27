using CrossModel;

namespace TradeFutNightData.Models.Specific.PrefixA
{
    public class DTO_A9920 : DtoParent<DTO_A9920>
    {
        public string PROD_ID { get; set; }
        public string PROD_SETTLE_DATE { get; set; }
        public string PDK_KIND_ID { get; set; }
        public string PDK_SUBTYPE { get; set; }
        public string PDK_STOCK_ID { get; set; }
        public decimal? TPPBP_THERICAL_P_REF { get; set; }
    }
}