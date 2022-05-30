using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class FMIFSTG : DtoParent<FMIFSTG>
    {
        [PrimaryKey]
        public string FMIFSTG_PROD_ID { get; set; } // char(20)

        public int FMIFSTG_BO_COUNT_TAL { get; set; } // int
        public int FMIFSTG_BO_QNTY_TAL { get; set; } // int
        public int FMIFSTG_SO_COUNT_TAL { get; set; } // int
        public int FMIFSTG_SO_QNTY_TAL { get; set; } // int
        public decimal? FMIFSTG_HIGH_PRICE { get; set; } // money
        public decimal? FMIFSTG_LOW_PRICE { get; set; } // money
        public decimal? FMIFSTG_OPEN_PRICE { get; set; } // money
        public decimal? FMIFSTG_CLOSE_PRICE { get; set; } // money
        public int FMIFSTG_M_COUNT_TAL { get; set; } // int
        public int FMIFSTG_M_QNTY_TAL { get; set; } // int
        public decimal? FMIFSTG_TERM_HIGH_PRICE { get; set; } // money
        public decimal? FMIFSTG_TERM_LOW_PRICE { get; set; } // money
        public decimal? FMIFSTG_LAST_BUY_PRICE { get; set; } // money
        public decimal? FMIFSTG_LAST_SELL_PRICE { get; set; } // money
        public int? FMIFSTG_MB_COUNT_TAL { get; set; } // int
        public int? FMIFSTG_MS_COUNT_TAL { get; set; } // int
        public DateTime? FMIFSTG_M_TIME { get; set; } // datetime
        public short? FMIFSTG_M_QNTY { get; set; } // smallint
    }
}