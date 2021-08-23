using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class CLSPRC : DtoParent<CLSPRC>
    {
        [PrimaryKey]
        public string CLSPRC_PROD_ID { get; set; } // char(10)
        public decimal CLSPRC_SETTLE_PRICE { get; set; } // smallmoney
        public int CLSPRC_OPEN_INTEREST { get; set; } // int
        public decimal CLSPRC_PREMIUM { get; set; } // smallmoney
        public decimal CLSPRC_RAISE_PRICE { get; set; } // smallmoney
        public decimal CLSPRC_FALL_PRICE { get; set; } // smallmoney
    }
}
