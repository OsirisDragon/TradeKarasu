using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeFutNightData.Models.Common
{
    public class TXN : DtoParent<TXN>
    {
        [PrimaryKey]
        public virtual string TXN_ID { get; set; }

        public virtual string TXN_NAME { get; set; }
        public virtual string TXN_REMARK { get; set; }
        public virtual DateTime TXN_W_TIME { get; set; }
        public virtual string TXN_W_USER_ID { get; set; }
        public virtual string TXN_TYPE { get; set; }
        public virtual string TXN_DEFAULT { get; set; }
        public virtual int? TXN_OSW_GRP { get; set; }

        public override string ToString()
        {
            return TXN_ID + " " + TXN_NAME;
        }
    }
}