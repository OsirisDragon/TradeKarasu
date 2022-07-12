using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_SLT : D_SLT<SLT>
    {
        public D_SLT(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_SLT<T> : ParentGate
    {
        public IList<SLT> ListAll()
        {
            var query = _das.DataConn.GetTable<SLT>()
                .OrderBy(c => c.SLT_KIND_ID).ThenBy(c => c.SLT_MIN).ThenBy(c => c.SLT_SPREAD_LONG);

            return query.ToList();
        }

        public IEnumerable<SLT> ListByKindID(string SLT_KIND_ID)
        {
            var query = _das.DataConn.GetTable<SLT>().Where(c => c.SLT_KIND_ID == SLT_KIND_ID)
                .OrderBy(c => c.SLT_KIND_ID).ThenBy(c => c.SLT_MIN).ThenBy(c => c.SLT_SPREAD_LONG);

            return query.ToList();
        }

        public void Update(IEnumerable<SLT> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<SLT>()
                    .Where(c => c.SLT_KIND_ID == item.OriginalData.SLT_KIND_ID && c.SLT_MAX == item.OriginalData.SLT_MAX && c.SLT_SPREAD_LONG == item.OriginalData.SLT_SPREAD_LONG)
                    .Set(c => c.SLT_KIND_ID, item.SLT_KIND_ID)
                    .Set(c => c.SLT_MAX, item.SLT_MAX)
                    .Set(c => c.SLT_MIN, item.SLT_MIN)
                    .Set(c => c.SLT_SPREAD, item.SLT_SPREAD)
                    .Set(c => c.SLT_SPREAD_LONG, item.SLT_SPREAD_LONG)
                    .Set(c => c.SLT_SPREAD_MULTI, item.SLT_SPREAD_MULTI)
                    .Set(c => c.SLT_SPREAD_MAX, item.SLT_SPREAD_MAX)
                    .Set(c => c.SLT_VALID_QNTY, item.SLT_VALID_QNTY)
                    .Set(c => c.SLT_PRICE_FLUC, item.SLT_PRICE_FLUC)
                    .Update();
            }
        }
    }
}