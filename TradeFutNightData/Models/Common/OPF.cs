using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class OPF : DtoParent<OPF>
    {
        [PrimaryKey]
        public virtual string OPF_OP_NO { get; set; } // char(7)

        public virtual char OPF_KIND { get; set; } // char(1)
        public virtual string OPF_SP_NAME { get; set; } // char(70)
        public virtual char? OPF_RUN_CODE { get; set; } // char(1)
        public virtual char? OPF_REP_CODE { get; set; } // char(1)
        public virtual DateTime? OPF_W_TIME { get; set; } // datetime
        public virtual string OPF_USER_ID { get; set; } // char(5)
        public virtual string OPF_NAME { get; set; } // char(30)
        public virtual string OPF_SW_SENDED { get; set; } // char(5)
        public virtual char? OPF_PRINT_CODE { get; set; } // char(1)
        public virtual char? OPF_SEND_MSG_CODE { get; set; } // char(1)
        public virtual DateTime? OPF_BEGIN_TIME { get; set; } // datetime
        public virtual char? OPF_CROSS_RUN_CODE { get; set; } // char(1)
        public virtual char? OPF_AUTORUN_MODE { get; set; } // char(1)
        public virtual char? OPF_AUTORUN_LOCK { get; set; } // char(1)
    }
}