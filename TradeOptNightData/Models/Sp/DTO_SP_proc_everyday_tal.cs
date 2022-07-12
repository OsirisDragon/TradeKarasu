using CrossModel;

namespace TradeOptNightData.Models.Sp
{
    public class DTO_SP_proc_everyday_tal : DtoParent<DTO_SP_proc_everyday_tal>
    {
        public string PROD_ID_OUT { get; set; }
        public string PROD_SETTLE_DATE { get; set; }
        public string PROD_NAME { get; set; }
        public string FCM_NO { get; set; }
        public string BRK_NAME { get; set; }
        public decimal BC_QNTY { get; set; }
        public decimal SC_QNTY { get; set; }
        public decimal BO_QNTY { get; set; }
        public decimal SO_QNTY { get; set; }

    }
}