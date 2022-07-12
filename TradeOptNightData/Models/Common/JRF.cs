using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Common
{
    public class JRF : DtoParent<JRF>
    {
        [PrimaryKey]
        public string JRF_ORIG_JOB_ID { get; set; } // char(7)
        [PrimaryKey]
        public char JRF_ORIG_JOB_TYPE { get; set; } // char(1)
        [PrimaryKey]
        public string JRF_SEQ_NO { get; set; } // char(3)
        public string JRF_UPD_JOB_ID { get; set; } // char(5)
        public char JRF_UPD_JOB_TYPE { get; set; } // char(1)
        public char JRF_SW_CODE { get; set; } // char(1)
    }
}
