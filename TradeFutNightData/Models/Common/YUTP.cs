using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class YUTP : DtoParent<YUTP>
    {
        [PrimaryKey]
        public virtual string YUTP_USER_ID { get; set; } // char(5)

        [PrimaryKey]
        public virtual string YUTP_YTXN_ID { get; set; } // char(5)

        public virtual char? YUTP_YN_CODE { get; set; } // char(1)
        public virtual DateTime YUTP_W_DATE { get; set; } // datetime
        public virtual string YUTP_W_USER_ID { get; set; } // char(5)
    }
}