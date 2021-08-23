using System;
using CrossModel;

namespace TradeFutNightData.Models.Common
{
    public class MTF : DtoParent<MTF>
    {
        public string MTF_FCM_NO { get; set; } // char(7)
        public string MTF_ORDER_NO { get; set; } // char(5)
        public string MTF_ACC_NO { get; set; } // char(7)
        public char MTF_ACC_CODE { get; set; } // char(1)
        public string MTF_PROD_ID { get; set; } // char(20)
        public string MTF_PROD_ID1 { get; set; } // char(10)
        public char? MTF_BS_CODE1 { get; set; } // char(1)
        public decimal? MTF_M_PRICE1 { get; set; } // smallmoney
        public short? MTF_M_QNTY1 { get; set; } // smallint
        public string MTF_PROD_ID2 { get; set; } // char(10)
        public char? MTF_BS_CODE2 { get; set; } // char(1)
        public decimal? MTF_M_PRICE2 { get; set; } // smallmoney
        public short? MTF_M_QNTY2 { get; set; } // smallint
        public char MTF_BS_CODE { get; set; } // char(1)
        public decimal MTF_PRICE { get; set; } // smallmoney
        public short MTF_QNTY { get; set; } // smallint
        public char MTF_OC_CODE { get; set; } // char(1)
        public string MTF_CM_NO { get; set; } // char(4)
        public decimal MTF_SEQ_NO { get; set; } // numeric(8, 0)
        public string MTF_M_INST { get; set; } // nvarchar(50)
        public DateTime MTF_M_TIME { get; set; } // datetime
        public char? MTF_OQ_CODE { get; set; } // char(1)
        public string MTF_STATUS_NO { get; set; } // char(3)
        public char MTF_ORDER_KIND { get; set; } // char(1)
        public int? MTF_BEFORE_QNTY { get; set; } // int
        public int? MTF_AFTER_QNTY { get; set; } // int
        public char MTF_SC_CODE { get; set; } // char(1)
        public char? MTF_ORDER_TYPE { get; set; } // char(1)
        public string MTF_SOURCE_FCM_NO { get; set; } // char(7)
        public int MTF_ACCU_BUY_CNT { get; set; } // int
        public int MTF_ACCU_SELL_CNT { get; set; } // int
        public int MTF_ACCU_QNTY { get; set; } // int
        public int MTF_GRP_SEQ_NO { get; set; } // int
        public char? MTF_M_CM_CODE { get; set; } // char(1)
        public char MTF_MARKET_CODE { get; set; } // char(1)
        public DateTime MTF_ORIG_TIME { get; set; } // datetime
    }
}
