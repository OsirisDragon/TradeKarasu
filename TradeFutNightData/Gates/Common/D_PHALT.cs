using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_PHALT : D_PHALT<PHALT>
    {
        public D_PHALT(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_PHALT<T> : ParentGate
    {
        public IList<PHALT> ListByDate(DateTime startDate, DateTime endDate)
        {
            var query = _das.DataConn.GetTable<PHALT>()
                .Where(c => c.PHALT_TRADE_DATE >= startDate &&
                          c.PHALT_TRADE_DATE <= endDate &&
                          c.PHALT_TYPE == 'T')
                .OrderByDescending(c => c.PHALT_TRADE_DATE).ThenByDescending(c => c.PHALT_W_TIME);

            return query.ToList();
        }
    }
}