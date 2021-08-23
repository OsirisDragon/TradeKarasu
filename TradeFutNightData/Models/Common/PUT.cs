using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class PUT : DtoParent<PUT>
    {
        [PrimaryKey]
        public string PUT_KIND_ID { get; set; } // char(4)
        [PrimaryKey]
        public decimal PUT_MAX { get; set; } // decimal(10, 4)
        public decimal PUT_MIN { get; set; } // decimal(10, 4)
        public decimal PUT_UNIT { get; set; } // decimal(10, 4)
    }
}
