using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Common
{
    public class PHALT : DtoParent<PHALT>
    {
        [PrimaryKey] public virtual DateTime PHALT_TRADE_DATE { get; set; } // datetime
        public virtual char PHALT_TYPE { get; set; } // char(1)
        [PrimaryKey] public virtual string PHALT_PROD_ID { get; set; } // char(10)
        [PrimaryKey] public virtual char PHALT_MSG_TYPE { get; set; } // char(1)
        public virtual DateTime? PHALT_TRADE_PAUSE_DATE { get; set; } // datetime
        public virtual string PHALT_TRADE_PAUSE_TIME { get; set; } // char(6)
        public virtual DateTime? PHALT_TRADE_RESUME_DATE { get; set; } // datetime
        public virtual string PHALT_TRADE_RESUME_TIME { get; set; } // char(6)
        public virtual string PHALT_ORDER_RESUME_TIME { get; set; } // char(6)
        public virtual string PHALT_MATCH_RESUME_TIME { get; set; } // char(6)
        public virtual string PHALT_STOCK_ID { get; set; } // char(6)
        public virtual string PHALT_USER_ID { get; set; } // char(5)
        public virtual DateTime PHALT_W_TIME { get; set; } // datetime
    }
}