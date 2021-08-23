using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_PRODLMT : D_PRODLMT<PRODLMT>
    {
        public D_PRODLMT(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_PRODLMT<T> : ParentGate
    {
        public IEnumerable<PRODLMT> ListAll()
        {
            var query = _das.DataConn.GetTable<PRODLMT>();
            return query.ToList();
        }
    }
}