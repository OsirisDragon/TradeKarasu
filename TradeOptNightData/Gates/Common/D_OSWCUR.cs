using DataEngine;
using LinqToDB;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_OSWCUR : D_OSWCUR<OSWCUR>
    {
        public D_OSWCUR(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_OSWCUR<T> : ParentGate
    {
        public int GetCurrOpenSwByGrp(int oswGrp)
        {
            var query = _das.DataConn.GetTable<OSWCUR>()
                .Where(c => c.OSWCUR_OSW_GRP == oswGrp)
                .Select(c => (int)c.OSWCUR_CURR_OPEN_SW).FirstOrDefault();

            return query;
        }
    }
}