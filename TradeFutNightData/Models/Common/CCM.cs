using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class CCM : DtoParent<CCM>
    {
        [PrimaryKey]
        public virtual string CCM_FCM_NO { get; set; } // char(7)

        public virtual string CCM_CM_NO { get; set; } // char(4)

        [PrimaryKey]
        public virtual char CCM_SUBTYPE { get; set; } // char(1)

        public virtual DateTime CCM_W_TIME { get; set; } // datetime
        public virtual string CCM_USER_ID { get; set; } // char(5)
    }
}