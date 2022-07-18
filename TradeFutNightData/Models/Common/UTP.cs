using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class UTP : DtoParent<UTP>
    {
        [PrimaryKey]
        public virtual string UTP_USER_ID { get; set; } // char(5)

        [PrimaryKey]
        public virtual string UTP_TXN_ID { get; set; } // char(5)

        public virtual char? UTP_YN_CODE { get; set; } // char(1)
        public virtual char? UTP_FILLER { get; set; } // char(1)
        public virtual DateTime UTP_W_DATE { get; set; } // datetime
        public virtual string UTP_W_USER_ID { get; set; } // char(5)
    }
}