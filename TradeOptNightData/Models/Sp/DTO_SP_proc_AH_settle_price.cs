using System;
using CrossModel;

namespace TradeOptNightData.Models.Sp
{
    public class DTO_SP_proc_AH_settle_price : DtoParent<DTO_SP_proc_AH_settle_price>
    {
        public string tmpprc_id_out { get; set; }
        public string tmpprc_settle_date { get; set; }
        public decimal tmpprc_clsprc_settle_price { get; set; }
        public decimal tmpprc_last_price { get; set; }
        public decimal tmpprc_bo_price { get; set; }
        public decimal tmpprc_so_price { get; set; }
        public decimal tmpprc_diff_price { get; set; }
        public decimal tmpprc_adjust_price { get; set; }
        public string tmpprc_adjust_type { get; set; }
        public decimal tmpprc_bs_price { get; set; }
    }
}