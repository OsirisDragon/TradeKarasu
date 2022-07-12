using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class UPFCRD : DtoParent<UPFCRD>
    {
        [PrimaryKey]
        public virtual string UPFCRD_CARD_NO { get; set; } // char(12)

        public virtual char UPFCRD_CARD_TYPE { get; set; } // char(1)
        public virtual string UPFCRD_USER_ID { get; set; } // char(5)
        public virtual char UPFCRD_DEPT_ID { get; set; } // char(1)
        public virtual DateTime? UPFCRD_VALID_DATE { get; set; } // datetime
        public virtual DateTime UPFCRD_W_DATE { get; set; } // datetime
        public virtual string UPFCRD_W_USER_ID { get; set; } // char(5)
    }
}