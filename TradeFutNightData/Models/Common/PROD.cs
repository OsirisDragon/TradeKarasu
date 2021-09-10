using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class PROD : DtoParent<PROD>
    {
        [PrimaryKey]
        public string PROD_ID { get; set; } // char(10)
        public string PROD_NAME { get; set; } // char(20)
        public string PROD_SETTLE_DATE { get; set; } // char(6)
        public decimal PROD_STRIKE_PRICE { get; set; } // smallmoney
        public char PROD_PC_CODE { get; set; } // char(1)
        public decimal PROD_PREMIUM { get; set; } // smallmoney
        public decimal PROD_SETTLE_PRICE { get; set; } // smallmoney
        public char PROD_EXPIRE_CODE { get; set; } // char(1)
        public DateTime PROD_BEGIN_DATE { get; set; } // datetime
        public DateTime PROD_END_DATE { get; set; } // datetime
        public int? PROD_M_QNTY { get; set; } // int
        public decimal? PROD_M_PRICE { get; set; } // smallmoney
        public int? PROD_GRP_ID { get; set; } // int
        public decimal? PROD_RAISE_PRICE { get; set; } // smallmoney
        public decimal? PROD_FALL_PRICE { get; set; } // smallmoney
        public decimal? PROD_SEQ_NO { get; set; } // numeric(8, 0)
        public double? PROD_RAISE_LIMIT { get; set; } // float
        public double? PROD_FALL_LIMIT { get; set; } // float
        public DateTime? PROD_VALID_DATE { get; set; } // datetime
        public decimal? PROD_TERM_HIGH_PRICE { get; set; } // smallmoney
        public decimal? PROD_TERM_LOW_PRICE { get; set; } // smallmoney
        public decimal? PROD_THERICAL_P { get; set; } // smallmoney
        public DateTime? PROD_REF_TIME { get; set; } // datetime
        public string PROD_ID_OUT { get; set; } // char(7)
        public DateTime? PROD_DELIVERY_DATE { get; set; } // datetime
        public char? PROD_SEASON_CODE { get; set; } // char(1)
        public DateTime? PROD_T1_DATE { get; set; } // datetime
        public int? PROD_PGSEQ { get; set; } // int
        public char? PROD_STATUS_CODE { get; set; } // char(1)
        public decimal? PROD_RAISE_PRICE1 { get; set; } // smallmoney
        public decimal? PROD_FALL_PRICE1 { get; set; } // smallmoney
        public decimal? PROD_RAISE_PRICE2 { get; set; } // smallmoney
        public decimal? PROD_FALL_PRICE2 { get; set; } // smallmoney
        public byte? PROD_SPREAD_CODE { get; set; } // tinyint
        public char? PROD_DAY_TRADE_CODE { get; set; } // char(1)
        public byte? PROD_OSW_GRP { get; set; } // tinyint
        public DateTime? PROD_OI_DATE { get; set; } // datetime
        public int? PROD_OPEN_INTEREST { get; set; } // int
        public string PROD_ID_LONG { get; set; } // char(20)
    }
}
