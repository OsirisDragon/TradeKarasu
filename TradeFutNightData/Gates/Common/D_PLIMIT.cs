using Dapper;
using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_PLIMIT : D_PLIMIT<PLIMIT>
    {
        public D_PLIMIT(DalSession das) { this._das = das; }
    }

    public class D_PLIMIT<T>: ParentGate
    {
        public IEnumerable<PLIMIT> ListAll()
        {
            var query = _das.DataConn.GetTable<PLIMIT>();
            return query.ToList();
        }
    }
}
