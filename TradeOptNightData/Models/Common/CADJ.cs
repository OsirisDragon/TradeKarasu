using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class CADJ : DtoParent<CADJ>
    {
        [PrimaryKey]
        public virtual DateTime CADJ_BASE_DATE { get; set; } // datetime

        [PrimaryKey]
        public virtual string CADJ_BF_KIND_ID { get; set; } // char(4)

        public virtual string CADJ_BF_STOCK_ID { get; set; } // char(6)
        public virtual decimal? CADJ_BF_STOCK_QNTY { get; set; } // smallmoney
        public virtual decimal? CADJ_BF_STOCK_CASH2 { get; set; } // money
        public virtual decimal? CADJ_BF_STOCK_CASH3 { get; set; } // smallmoney
        public virtual string CADJ_BF_STOCK_ID4 { get; set; } // char(6)
        public virtual decimal? CADJ_BF_STOCK_QNTY4 { get; set; } // smallmoney
        public virtual string CADJ_AF_KIND_ID { get; set; } // char(4)
        public virtual string CADJ_AF_STOCK_ID { get; set; } // char(6)
        public virtual decimal? CADJ_AF_STOCK_QNTY { get; set; } // smallmoney
        public virtual decimal? CADJ_AF_STOCK_CASH2 { get; set; } // money
        public virtual decimal? CADJ_AF_STOCK_PRICE3 { get; set; } // smallmoney
        public virtual decimal? CADJ_AF_STOCK_QNTY3 { get; set; } // smallmoney
        public virtual DateTime? CADJ_AF_STOCK_DATE3 { get; set; } // datetime
        public virtual string CADJ_AF_STOCK_ID4 { get; set; } // char(6)
        public virtual decimal? CADJ_AF_STOCK_QNTY4 { get; set; } // smallmoney
        public virtual DateTime? CADJ_DIVIDEND_DATE { get; set; } // datetime
        public virtual string CADJ_USER_ID { get; set; } // char(5)
        public virtual DateTime CADJ_W_TIME { get; set; } // datetime
        public virtual decimal? CADJ_BF_STOCK_PRICE3 { get; set; } // smallmoney
        public virtual decimal? CADJ_BF_STOCK_QNTY3 { get; set; } // smallmoney
        public virtual DateTime? CADJ_BF_STOCK_DATE3 { get; set; } // datetime
        public virtual string CADJ_BF_KIND_ID_LONG { get; set; } // char(8)
        public virtual string CADJ_AF_KIND_ID_LONG { get; set; } // char(8)
    }
}