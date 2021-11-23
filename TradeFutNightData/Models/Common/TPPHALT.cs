using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class TPPHALT : DtoParent<TPPHALT>
    {
        [PrimaryKey]
        public DateTime TPPHALT_TRADE_DATE { get; set; } // datetime

        public char TPPHALT_TYPE { get; set; } // char(1)

        [PrimaryKey]
        public string TPPHALT_PROD_ID { get; set; } // char(20)

        [PrimaryKey]
        public string TPPHALT_SETTLE_DATE { get; set; } // char(6)

        [PrimaryKey]
        public char TPPHALT_MSG_TYPE { get; set; } // char(1)

        public DateTime? TPPHALT_PAUSE_DATE { get; set; } // datetime
        public string TPPHALT_PAUSE_TIME { get; set; } // char(6)
        public decimal? TPPHALT_MULTI { get; set; } // decimal(2, 1)
        public char? TPPHALT_RANGE { get; set; } // char(1)
        public DateTime? TPPHALT_RESUME_DATE { get; set; } // datetime
        public string TPPHALT_RESUME_TIME { get; set; } // char(6)
        public char TPPHALT_FUT_LINK_OPT { get; set; } // char(1)
        public string TPPHALT_USER_ID { get; set; } // char(5)

        [PrimaryKey]
        public DateTime TPPHALT_W_TIME { get; set; } // datetime
    }
}