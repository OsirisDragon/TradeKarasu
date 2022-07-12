using Dapper;
using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_CLSPRC : D_CLSPRC<CLSPRC>
    {
        public D_CLSPRC(DalSession das) { this._das = das; }
    }

    public class D_CLSPRC<T> : ParentGate
    {
        public IEnumerable<CLSPRC> ListAll()
        {
            return _das.DataConn.GetTable<CLSPRC>().ToList();
        }
    }
}
