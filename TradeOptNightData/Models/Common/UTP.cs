using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class UTP : DtoParent<UTP>
    {
        [PrimaryKey]
        public string UTP_USER_ID { get; set; } // char(5)

        [PrimaryKey]
        public string UTP_TXN_ID { get; set; } // char(5)

        public char? UTP_YN_CODE { get; set; } // char(1)
        public char? UTP_FILLER { get; set; } // char(1)
        public DateTime UTP_W_DATE { get; set; } // datetime
        public string UTP_W_USER_ID { get; set; } // char(5)
    }
}