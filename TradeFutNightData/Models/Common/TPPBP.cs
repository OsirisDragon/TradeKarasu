using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class TPPBP : DtoParent<TPPBP>
    {
        public virtual string TPPBP_RT_MARKET_CLOSE { get; set; } // char(2)

        [PrimaryKey]
        public virtual string TPPBP_PROD_ID { get; set; } // char(10)

        public virtual decimal? TPPBP_THERICAL_P { get; set; } // smallmoney
        public virtual decimal? TPPBP_THERICAL_P_REF { get; set; } // smallmoney
    }
}