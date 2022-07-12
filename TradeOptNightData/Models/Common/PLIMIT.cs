using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Common
{
    public class PLIMIT : DtoParent<PLIMIT>
    {
        [PrimaryKey]
        public string PLIMIT_PROD_ID { get; set; } // char(10)
        public byte PLIMIT_RAISE_CODE { get; set; } // tinyint
        public byte PLIMIT_FALL_CODE { get; set; } // tinyint
    }
}
