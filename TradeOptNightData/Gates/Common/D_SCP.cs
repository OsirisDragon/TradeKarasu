using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_SCP : D_SCP<SCP>
    {
        public D_SCP(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_SCP<T> : ParentGate
    {
        public IList<SCP> ListAll()
        {
            var query = _das.DataConn.GetTable<SCP>();

            return query.ToList();
        }
    }
}