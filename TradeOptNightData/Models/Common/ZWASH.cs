using CrossModel;
using LinqToDB.Mapping;
using System;

namespace TradeOptNightData.Models.Common
{
    public class ZWASH : DtoParent<ZWASH>
    {
        [PrimaryKey]
        public virtual string ZWASH_COMB_PROD { get; set; } // char(8)

        public virtual DateTime ZWASH_W_TIME { get; set; } // datetime
    }
}