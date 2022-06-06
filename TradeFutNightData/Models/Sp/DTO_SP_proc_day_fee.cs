using CrossModel;

namespace TradeFutNightData.Models.Sp
{
    public class DTO_SP_proc_day_fee : DtoParent<DTO_SP_proc_day_fee>
    {
        public string PROD_ID_OUT { get; set; }
        public string PROD_SETTLE_DATE { get; set; }
        public string FCM_NO { get; set; }
        public string BRK_NAME { get; set; }
        public decimal CLR_QNTY { get; set; }
        public decimal FEE { get; set; }
    }
}