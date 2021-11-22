using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class MHALT : DtoParent<MHALT>
    {
        [PrimaryKey]
        public DateTime MHALT_TRADE_DATE { get; set; } // datetime

        [PrimaryKey]
        public string MHALT_KIND_ID { get; set; } // char(4)

        public DateTime? MHALT_PAUSE_DATE { get; set; } // datetime
        public string MHALT_PAUSE_TIME { get; set; } // char(6)
        public decimal MHALT_SPREAD_MULTI { get; set; } // decimal(7, 4)
        public decimal MHALT_VALID_QNTY_MULTI { get; set; } // decimal(7, 4)
        public decimal MHALT_QUOTE_DURATION_MULTI { get; set; } // decimal(7, 4)
        public string MHALT_USER_ID { get; set; } // char(5)

        [PrimaryKey]
        public DateTime MHALT_W_TIME { get; set; } // datetime
    }
}