using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class JCF : DtoParent<JCF>
    {
        [PrimaryKey]
        public virtual string JCF_JOB_ID { get; set; } // char(5)

        public virtual char? JCF_CONFIRM_CODE { get; set; } // char(1)
        public virtual string JCF_USER_ID { get; set; } // char(5)
        public virtual DateTime? JCF_W_TIME { get; set; } // datetime
        public virtual string JCF_WAIT_JOB_ID { get; set; } // char(7)
    }
}