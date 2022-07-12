using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Common
{
    public class FMIF : DtoParent<FMIF>
    {
        [PrimaryKey]
        public string FMIF_PROD_ID { get; set; } // char(20)
        public int FMIF_BO_COUNT_TAL { get; set; } // int
        public int FMIF_BO_QNTY_TAL { get; set; } // int
        public int FMIF_SO_COUNT_TAL { get; set; } // int
        public int FMIF_SO_QNTY_TAL { get; set; } // int
        public decimal FMIF_HIGH_PRICE { get; set; } // smallmoney
        public decimal FMIF_LOW_PRICE { get; set; } // smallmoney
        public decimal FMIF_OPEN_PRICE { get; set; } // smallmoney
        public decimal FMIF_CLOSE_PRICE { get; set; } // smallmoney
        public int FMIF_M_COUNT_TAL { get; set; } // int
        public int FMIF_M_QNTY_TAL { get; set; } // int
         public decimal? FMIF_TERM_HIGH_PRICE { get; set; } // smallmoney
         public decimal? FMIF_TERM_LOW_PRICE { get; set; } // smallmoney
         public decimal? FMIF_LAST_BUY_PRICE { get; set; } // smallmoney
         public decimal? FMIF_LAST_SELL_PRICE { get; set; } // smallmoney
         public int? FMIF_OPEN_INTEREST { get; set; } // int
         public decimal? FMIF_UP_DOWN_VAL { get; set; } // smallmoney
         public decimal? FMIF_LAST_DAY_CLOSE { get; set; } // smallmoney
         public decimal? FMIF_SETTLE_PRICE { get; set; } // smallmoney
         public int? FMIF_MB_COUNT_TAL { get; set; } // int
         public int? FMIF_MS_COUNT_TAL { get; set; } // int
         public decimal? FMIF_OPEN_REF { get; set; } // smallmoney
         public DateTime? FMIF_M_TIME { get; set; } // datetime
         public short? FMIF_M_QNTY { get; set; } // smallint
        public int FMIF_BO_COUNT_TAL_S { get; set; } // int
        public int FMIF_BO_QNTY_TAL_S { get; set; } // int
        public int FMIF_SO_COUNT_TAL_S { get; set; } // int
        public int FMIF_SO_QNTY_TAL_S { get; set; } // int
        public int FMIF_M_QNTY_TAL_S { get; set; } // int
        public int FMIF_BTRD_QNTY_TAL { get; set; } // int
    }
}
