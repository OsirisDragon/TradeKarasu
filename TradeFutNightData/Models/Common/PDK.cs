using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class PDK : DtoParent<PDK>
    {
        [PrimaryKey]
        public string PDK_KIND_ID { get; set; } // char(4)
        [PrimaryKey]
        public char PDK_TYPE { get; set; } // char(1)
        public short? PDK_MIN_QNTY { get; set; } // smallint
        public decimal? PDK_MAX_SPREAD { get; set; } // smallmoney
        public decimal? PDK_FEE { get; set; } // smallmoney
        public decimal? PDK_SHORT { get; set; } // smallmoney
        public decimal? PDK_LONG { get; set; } // smallmoney
        public short? PDK_SHORT_N { get; set; } // smallint
        public double? PDK_RAISE_LIMIT { get; set; } // float
        public double? PDK_FALL_LIMIT { get; set; } // float
        public decimal? PDK_XXX { get; set; } // money
        public string PDK_NAME { get; set; } // char(30)
        public decimal? PDK_RAISE_LIMIT1 { get; set; } // smallmoney
        public decimal? PDK_FALL_LIMIT1 { get; set; } // smallmoney
        public short? PDK_LOCK_TIME1 { get; set; } // smallint
        public short? PDK_COOL_TIME1 { get; set; } // smallint
        public decimal? PDK_RAISE_LIMIT2 { get; set; } // smallmoney
        public decimal? PDK_FALL_LIMIT2 { get; set; } // smallmoney
        public short? PDK_LOCK_TIME2 { get; set; } // smallint
        public short? PDK_COOL_TIME2 { get; set; } // smallint
        public short? PDK_LONG_N { get; set; } // smallint
        public string PDK_PROD_ID { get; set; } // char(5)
        public decimal? PDK_PROD_IDX { get; set; } // smallmoney
        public DateTime? PDK_REF_TIME { get; set; } // datetime
        public decimal? PDK_MARGIN { get; set; } // money
        public short? PDK_LONG_MONTH { get; set; } // smallint
        public string PDK_CATEGORY { get; set; } // char(3)
        public char? PDK_STATUS { get; set; } // char(1)
        public string PDK_STOCK_ID { get; set; } // char(6)
        public string PDK_KIND_ID_OUT { get; set; } // char(7)
        public short? PDK_MAX_QNTY { get; set; } // smallint
        public decimal? PDK_P_LAST_SETTLE_PRICE { get; set; } // smallmoney
        public decimal? PDK_C_LAST_SETTLE_PRICE { get; set; } // smallmoney
        public string PDK_OFF_MONTH { get; set; } // char(6)
        public decimal? PDK_MARGIN_SPREAD { get; set; } // smallmoney
        public decimal? PDK_MARGIN_STRADDLE { get; set; } // smallmoney
        public decimal? PDK_MARGIN_CONVERSION { get; set; } // smallmoney
        public decimal? PDK_MARGIN_QUOTE { get; set; } // smallmoney
        public string PDK_USER_ID { get; set; } // char(5)
        public DateTime? PDK_W_TIME { get; set; } // datetime
        public decimal? PDK_SELF_FEE { get; set; } // smallmoney
        public decimal? PDK_MARKET_FEE { get; set; } // smallmoney
        public byte PDK_SCALE { get; set; } // tinyint
        public char PDK_BROADCAST { get; set; } // char(1)
        public char PDK_PRICE_FLUC { get; set; } // char(1)
        public char PDK_SUBTYPE { get; set; } // char(1)
        public decimal? PDK_BUY_LAST_SETTLE_PRICE { get; set; } // money
        public decimal? PDK_SELL_LAST_SETTLE_PRICE { get; set; } // money
        public string PDK_MARKET_CLOSE { get; set; } // char(2)
        public DateTime? PDK_TRADE_PAUSE { get; set; } // datetime
        public DateTime? PDK_TRADE_RESUME { get; set; } // datetime
        public DateTime? PDK_SERIES_PAUSE { get; set; } // datetime
        public DateTime? PDK_SERIES_RESUME { get; set; } // datetime
        public char PDK_EXPIRE_CODE { get; set; } // char(1)
        public char PDK_STATUS_CODE { get; set; } // char(1)
        public char PDK_SERIES_CODE { get; set; } // char(1)
        public string PDK_COOL_GRP_ID { get; set; } // char(2)
        public byte PDK_PLIMIT_CODE { get; set; } // tinyint
        public byte PDK_SHORT_STEP { get; set; } // tinyint
        public char PDK_CURRENCY_TYPE { get; set; } // char(1)
        public string PDK_PARAM_KEY { get; set; } // char(3)
        public byte PDK_PRICE_SCALE { get; set; } // tinyint
        public char PDK_QUOTE_CODE { get; set; } // char(1)
        public char PDK_DAY_TRADE_CODE { get; set; } // char(1)
        public short PDK_DAY_TRADE_MONTH { get; set; } // smallint
        public decimal? PDK_MARGIN_ORIGINAL { get; set; } // smallmoney
        public char PDK_MARKET_CODE { get; set; } // char(1)
        public DateTime? PDK_BEGIN_DATE { get; set; } // datetime
        public DateTime? PDK_END_DATE { get; set; } // datetime
        public char PDK_ON_CODE { get; set; } // char(1)
        public decimal? PDK_STOCK_QNTY { get; set; } // smallmoney
        public decimal? PDK_STOCK_CASH2 { get; set; } // money
        public decimal? PDK_STOCK_CASH3 { get; set; } // smallmoney
        public decimal? PDK_STOCK_PRICE3 { get; set; } // smallmoney
        public decimal? PDK_STOCK_QNTY3 { get; set; } // smallmoney
        public DateTime? PDK_STOCK_DATE3 { get; set; } // datetime
        public string PDK_STOCK_ID4 { get; set; } // char(6)
        public decimal? PDK_STOCK_QNTY4 { get; set; } // smallmoney
        public decimal? PDK_ODD_LAST_SETTLE_PRICE { get; set; } // money
        public char PDK_BTRD_CODE { get; set; } // char(1)
        public short? PDK_BTRD_MIN_QNTY { get; set; } // smallint
        public char PDK_EXPIRY_TYPE { get; set; } // char(1)
        public string PDK_KIND_ID_LONG { get; set; } // char(8)
        public char PDK_UNDERLYING_MARKET { get; set; } // char(1)
        public decimal? PDK_ORIG_PROD_IDX { get; set; } // smallmoney
        public char PDK_FSP_CONFIRM { get; set; } // char(1)
        public string PDK_BTRD_STOCK_ID { get; set; } // char(6)
        public char? PDK_REMARK { get; set; } // char(1)
        public byte PDK_MARKET_OPEN { get; set; } // tinyint
        public decimal? PDK_RAISE_LIMIT9 { get; set; } // smallmoney
        public decimal? PDK_FALL_LIMIT9 { get; set; } // smallmoney
        public byte PDK_LONG_STEP { get; set; } // tinyint
        public char PDK_END_SESSION { get; set; } // char(1)
        public char PDK_EXEMPT_CODE { get; set; } // char(1)
        public char? PDK_STOCK_PRICE_FLUC { get; set; } // char(1)
    }
}
