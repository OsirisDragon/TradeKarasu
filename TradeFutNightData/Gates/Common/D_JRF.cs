using Dapper;
using DataEngine;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_JRF : D_JRF<JRF>
    {
        public D_JRF(DalSession das) { this._das = das; }
    }

    public class D_JRF<T> : ParentGate
    {
        public JRF GetTopOne(string JRF_ORIG_JOB_ID)
        {
            var query = _das.DataConn.GetTable<JRF>().Where(c => c.JRF_ORIG_JOB_ID == JRF_ORIG_JOB_ID).Take(1);
            return query.FirstOrDefault();
        }
    }
}
