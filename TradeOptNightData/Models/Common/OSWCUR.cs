using CrossModel;
using LinqToDB.Mapping;

namespace TradeOptNightData.Models.Common
{
    public class OSWCUR : DtoParent<OSWCUR>
    {
        [PrimaryKey]
        public virtual sbyte OSWCUR_OSW_GRP { get; set; } // tinyint

        public virtual int? OSWCUR_CURR_OPEN_SW { get; set; } // int
        public virtual char? OSWCUR_FORCE_EXEC_FLAG { get; set; } // char(1)
        public virtual int? OSWCUR_MAX_OPEN_SW { get; set; } // int
        public virtual char? OSWCUR_CONDITION { get; set; } // char(1)
    }
}