using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class LOGF : DtoParent<LOGF>
    {
        [NotColumn]
        public decimal LOGF_SER { get; set; } // numeric(8, 0)
        public DateTime LOGF_TIME { get; set; } // datetime
        public string LOGF_USER_ID { get; set; } // char(5)
        public string LOGF_ITEM { get; set; } // char(7)
        public string LOGF_KEY_DATA { get; set; } // varchar(100)
        public char? LOGF_FILLER { get; set; } // char(1)
    }
}
