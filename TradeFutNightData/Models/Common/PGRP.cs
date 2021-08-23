using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class PGRP : DtoParent<PGRP>
    {
        [PrimaryKey]
        public string PGRP_KIND_ID { get; set; } // char(4)
        public int PGRP_GRP_ID { get; set; } // int
        public sbyte PGRP_OSW_GRP { get; set; } // tinyint
        public sbyte PGRP_SETTLE_OSW_GRP { get; set; } // tinyint
        public sbyte PGRP_DSP_GRP { get; set; } // tinyint
        public char PGRP_DSP_CONFIRM { get; set; } // char(1)
        public sbyte PGRP_BTRD_OSW_GRP { get; set; } // tinyint
         public sbyte? PGRP_AH_OSW_GRP { get; set; } // tinyint
         public sbyte? PGRP_AH_BTRD_OSW_GRP { get; set; } // tinyint
         public sbyte? PGRP_AH_MARKET_OPEN { get; set; } // tinyint
         public sbyte? PGRP_AH_MARKET_CLOSE { get; set; } // tinyint
         public sbyte? PGRP_AH_SETTLE_OSW_GRP { get; set; } // tinyint
    }
}
