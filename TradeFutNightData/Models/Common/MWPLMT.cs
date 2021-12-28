using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class MWPLMT : DtoParent<MWPLMT>
    {
        [PrimaryKey]
        public virtual string MWPLMT_KIND_ID { get; set; } // char(4)

        public virtual decimal MWPLMT_LIMIT { get; set; } // decimal(11, 7)
        public virtual decimal MWPLMT_LIMIT_SPREAD { get; set; } // decimal(11, 7)
        public virtual DateTime MWPLMT_W_TIME { get; set; } // datetime

        [PrimaryKey]
        public virtual string MWPLMT_MONTH { get; set; } // char(6)
    }
}