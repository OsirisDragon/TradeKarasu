using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class PCM : DtoParent<PCM>
    {
        [PrimaryKey]
        public string PCM_FCM_NO { get; set; } // char(7)

        [PrimaryKey]
        public string PCM_PROD_ID { get; set; } // char(10)

        public string PCM_CM_NO { get; set; } // char(4)
        public char PCM_CONSTRICT { get; set; } // char(1)
        public DateTime? PCM_CON_TIME { get; set; } // datetime
        public DateTime? PCM_UNCON_TIME { get; set; } // datetime
    }
}