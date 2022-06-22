using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class SDI : DtoParent<SDI>
    {
        [PrimaryKey]
        public virtual DateTime SDI_BASE_DATE { get; set; } // datetime

        [PrimaryKey]
        public virtual string SDI_STOCK_ID { get; set; } // char(6)

        public virtual decimal SDI_DIVIDEND_SHARE { get; set; } // decimal(10, 8)
        public virtual decimal SDI_DIVIDEND_CASH { get; set; } // decimal(11, 8)
        public virtual decimal SDI_INCREASE_PRICE { get; set; } // smallmoney
        public virtual decimal SDI_INCREASE_RATE { get; set; } // decimal(10, 8)
        public virtual DateTime? SDI_INCREASE_DATE { get; set; } // datetime
        public virtual decimal SDI_DISINVEST_RATE { get; set; } // decimal(10, 8)
        public virtual string SDI_COMB_STOCK_ID { get; set; } // char(6)
        public virtual decimal SDI_COMB_RATE { get; set; } // decimal(10, 8)
        public virtual string SDI_OTH_STOCK_ID { get; set; } // char(6)
        public virtual decimal SDI_OTH_RATE { get; set; } // decimal(10, 8)
        public virtual DateTime? SDI_DIVIDEND_DATE { get; set; } // datetime
        public virtual decimal SDI_U { get; set; } // decimal(10, 8)
        public virtual decimal SDI_N1 { get; set; } // decimal(10, 8)
        public virtual string SDI_ADJUST_TYPE_CH { get; set; } // varchar(20)
        public virtual string SDI_ADJUST_CONTENT_CH { get; set; } // varchar(240)
        public virtual string SDI_WEBSITE_CH { get; set; } // varchar(100)
        public virtual string SDI_ADJUST_TYPE_EN { get; set; } // varchar(50)
        public virtual string SDI_ADJUST_CONTENT_EN { get; set; } // varchar(400)
        public virtual string SDI_WEBSITE_EN { get; set; } // varchar(100)
        public virtual DateTime? SDI_POSITION_DEADLINE { get; set; } // datetime
        public virtual decimal? SDI_POSITION_STOCK_QNTY { get; set; } // smallmoney
    }
}