using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class FRQ : DtoParent<FRQ>
    {
        [PrimaryKey]
        public char FRQ_MSG_TYPE { get; set; } // char(1)
        public int FRQ_INTERVAL { get; set; } // int
        public string FRQ_REMARK { get; set; } // char(50)
        public string FRQ_USER_ID { get; set; } // char(5)
        public DateTime FRQ_W_TIME { get; set; } // datetime
    }
}
