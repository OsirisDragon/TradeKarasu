using CrossModel;
using System;

namespace TradeFutNightData.Models.Common
{
    public class PRODLMT : DtoParent<PRODLMT>
    {
        public string PRODLMT_PROD_ID { get; set; } // char(10)
        public sbyte PRODLMT_PLIMIT_CODE { get; set; } // tinyint
        public decimal? PRODLMT_RAISE_PRICE { get; set; } // smallmoney
        public decimal? PRODLMT_FALL_PRICE { get; set; } // smallmoney
        public DateTime PRODLMT_W_TIME { get; set; } // datetime
    }
}