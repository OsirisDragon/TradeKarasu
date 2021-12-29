using CrossModel;

namespace TradeFutNightData.Models.Common
{
    public class SCP : DtoParent<SCP>
    {
        public virtual string SCP_KIND_ID { get; set; } // char(4)
        public virtual string SCP_STOCK_ID { get; set; } // char(6)
        public virtual decimal SCP_CLOSE_PRICE { get; set; } // smallmoney
    }
}