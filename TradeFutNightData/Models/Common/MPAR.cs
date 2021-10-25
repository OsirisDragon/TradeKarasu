using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class MPAR : DtoParent<MPAR>
    {
        public decimal MPAR_ADD_RATE { get; set; } // decimal(7, 4)
        public decimal MPAR_FUT_INTEREST { get; set; } // decimal(7, 4)

        [PrimaryKey]
        public DateTime MPAR_W_TIME { get; set; } // datetime

        public string MPAR_USER_ID { get; set; } // char(5)
        public decimal MPAR_TAX_RATE { get; set; } // decimal(7, 4)
        public decimal MPAR_SPREAD_MULTI { get; set; } // smallmoney
        public decimal MPAR_COMPOUND { get; set; } // numeric(8, 6)
        public char MPAR_MARKET_CODE { get; set; } // char(1)
        public char MPAR_AUTH_TYPE { get; set; } // char(1)
        public decimal MPAR_UP_DOWN_RATE { get; set; } // smallmoney
    }
}