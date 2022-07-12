using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class BMF512 : DtoParent<BMF512>
    {
        public decimal? BMF512_SEQ_NO { get; set; } // numeric(8, 0)
        public DateTime BMF512_W_DATETIME { get; set; } // datetime
        public DateTime BMF512_BEGIN_DATE { get; set; } // datetime
        public DateTime? BMF512_END_DATE { get; set; } // datetime
        public short BMF512_MSG_LEN { get; set; } // smallint
        public string BMF512_MSG_TEXT { get; set; } // char(80)
        public string BMF512_USER_ID { get; set; } // char(10)
        public char BMF512_MSG_TYPE { get; set; } // char(1)
        [PrimaryKey, Identity]
        public decimal BMF512_AUTO_SEQ_NO { get; set; } // numeric(8, 0)
    }
}
