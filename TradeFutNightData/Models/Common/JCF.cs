using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class JCF : DtoParent<JCF>
    {
        [PrimaryKey]
        public string JCF_JOB_ID { get; set; } // char(5)

        public char? JCF_CONFIRM_CODE { get; set; } // char(1)
        public string JCF_USER_ID { get; set; } // char(5)
        public DateTime? JCF_W_TIME { get; set; } // datetime
        public string JCF_WAIT_JOB_ID { get; set; } // char(7)
    }
}