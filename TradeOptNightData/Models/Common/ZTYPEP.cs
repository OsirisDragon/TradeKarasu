using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class ZTYPEP : DtoParent<ZTYPEP>
    {
        [PrimaryKey]
        public virtual string ZTYPEP_PROD { get; set; } // char(10)

        public virtual string ZTYPEP_PROD_NAME { get; set; } // char(15)
        public virtual string ZTYPEP_PROD_TYPE { get; set; } // char(3)
        public virtual string ZTYPEP_PRICE_MODEL { get; set; } // char(2)
        public virtual string ZTYPEP_PRICE_QUOTE { get; set; } // char(3)
        public virtual string ZTYPEP_VALUATION { get; set; } // char(5)
        public virtual string ZTYPEP_SETTLEMENT { get; set; } // char(5)
        public virtual string ZTYPEP_EXERCISE { get; set; } // char(4)
    }
}