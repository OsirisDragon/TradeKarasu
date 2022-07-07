using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class YTXN : DtoParent<YTXN>
    {
        [PrimaryKey]
        public virtual string YTXN_ID { get; set; } // char(7)

        public virtual string YTXN_NAME { get; set; } // char(40)
        public virtual DateTime YTXN_W_TIME { get; set; } // datetime
        public virtual string YTXN_W_USER_ID { get; set; } // char(5)
        public virtual string YTXN_SYS { get; set; } // char(20)
        public virtual char YTXN_TYPE { get; set; } // char(1)
        public virtual char? YTXN_DEFAULT { get; set; } // char(1)
        public virtual string YTXN_REMARK { get; set; } // varchar(50)
        public virtual DateTime? YTXN_BEGIN_TIME { get; set; } // datetime
        public virtual DateTime? YTXN_END_TIME { get; set; } // datetime
        public virtual char? YTXN_RUN_CODE { get; set; } // char(1)
        public virtual char YTXN_ON_CODE { get; set; } // char(1)
        public virtual string YTXN_SERVICE { get; set; } // char(12)
    }
}