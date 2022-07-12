using CrossModel;
using System;

namespace TradeOptNightData.Models.Common
{
    public class MTF : DtoParent<MTF>
    {
        public virtual string MTF_FCM_NO { get; set; } // char(7)
        public virtual string MTF_ORDER_NO { get; set; } // char(5)
        public virtual string MTF_ACC_NO { get; set; } // char(7)
        public virtual char MTF_ACC_CODE { get; set; } // char(1)
        public virtual string MTF_PROD_ID { get; set; } // char(20)
        public virtual string MTF_PROD_ID1 { get; set; } // char(10)
        public virtual char? MTF_BS_CODE1 { get; set; } // char(1)
        public virtual decimal? MTF_M_PRICE1 { get; set; } // smallmoney
        public virtual short? MTF_M_QNTY1 { get; set; } // smallint
        public virtual string MTF_PROD_ID2 { get; set; } // char(10)
        public virtual char? MTF_BS_CODE2 { get; set; } // char(1)
        public virtual decimal? MTF_M_PRICE2 { get; set; } // smallmoney
        public virtual short? MTF_M_QNTY2 { get; set; } // smallint
        public virtual char MTF_BS_CODE { get; set; } // char(1)
        public virtual decimal MTF_PRICE { get; set; } // smallmoney
        public virtual short MTF_QNTY { get; set; } // smallint
        public virtual char MTF_OC_CODE { get; set; } // char(1)
        public virtual string MTF_CM_NO { get; set; } // char(4)
        public virtual decimal MTF_SEQ_NO { get; set; } // numeric(8, 0)
        public virtual string MTF_M_INST { get; set; } // nvarchar(50)
        public virtual DateTime MTF_M_TIME { get; set; } // datetime
        public virtual char? MTF_OQ_CODE { get; set; } // char(1)
        public virtual string MTF_STATUS_NO { get; set; } // char(3)
        public virtual char MTF_ORDER_KIND { get; set; } // char(1)
        public virtual int? MTF_BEFORE_QNTY { get; set; } // int
        public virtual int? MTF_AFTER_QNTY { get; set; } // int
        public virtual char MTF_SC_CODE { get; set; } // char(1)
        public virtual char? MTF_ORDER_TYPE { get; set; } // char(1)
        public virtual string MTF_SOURCE_FCM_NO { get; set; } // char(7)
        public virtual int MTF_ACCU_BUY_CNT { get; set; } // int
        public virtual int MTF_ACCU_SELL_CNT { get; set; } // int
        public virtual int MTF_ACCU_QNTY { get; set; } // int
        public virtual int MTF_GRP_SEQ_NO { get; set; } // int
        public virtual char? MTF_M_CM_CODE { get; set; } // char(1)
        public virtual char MTF_MARKET_CODE { get; set; } // char(1)
        public virtual DateTime MTF_ORIG_TIME { get; set; } // datetime
    }
}