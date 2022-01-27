using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class UPF : DtoParent<UPF>
    {
        [PrimaryKey]
        public virtual string UPF_USER_ID { get; set; } // char(5)

        public virtual string UPF_USER_NAME { get; set; } // char(10)
        public virtual char UPF_DEPT_ID { get; set; } // char(1)
        public virtual string UPF_PASSWORD { get; set; } // char(10)
        public virtual DateTime UPF_W_DATE { get; set; } // datetime
        public virtual string UPF_W_USER_ID { get; set; } // char(5)
        public virtual char UPF_CHANGE_FLAG { get; set; } // char(1)
        public virtual string UPF_USER_AD { get; set; } // char(30)
        public virtual string UPF_EMPLOYEE_ID { get; set; } // char(6)
    }
}