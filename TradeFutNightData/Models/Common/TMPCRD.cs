using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class TMPCRD : DtoParent<TMPCRD>
    {
        [PrimaryKey]
        public virtual string TMPCRD_USER_ID { get; set; } // char(7)

        public virtual string TMPCRD_TXT_CODE { get; set; }
        public virtual byte[] TMPCRD_BIN_CODE { get; set; }
    }
}