using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class TPPST : DtoParent<TPPST>
    {
        [PrimaryKey] public virtual string TPPST_KIND_ID { get; set; } // char(4)
        [PrimaryKey] public virtual byte TPPST_MONTH { get; set; } // tinyint
        public virtual decimal TPPST_M_PRICE_LIMIT { get; set; } // smallmoney
        public virtual decimal TPPST_M_PRICE_LIMIT_F { get; set; } // smallmoney
        public virtual int TPPST_M_INTERVAL { get; set; } // int
        public virtual byte TPPST_ACCU_QNTY { get; set; } // tinyint
        public virtual decimal TPPST_M_PRICE_FILTER { get; set; } // smallmoney
        public virtual decimal TPPST_BS_PRICE_FILTER { get; set; } // smallmoney
        public virtual string TPPST_INDEX_ID { get; set; } // char(10)
        public virtual decimal TPPST_UNIT { get; set; } // decimal(11, 7)
        public virtual string TPPST_USER_ID { get; set; } // char(5)
        public virtual DateTime TPPST_W_TIME { get; set; } // datetime
        public virtual char TPPST_UNDERLYING_MARKET { get; set; } // char(1)
    }
}