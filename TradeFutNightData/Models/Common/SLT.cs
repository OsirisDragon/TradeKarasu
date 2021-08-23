using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class SLT : DtoParent<SLT>
    {
        [PrimaryKey]
        public virtual string SLT_KIND_ID { get; set; } // char(4)

        [PrimaryKey]
        public virtual decimal SLT_MAX { get; set; } // smallmoney

        public virtual decimal? SLT_MIN { get; set; } // smallmoney
        public virtual decimal? SLT_SPREAD { get; set; } // smallmoney

        [PrimaryKey]
        public virtual decimal SLT_SPREAD_LONG { get; set; } // smallmoney

        public virtual decimal SLT_SPREAD_MULTI { get; set; } // smallmoney
        public virtual decimal SLT_SPREAD_MAX { get; set; } // smallmoney
        public virtual short? SLT_VALID_QNTY { get; set; } // smallint
        public virtual char? SLT_PRICE_FLUC { get; set; } // char(1)
    }
}