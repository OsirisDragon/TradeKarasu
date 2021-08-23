using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class MOCFEX : DtoParent<MOCFEX>
    {
        [PrimaryKey]
        public DateTime MOCFEX_DATE { get; set; } // datetime
        public char MOCFEX_DAY_OF_WEEK { get; set; } // char(1)
        public char MOCFEX_CBOE_OPEN_CODE { get; set; } // char(1)
        public string MOCFEX_USER_ID { get; set; } // char(5)
        public DateTime MOCFEX_W_TIME { get; set; } // datetime
    }
}
