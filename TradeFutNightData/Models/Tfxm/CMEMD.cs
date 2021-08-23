using System;
using CrossModel;

namespace TradeFutNightData.Models.Tfxm
{
    public class CMEMD : DtoParent<CMEMD>
    {
        public string CMEMD_SYMBOL { get; set; }
        public string CMEMD_CRT_MNTH { get; set; }
        public decimal CMEMD_PX { get; set; }
        public DateTime CMEMD_TRADE_DATE { get; set; }
        public DateTime CMEMD_UPDATE_TIME { get; set; }
    }
}
