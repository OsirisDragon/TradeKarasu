using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class PROD : DtoParent<PROD>
    {
        [PrimaryKey]
        public virtual string PROD_ID { get; set; } // char(10)

        public virtual string PROD_NAME { get; set; } // char(20)
        public virtual string PROD_SETTLE_DATE { get; set; } // char(6)
        public virtual decimal PROD_STRIKE_PRICE { get; set; } // smallmoney
        public virtual char PROD_PC_CODE { get; set; } // char(1)
        public virtual decimal PROD_PREMIUM { get; set; } // smallmoney
        public virtual decimal PROD_SETTLE_PRICE { get; set; } // smallmoney
        public virtual char PROD_EXPIRE_CODE { get; set; } // char(1)
        public virtual DateTime PROD_BEGIN_DATE { get; set; } // datetime
        public virtual DateTime PROD_END_DATE { get; set; } // datetime
        public virtual int? PROD_M_QNTY { get; set; } // int
        public virtual decimal? PROD_M_PRICE { get; set; } // smallmoney
        public virtual int? PROD_GRP_ID { get; set; } // int
        public virtual decimal? PROD_RAISE_PRICE { get; set; } // smallmoney
        public virtual decimal? PROD_FALL_PRICE { get; set; } // smallmoney
        public virtual decimal? PROD_SEQ_NO { get; set; } // numeric(8, 0)
        public virtual double? PROD_RAISE_LIMIT { get; set; } // float
        public virtual double? PROD_FALL_LIMIT { get; set; } // float
        public virtual DateTime? PROD_VALID_DATE { get; set; } // datetime
        public virtual decimal? PROD_TERM_HIGH_PRICE { get; set; } // smallmoney
        public virtual decimal? PROD_TERM_LOW_PRICE { get; set; } // smallmoney
        public virtual decimal? PROD_THERICAL_P { get; set; } // smallmoney
        public virtual DateTime? PROD_REF_TIME { get; set; } // datetime
        public virtual string PROD_ID_OUT { get; set; } // char(7)
        public virtual DateTime? PROD_DELIVERY_DATE { get; set; } // datetime
        public virtual char? PROD_SEASON_CODE { get; set; } // char(1)
        public virtual DateTime? PROD_T1_DATE { get; set; } // datetime
        public virtual int? PROD_PGSEQ { get; set; } // int
        public virtual char? PROD_STATUS_CODE { get; set; } // char(1)
        public virtual decimal? PROD_RAISE_PRICE1 { get; set; } // smallmoney
        public virtual decimal? PROD_FALL_PRICE1 { get; set; } // smallmoney
        public virtual decimal? PROD_RAISE_PRICE2 { get; set; } // smallmoney
        public virtual decimal? PROD_FALL_PRICE2 { get; set; } // smallmoney
        public virtual byte? PROD_SPREAD_CODE { get; set; } // tinyint
        public virtual char? PROD_DAY_TRADE_CODE { get; set; } // char(1)
        public virtual byte? PROD_OSW_GRP { get; set; } // tinyint
        public virtual DateTime? PROD_OI_DATE { get; set; } // datetime
        public virtual int? PROD_OPEN_INTEREST { get; set; } // int
        public virtual string PROD_ID_LONG { get; set; } // char(20)
    }
}