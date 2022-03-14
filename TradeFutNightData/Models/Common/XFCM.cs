using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class XFCM : DtoParent<XFCM>
    {
        public string XFCM_FCM_NO { get; set; } // char(7)
        public char XFCM_SYS_ID { get; set; } // char(1)

        [PrimaryKey]
        public string XFCM_IP { get; set; } // char(20)

        public char XFCM_PROTOCOL { get; set; } // char(1)

        [PrimaryKey]
        public int XFCM_PORT_NO { get; set; } // int

        public int XFCM_SESSION_ID { get; set; } // int
        public string XFCM_PASSWORD { get; set; } // char(4)
        public char XFCM_R1314_FLAG { get; set; } // char(1)
        public decimal XFCM_MAX_ORDER { get; set; } // smallmoney
        public char? XFCM_START_FLAG { get; set; } // char(1)
        public string XFCM_USER_ID { get; set; } // char(5)
        public DateTime XFCM_W_TIME { get; set; } // datetime
        public char? XFCM_ORD_PROTOCOL { get; set; } // char(1)
        public int? XFCM_ORD_SESSION_ID { get; set; } // int
    }
}