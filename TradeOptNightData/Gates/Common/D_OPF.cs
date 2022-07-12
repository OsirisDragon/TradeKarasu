using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_OPF : D_OPF<OPF>
    {
        public D_OPF(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_OPF<T> : ParentGate
    {
        public IEnumerable<OPF> ListAll()
        {
            var query = _das.DataConn.GetTable<OPF>().OrderBy(c => c.OPF_OP_NO);
            return query.ToList();
        }
    }
}