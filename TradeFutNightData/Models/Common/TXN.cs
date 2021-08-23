using System;
using CrossModel;
using LinqToDB.Mapping;

namespace TradeFutNightData.Models.Common
{
    public class TXN : DtoParent<TXN>
    {
        [PrimaryKey]
        public string TXN_ID;
        public string TXN_NAME;
        public string TXN_REMARK;
        public DateTime TXN_W_TIME;
        public string TXN_W_USER_ID;
        public string TXN_TYPE;
        public string TXN_DEFAULT;
        public int TXN_OSW_GRP;
    }
}
