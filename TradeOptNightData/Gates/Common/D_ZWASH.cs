using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_ZWASH : D_ZWASH<ZWASH>
    {
        public D_ZWASH(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_ZWASH<T> : ParentGate
    {
        public IList<ZWASH> ListAll()
        {
            var query = _das.DataConn.GetTable<ZWASH>();

            return query.ToList();
        }
    }
}