using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class TPPADJ : DtoParent<TPPADJ>
    {
        [PrimaryKey] public virtual DateTime TPPADJ_TRADE_DATE { get; set; } // datetime
        public virtual char TPPADJ_TYPE { get; set; } // char(1)
        [PrimaryKey] public virtual string TPPADJ_PROD_ID { get; set; } // char(10)
        [PrimaryKey] public virtual string TPPADJ_SETTLE_DATE { get; set; } // char(6)
        public virtual decimal? TPPADJ_M_PRICE_LIMIT { get; set; } // smallmoney
        public virtual decimal? TPPADJ_M_PRICE_LIMIT_F { get; set; } // smallmoney
        public virtual int? TPPADJ_M_INTERVAL { get; set; } // int
        public virtual byte? TPPADJ_ACCU_QNTY { get; set; } // tinyint
        public virtual decimal? TPPADJ_M_PRICE_FILTER { get; set; } // smallmoney
        public virtual decimal? TPPADJ_BS_PRICE_FILTER { get; set; } // smallmoney
        public virtual decimal? TPPADJ_DIVIDEND_POINTS { get; set; } // smallmoney
        public virtual decimal? TPPADJ_THERICAL_P_REF { get; set; } // smallmoney
        public virtual decimal? TPPADJ_SPREAD { get; set; } // smallmoney
        public virtual string TPPADJ_USER_ID { get; set; } // char(5)
        [PrimaryKey] public virtual DateTime TPPADJ_W_TIME { get; set; } // datetime
    }
}