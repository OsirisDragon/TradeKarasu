using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Common
{
    public class JSW : DtoParent<JSW>
    {
        [PrimaryKey]
        public string JSW_ID { get; set; } // char(5)
        [PrimaryKey]
        public char JSW_TYPE { get; set; } // char(1)
        public char? JSW_SW_CODE { get; set; } // char(1)
    }
}
