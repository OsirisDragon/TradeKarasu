using Dapper;
using DataEngine;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_OCF : D_OCF<OCF>
    {
        public D_OCF(DalSession das) { this._das = das; }
    }

    public class D_OCF<T> : ParentGate
    {
        public OCF Get()
        {
            var query = _das.DataConn.GetTable<OCF>();
            return query.SingleOrDefault();
        }
    }
}
