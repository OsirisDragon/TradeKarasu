using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Tfxm
{
    public class FRP : DtoParent<FRP>
    {
        [PrimaryKey]
        public virtual string FRP_PROD_ID { get; set; } // char(10)

        public virtual DateTime FRP_DATE { get; set; } // datetime
        public virtual decimal FRP_PX { get; set; } // numeric(10, 4)

        [PrimaryKey]
        public virtual DateTime FRP_UPDATE_TIME { get; set; } // datetime

        public virtual string FRP_USER_ID { get; set; } // char(10)
        public virtual DateTime? FRP_W_TIME { get; set; } // datetime
        public virtual char FRP_CONFIRM { get; set; } // char(1)
    }
}