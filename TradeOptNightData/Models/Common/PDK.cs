using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class PDK : DtoParent<PDK>
    {
        [PrimaryKey]
        public virtual string PDK_KIND_ID { get; set; } // char(4)

        [PrimaryKey]
        public virtual char PDK_TYPE { get; set; } // char(1)

        public virtual short? PDK_MIN_QNTY { get; set; } // smallint
        public virtual decimal? PDK_MAX_SPREAD { get; set; } // smallmoney
        public virtual decimal? PDK_FEE { get; set; } // smallmoney
        public virtual decimal? PDK_SHORT { get; set; } // smallmoney
        public virtual decimal? PDK_LONG { get; set; } // smallmoney
        public virtual short? PDK_SHORT_N { get; set; } // smallint
        public virtual double? PDK_RAISE_LIMIT { get; set; } // float
        public virtual double? PDK_FALL_LIMIT { get; set; } // float
        public virtual decimal? PDK_XXX { get; set; } // money
        public virtual string PDK_NAME { get; set; } // char(30)
        public virtual decimal? PDK_RAISE_LIMIT1 { get; set; } // smallmoney
        public virtual decimal? PDK_FALL_LIMIT1 { get; set; } // smallmoney
        public virtual short? PDK_LOCK_TIME1 { get; set; } // smallint
        public virtual short? PDK_COOL_TIME1 { get; set; } // smallint
        public virtual decimal? PDK_RAISE_LIMIT2 { get; set; } // smallmoney
        public virtual decimal? PDK_FALL_LIMIT2 { get; set; } // smallmoney
        public virtual short? PDK_LOCK_TIME2 { get; set; } // smallint
        public virtual short? PDK_COOL_TIME2 { get; set; } // smallint
        public virtual short? PDK_LONG_N { get; set; } // smallint
        public virtual string PDK_PROD_ID { get; set; } // char(5)
        public virtual decimal? PDK_PROD_IDX { get; set; } // smallmoney
        public virtual DateTime? PDK_REF_TIME { get; set; } // datetime
        public virtual decimal? PDK_MARGIN { get; set; } // money
        public virtual short? PDK_LONG_MONTH { get; set; } // smallint
        public virtual string PDK_CATEGORY { get; set; } // char(3)
        public virtual char? PDK_STATUS { get; set; } // char(1)
        public virtual string PDK_STOCK_ID { get; set; } // char(6)
        public virtual string PDK_KIND_ID_OUT { get; set; } // char(7)
        public virtual short? PDK_MAX_QNTY { get; set; } // smallint
        public virtual decimal? PDK_P_LAST_SETTLE_PRICE { get; set; } // smallmoney
        public virtual decimal? PDK_C_LAST_SETTLE_PRICE { get; set; } // smallmoney
        public virtual string PDK_OFF_MONTH { get; set; } // char(6)
        public virtual decimal? PDK_MARGIN_SPREAD { get; set; } // smallmoney
        public virtual decimal? PDK_MARGIN_STRADDLE { get; set; } // smallmoney
        public virtual decimal? PDK_MARGIN_CONVERSION { get; set; } // smallmoney
        public virtual decimal? PDK_MARGIN_QUOTE { get; set; } // smallmoney
        public virtual string PDK_USER_ID { get; set; } // char(5)
        public virtual DateTime? PDK_W_TIME { get; set; } // datetime
        public virtual decimal? PDK_SELF_FEE { get; set; } // smallmoney
        public virtual decimal? PDK_MARKET_FEE { get; set; } // smallmoney
        public virtual byte PDK_SCALE { get; set; } // tinyint
        public virtual char PDK_BROADCAST { get; set; } // char(1)
        public virtual char PDK_PRICE_FLUC { get; set; } // char(1)
        public virtual char PDK_SUBTYPE { get; set; } // char(1)
        public virtual decimal? PDK_BUY_LAST_SETTLE_PRICE { get; set; } // money
        public virtual decimal? PDK_SELL_LAST_SETTLE_PRICE { get; set; } // money
        public virtual string PDK_MARKET_CLOSE { get; set; } // char(2)
        public virtual DateTime? PDK_TRADE_PAUSE { get; set; } // datetime
        public virtual DateTime? PDK_TRADE_RESUME { get; set; } // datetime
        public virtual DateTime? PDK_SERIES_PAUSE { get; set; } // datetime
        public virtual DateTime? PDK_SERIES_RESUME { get; set; } // datetime
        public virtual char PDK_EXPIRE_CODE { get; set; } // char(1)
        public virtual char PDK_STATUS_CODE { get; set; } // char(1)
        public virtual char PDK_SERIES_CODE { get; set; } // char(1)
        public virtual string PDK_COOL_GRP_ID { get; set; } // char(2)
        public virtual byte PDK_PLIMIT_CODE { get; set; } // tinyint
        public virtual byte PDK_SHORT_STEP { get; set; } // tinyint
        public virtual char PDK_CURRENCY_TYPE { get; set; } // char(1)
        public virtual string PDK_PARAM_KEY { get; set; } // char(3)
        public virtual byte PDK_PRICE_SCALE { get; set; } // tinyint
        public virtual char PDK_QUOTE_CODE { get; set; } // char(1)
        public virtual char PDK_DAY_TRADE_CODE { get; set; } // char(1)
        public virtual short PDK_DAY_TRADE_MONTH { get; set; } // smallint
        public virtual decimal? PDK_MARGIN_ORIGINAL { get; set; } // smallmoney
        public virtual char PDK_MARKET_CODE { get; set; } // char(1)
        public virtual DateTime? PDK_BEGIN_DATE { get; set; } // datetime
        public virtual DateTime? PDK_END_DATE { get; set; } // datetime
        public virtual char PDK_ON_CODE { get; set; } // char(1)
        public virtual decimal? PDK_STOCK_QNTY { get; set; } // smallmoney
        public virtual decimal? PDK_STOCK_CASH2 { get; set; } // money
        public virtual decimal? PDK_STOCK_CASH3 { get; set; } // smallmoney
        public virtual decimal? PDK_STOCK_PRICE3 { get; set; } // smallmoney
        public virtual decimal? PDK_STOCK_QNTY3 { get; set; } // smallmoney
        public virtual DateTime? PDK_STOCK_DATE3 { get; set; } // datetime
        public virtual string PDK_STOCK_ID4 { get; set; } // char(6)
        public virtual decimal? PDK_STOCK_QNTY4 { get; set; } // smallmoney
        public virtual decimal? PDK_ODD_LAST_SETTLE_PRICE { get; set; } // money
        public virtual char PDK_BTRD_CODE { get; set; } // char(1)
        public virtual short? PDK_BTRD_MIN_QNTY { get; set; } // smallint
        public virtual char PDK_EXPIRY_TYPE { get; set; } // char(1)
        public virtual string PDK_KIND_ID_LONG { get; set; } // char(8)
        public virtual char PDK_UNDERLYING_MARKET { get; set; } // char(1)
        public virtual decimal? PDK_ORIG_PROD_IDX { get; set; } // smallmoney
        public virtual char PDK_FSP_CONFIRM { get; set; } // char(1)
        public virtual string PDK_BTRD_STOCK_ID { get; set; } // char(6)
        public virtual char? PDK_REMARK { get; set; } // char(1)
        public virtual byte PDK_MARKET_OPEN { get; set; } // tinyint
        public virtual decimal? PDK_RAISE_LIMIT9 { get; set; } // smallmoney
        public virtual decimal? PDK_FALL_LIMIT9 { get; set; } // smallmoney
        public virtual byte PDK_LONG_STEP { get; set; } // tinyint
        public virtual char PDK_END_SESSION { get; set; } // char(1)
        public virtual char PDK_EXEMPT_CODE { get; set; } // char(1)
        public virtual char? PDK_STOCK_PRICE_FLUC { get; set; } // char(1)
    }
}