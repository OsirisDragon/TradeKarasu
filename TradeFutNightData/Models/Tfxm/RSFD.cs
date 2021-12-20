using CrossModel;
using System;

namespace TradeFutNightData.Models.Tfxm
{
    public class RSFD : DtoParent<RSFD>
    {
        public virtual string RSFD_SID { get; set; } // varchar(10)
        public virtual decimal? RSFD_CLOSE_PRICE { get; set; } // money
        public virtual DateTime? RSFD_DATE { get; set; } // datetime
    }
}