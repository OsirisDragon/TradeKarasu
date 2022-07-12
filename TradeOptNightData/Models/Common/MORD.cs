using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class MORD : DtoParent<MORD>
    {
        [PrimaryKey]
        public virtual string MORD_KIND_ID { get; set; } // char(4)

        public virtual char MORD_KIND_ID_TYPE { get; set; } // char(1)

        [PrimaryKey]
        public virtual byte MORD_SPREAD_CODE { get; set; } // tinyint

        public virtual short MORD_MAX_QNTY { get; set; } // smallint
        public virtual string MORD_USER_ID { get; set; } // char(5)
        public virtual DateTime MORD_W_TIME { get; set; } // datetime
    }
}