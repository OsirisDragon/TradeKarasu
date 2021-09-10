using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class TPPVOL : DtoParent<TPPVOL>
    {
        [PrimaryKey]
        public virtual string TPPVOL_TYPE { get; set; } // char(6)

        public virtual decimal TPPVOL_REMOVE_COUNT { get; set; } // smallmoney
        public virtual decimal TPPVOL_GROWTH_RATE { get; set; } // smallmoney
        public virtual decimal TPPVOL_INCREASE_RATE { get; set; } // smallmoney
        public virtual byte TPPVOL_SEND_COUNT { get; set; } // tinyint
        public virtual string TPPVOL_USER_ID { get; set; } // char(5)
        public virtual DateTime TPPVOL_W_TIME { get; set; } // datetime
        public virtual decimal? TPPVOL_SEND_THRESHOLD { get; set; } // smallmoney

        [PrimaryKey]
        public virtual byte? TPPVOL_INDEX_GRP { get; set; } // tinyint

        public virtual decimal TPPVOL_UP_DOWN_RATE { get; set; } // smallmoney
    }
}