using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class TPPINTD : DtoParent<TPPINTD>
    {
        [PrimaryKey]
        public virtual string TPPINTD_FIRST_KIND_ID { get; set; } // char(4)

        [PrimaryKey]
        public virtual byte TPPINTD_FIRST_MONTH { get; set; } // tinyint

        [PrimaryKey]
        public virtual string TPPINTD_SECOND_KIND_ID { get; set; } // char(4)

        [PrimaryKey]
        public virtual byte TPPINTD_SECOND_MONTH { get; set; } // tinyint

        public virtual decimal TPPINTD_M_PRICE_LIMIT { get; set; } // smallmoney
        public virtual int TPPINTD_M_INTERVAL { get; set; } // int
        public virtual byte TPPINTD_ACCU_QNTY { get; set; } // tinyint
        public virtual decimal TPPINTD_M_PRICE_FILTER { get; set; } // smallmoney
        public virtual decimal TPPINTD_BS_PRICE_FILTER { get; set; } // smallmoney
        public virtual decimal TPPINTD_DIVIDEND_POINTS { get; set; } // smallmoney
        public virtual string TPPINTD_USER_ID { get; set; } // char(5)
        public virtual DateTime TPPINTD_W_TIME { get; set; } // datetime
        public virtual decimal TPPINTD_UNIT { get; set; } // decimal(11, 7)
        public virtual decimal TPPINTD_M_PRICE_LIMIT_F { get; set; } // smallmoney
        public virtual string TPPINTD_KIND_ID_FUT { get; set; } // char(4)

        public virtual int TPPINTD_FOREIGN_INTERVAL { get; set; }
    }
}