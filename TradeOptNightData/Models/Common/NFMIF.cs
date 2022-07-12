using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Common
{
    public class NFMIF : DtoParent<NFMIF>
    {
        [PrimaryKey]
        public string NFMIF_PROD_ID { get; set; } // char(20)
        public int NFMIF_BO_COUNT_TAL { get; set; } // int
        public int NFMIF_BO_QNTY_TAL { get; set; } // int
        public int NFMIF_SO_COUNT_TAL { get; set; } // int
        public int NFMIF_SO_QNTY_TAL { get; set; } // int
        public decimal NFMIF_HIGH_PRICE { get; set; } // smallmoney
        public decimal NFMIF_LOW_PRICE { get; set; } // smallmoney
        public decimal NFMIF_OPEN_PRICE { get; set; } // smallmoney
        public decimal NFMIF_CLOSE_PRICE { get; set; } // smallmoney
        public int NFMIF_M_COUNT_TAL { get; set; } // int
        public int NFMIF_M_QNTY_TAL { get; set; } // int
        public decimal? NFMIF_TERM_HIGH_PRICE { get; set; } // smallmoney
        public decimal? NFMIF_TERM_LOW_PRICE { get; set; } // smallmoney
        public decimal? NFMIF_LAST_BUY_PRICE { get; set; } // smallmoney
        public decimal? NFMIF_LAST_SELL_PRICE { get; set; } // smallmoney
        public int? NFMIF_OPEN_INTEREST { get; set; } // int
        public decimal? NFMIF_UP_DOWN_VAL { get; set; } // smallmoney
        public decimal? NFMIF_LAST_DAY_CLOSE { get; set; } // smallmoney
        public decimal? NFMIF_SETTLE_PRICE { get; set; } // smallmoney
        public int? NFMIF_MB_COUNT_TAL { get; set; } // int
        public int? NFMIF_MS_COUNT_TAL { get; set; } // int
        public decimal? NFMIF_OPEN_REF { get; set; } // smallmoney
        public DateTime? NFMIF_M_TIME { get; set; } // datetime
        public short? NFMIF_M_QNTY { get; set; } // smallint
        public int NFMIF_BO_COUNT_TAL_S { get; set; } // int
        public int NFMIF_BO_QNTY_TAL_S { get; set; } // int
        public int NFMIF_SO_COUNT_TAL_S { get; set; } // int
        public int NFMIF_SO_QNTY_TAL_S { get; set; } // int
        public int NFMIF_M_QNTY_TAL_S { get; set; } // int
        public int NFMIF_BTRD_QNTY_TAL { get; set; } // int
    }
}
