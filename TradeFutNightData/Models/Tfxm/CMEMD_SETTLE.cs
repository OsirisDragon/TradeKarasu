using System;
using CrossModel;

namespace TradeFutNightData.Models.Tfxm
{
    public class CMEMD_SETTLE : DtoParent<CMEMD_SETTLE>
    {
        public string CMEMD_SETTLE_SYMBOL { get; set; }
        public string CMEMD_SETTLE_CRT_MNTH { get; set; }
        public decimal CMEMD_SETTLE_PX { get; set; }
        public DateTime CMEMD_SETTLE_TRADE_DATE { get; set; }
        public DateTime CMEMD_SETTLE_UPDATE_TIME { get; set; }
    }
}
