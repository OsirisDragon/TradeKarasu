using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class EXRT : DtoParent<EXRT>
    {
        [PrimaryKey] public virtual char EXRT_CURRENCY_TYPE { get; set; } // char(1)
        public virtual decimal EXRT_EXCHANGE_RATE { get; set; } // decimal(12, 6)
        public virtual string EXRT_USER_ID { get; set; } // char(5)
        public virtual DateTime EXRT_W_TIME { get; set; } // datetime

        [PrimaryKey]
        public virtual char EXRT_COUNT_CURRENCY { get; set; } // char(1)

        public virtual decimal EXRT_MARKET_EXCHANGE_RATE { get; set; } // decimal(12, 6)
    }
}