using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class CVAR : DtoParent<CVAR>
    {
        [PrimaryKey]
        public virtual DateTime CVAR_BASE_DATE { get; set; } // datetime

        [PrimaryKey]
        public virtual string CVAR_KIND_ID { get; set; } // char(4)

        [PrimaryKey]
        public virtual char CVAR_VAR_CODE { get; set; } // char(1)

        public virtual char? CVAR_CONFIRM_CODE { get; set; } // char(1)
        public virtual string CVAR_USER_ID { get; set; } // char(5)
        public virtual DateTime CVAR_W_TIME { get; set; } // datetime
        public virtual DateTime? CVAR_DELIVERY_DATE { get; set; } // datetime
    }
}