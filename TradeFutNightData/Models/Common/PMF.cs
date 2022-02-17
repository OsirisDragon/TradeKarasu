using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class PMF : DtoParent<PMF>
    {
        [PrimaryKey]
        public virtual string PMF_KIND_ID { get; set; } // char(4)

        [PrimaryKey]
        public virtual char PMF_TYPE { get; set; } // char(1)

        [PrimaryKey]
        public virtual string PMF_MONTH { get; set; } // char(6)

        public virtual DateTime? PMF_VALID_DATE { get; set; } // datetime
        public virtual DateTime? PMF_END_DATE { get; set; } // datetime
        public virtual DateTime? PMF_BEGIN_DATE { get; set; } // datetime
        public virtual DateTime? PMF_DELIVERY_DATE { get; set; } // datetime
        public virtual decimal? PMF_REF_PRICE { get; set; } // smallmoney
        public virtual string PMF_USER_ID { get; set; } // char(5)
        public virtual DateTime? PMF_W_TIME { get; set; } // datetime
        public virtual DateTime? PMF_T1_DATE { get; set; } // datetime
        public virtual decimal PMF_COMPOUND { get; set; } // numeric(8, 6)
    }
}