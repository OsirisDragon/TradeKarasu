using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_TPPADJ : D_TPPADJ<TPPADJ>
    {
        public D_TPPADJ(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_TPPADJ<T> : ParentGate
    {
        public IList<TPPADJ> ListAll()
        {
            var query = _das.DataConn.GetTable<TPPADJ>();

            return query.ToList();
        }
    }
}