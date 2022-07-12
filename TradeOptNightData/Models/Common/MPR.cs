using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class MPR : DtoParent<MPR>
    {
        [PrimaryKey]
        public virtual string MPR_PROD_ID { get; set; } // char(20)

        [PrimaryKey]
        public virtual DateTime MPR_M_TIME { get; set; } // datetime

        public virtual decimal MPR_M_PRICE { get; set; } // smallmoney
        public virtual int MPR_M_QNTY { get; set; } // int

        [PrimaryKey]
        public virtual decimal MPR_SEQ_NO { get; set; } // numeric(8, 0)

        public virtual decimal MPR_BUY_HIGH_PRICE { get; set; } // smallmoney
        public virtual decimal MPR_SELL_LOW_PRICE { get; set; } // smallmoney
        public virtual int MPR_BO_COUNT { get; set; } // int
        public virtual int MPR_SO_COUNT { get; set; } // int
        public virtual decimal MPR_FIRST_M_PRICE { get; set; } // smallmoney

        [PrimaryKey]
        public virtual char MPR_MARKET_CODE { get; set; } // char(1)

        public virtual DateTime MPR_ORIG_TIME { get; set; } // datetime
    }
}