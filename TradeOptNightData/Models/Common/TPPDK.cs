using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class TPPDK : DtoParent<TPPDK>
    {
        [PrimaryKey]
        public virtual string TPPDK_KIND_ID { get; set; } // char(4)

        public virtual byte? TPPDK_INDEX_GRP { get; set; } // tinyint
        public virtual decimal TPPDK_MULTI { get; set; } // decimal(2, 1)
        public virtual string TPPDK_USER_ID { get; set; } // char(5)
        public virtual DateTime TPPDK_W_TIME { get; set; } // datetime
        public virtual int TPPDK_THERICAL_P_INTERVAL { get; set; } // int
    }
}