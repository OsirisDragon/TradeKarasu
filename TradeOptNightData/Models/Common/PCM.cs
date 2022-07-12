using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class PCM : DtoParent<PCM>
    {
        [PrimaryKey]
        public virtual string PCM_FCM_NO { get; set; } // char(7)

        [PrimaryKey]
        public virtual string PCM_PROD_ID { get; set; } // char(10)

        public virtual string PCM_CM_NO { get; set; } // char(4)
        public virtual char PCM_CONSTRICT { get; set; } // char(1)
        public virtual DateTime? PCM_CON_TIME { get; set; } // datetime
        public virtual DateTime? PCM_UNCON_TIME { get; set; } // datetime
    }
}