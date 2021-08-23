using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class OCF : DtoParent<OCF>
    {
        public DateTime OCF_ACCEPT_TIME { get; set; } // datetime
        public DateTime OCF_OPEN_TIME { get; set; } // datetime
        public DateTime OCF_CLOSE_TIME { get; set; } // datetime
        public DateTime OCF_SINGLE_TIME { get; set; } // datetime
        public DateTime OCF_DATE { get; set; } // datetime
        [PrimaryKey]
        public string OCF_KIND_ID { get; set; } // char(4)
        [PrimaryKey]
        public char OCF_TYPE { get; set; } // char(1)
        public DateTime OCF_NEXT_DATE { get; set; } // datetime
        public int? OCF_CURR_OPEN_SW { get; set; } // int
        public char? OCF_FORCE_EXEC_FLAG { get; set; } // char(1)
        public DateTime? OCF_PREV_DATE { get; set; } // datetime
    }

}
