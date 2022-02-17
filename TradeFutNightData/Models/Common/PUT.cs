using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class PUT : DtoParent<PUT>
    {
        [PrimaryKey]
        public virtual string PUT_KIND_ID { get; set; } // char(4)

        [PrimaryKey]
        public virtual decimal PUT_MAX { get; set; } // decimal(10, 4)

        public virtual decimal PUT_MIN { get; set; } // decimal(10, 4)
        public virtual decimal PUT_UNIT { get; set; } // decimal(10, 4)
    }
}