using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class UPF : DtoParent<UPF>
    {
        [PrimaryKey]
        public string UPF_USER_ID { get; set; } // char(5)
        public string UPF_USER_NAME { get; set; } // char(10)
        public char UPF_DEPT_ID { get; set; } // char(1)
        public string UPF_PASSWORD { get; set; } // char(10)
        public DateTime UPF_W_DATE { get; set; } // datetime
        public string UPF_W_USER_ID { get; set; } // char(5)
        public char UPF_CHANGE_FLAG { get; set; } // char(1)
        public string UPF_EMPLOYEE_ID { get; set; } // char(6)
    }
}
