using CrossModel;
using System;

namespace TradeOptNightData.Models.Tfxm
{
    public class GDEX : DtoParent<GDEX>
    {
        public virtual int? GDEX_ID { get; set; } // int
        public virtual DateTime? GDEX_DATE { get; set; } // datetime
        public virtual decimal? GDEX_FIXAM { get; set; } // money
        public virtual decimal? GDEX_FIXPM { get; set; } // money
        public virtual decimal? GDEX_FIXAMNT { get; set; } // money
        public virtual decimal? GDEX_FIXPMNT { get; set; } // money
        public virtual decimal? GDEX_EX_RATE { get; set; } // money
        public virtual string GDEX_RIC { get; set; } // varchar(20)
    }
}