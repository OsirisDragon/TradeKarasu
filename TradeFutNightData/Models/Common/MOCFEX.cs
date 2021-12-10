using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class MOCFEX : DtoParent<MOCFEX>
    {
        [PrimaryKey]
        public virtual DateTime MOCFEX_DATE { get; set; } // datetime

        public virtual char MOCFEX_DAY_OF_WEEK { get; set; } // char(1)
        public virtual char MOCFEX_CBOE_OPEN_CODE { get; set; } // char(1)
        public virtual string MOCFEX_USER_ID { get; set; } // char(5)
        public virtual DateTime MOCFEX_W_TIME { get; set; } // datetime
    }
}