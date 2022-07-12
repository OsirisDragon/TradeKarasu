using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class PDK2ND : DtoParent<PDK2ND>
    {
        [PrimaryKey]
        public virtual string PDK2ND_KIND_ID { get; set; } // char(4)

        public virtual decimal PDK2ND_UNIT { get; set; } // decimal(11, 7)
        public virtual decimal PDK2ND_UNIT_SPREAD { get; set; } // decimal(11, 7)
        public virtual char PDK2ND_PRICE_FLUC { get; set; } // char(1)
        public virtual string PDK2ND_USER_ID { get; set; } // char(5)
        public virtual DateTime PDK2ND_W_TIME { get; set; } // datetime
        public virtual char PDK2ND_PRICE_TYPE { get; set; } // char(1)
        public virtual char PDK2ND_TPP_PRICE_TYPE { get; set; } // char(1)
    }
}