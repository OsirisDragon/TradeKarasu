using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class TPPLMT : DtoParent<TPPLMT>
    {
        [PrimaryKey]
        public virtual string TPPLMT_PROD_ID { get; set; } // char(20)

        public virtual string TPPLMT_FIRST_PROD { get; set; } // char(10)
        public virtual string TPPLMT_SECOND_PROD { get; set; } // char(10)
        public virtual decimal TPPLMT_LIMIT { get; set; } // decimal(11, 7)
        public virtual DateTime TPPLMT_W_TIME { get; set; } // datetime
    }
}