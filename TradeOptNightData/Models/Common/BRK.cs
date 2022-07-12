using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class BRK : DtoParent<BRK>
    {
        [PrimaryKey]
        public virtual string BRK_NO { get; set; } // char(7)

        public virtual string BRK_NAME { get; set; } // char(20)
        public virtual char BRK_TYPE { get; set; } // char(1)
        public virtual string BRK_TEL { get; set; } // char(11)
        public virtual string BRK_ABBR_NAME { get; set; } // char(10)
        public virtual string BRK_AREA { get; set; } // char(2)
        public virtual string BRK_ADDR { get; set; } // char(50)
        public virtual char BRK_OPEN_CODE { get; set; } // char(1)
        public virtual DateTime? BRK_CLOSE_DATE { get; set; } // datetime
        public virtual DateTime BRK_CRE_DATE { get; set; } // datetime
        public virtual DateTime BRK_MOD_DATETIME { get; set; } // datetime
        public virtual string BRK_LAST_USER { get; set; } // char(10)
        public virtual char? BRK_OPEN_CODE_F { get; set; } // char(1)
        public virtual char? BRK_OPEN_CODE_O { get; set; } // char(1)
        public virtual char? BRK_OPEN_CODE_3 { get; set; } // char(1)
        public virtual char? BRK_OPEN_CODE_4 { get; set; } // char(1)
        public virtual char? BRK_OPEN_CODE_5 { get; set; } // char(1)
        public virtual int BRK_SEQ { get; set; } // int
    }
}