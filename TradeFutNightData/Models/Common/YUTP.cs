using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class YUTP : DtoParent<YUTP>
    {
        [PrimaryKey]
        public string YUTP_USER_ID { get; set; } // char(5)

        [PrimaryKey]
        public string YUTP_YTXN_ID { get; set; } // char(5)

        public char? YUTP_YN_CODE { get; set; } // char(1)
        public DateTime YUTP_W_DATE { get; set; } // datetime
        public string YUTP_W_USER_ID { get; set; } // char(5)
    }
}