using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class MPD : DtoParent<MPD>
    {
        [PrimaryKey]
        public virtual string MPD_FCM_NO { get; set; } // char(7)

        [PrimaryKey]
        public virtual string MPD_ACC_NO { get; set; } // char(7)

        [PrimaryKey]
        public virtual string MPD_PROD_ID { get; set; } // char(20)

        public virtual string MPD_B_FCM_NO { get; set; } // char(7)

        public virtual string MPD_B_ACC_NO { get; set; } // char(7)
    }
}