using CrossModel;

namespace TradeFutNightData.Models.Sp
{
    public class DTO_SP_proc_BTRD_mtf_detail : DtoParent<DTO_SP_proc_BTRD_mtf_detail>
    {
        public string PROD_ID_OUT { get; set; }
        public string PROD_SETTLE_DATE { get; set; }
        public string BMTF_FCM_NO { get; set; }
        public string BRK_NAME { get; set; }
        public decimal BMTF_BUY_QNTY { get; set; }
        public decimal BMTF_PRICE { get; set; }
        public decimal BMTF_SELL_QNTY { get; set; }
    }
}