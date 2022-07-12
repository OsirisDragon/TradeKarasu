using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Common
{
    public class BLOT : DtoParent<BLOT>
    {
        [PrimaryKey]
        public virtual string BLOT_KIND_ID { get; set; } // char(4)

        [PrimaryKey]
        public virtual decimal BLOT_MAX { get; set; } // decimal(10, 4)

        public virtual decimal BLOT_MIN { get; set; } // decimal(10, 4)
        public virtual decimal BLOT_MIN_QNTY { get; set; } // decimal(10, 4)
    }
}