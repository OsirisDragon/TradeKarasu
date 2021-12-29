using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class SFD : DtoParent<SFD>
    {
        [PrimaryKey]
        public virtual DateTime SFD_TRADE_DATE { get; set; } // datetime

        [PrimaryKey]
        public virtual string SFD_STOCK_ID { get; set; } // char(6)

        public virtual decimal? SFD_LAST_PRICE { get; set; } // smallmoney
        public virtual decimal? SFD_CLOSE_PRICE { get; set; } // smallmoney
        public virtual decimal? SFD_OPEN_REF { get; set; } // smallmoney
        public virtual decimal? SFD_RAISE_PRICE { get; set; } // smallmoney
        public virtual decimal? SFD_FALL_PRICE { get; set; } // smallmoney
        public virtual decimal? SFD_DIV_CLOSE_PRICE { get; set; } // smallmoney
        public virtual decimal? SFD_DIV_REF_PRICE { get; set; } // smallmoney
        public virtual decimal? SFD_DIV_RAISE_PRICE { get; set; } // smallmoney
        public virtual decimal? SFD_DIV_FALL_PRICE { get; set; } // smallmoney
    }
}