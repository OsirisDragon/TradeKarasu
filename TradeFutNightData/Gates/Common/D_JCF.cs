using DataEngine;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_JCF : D_JCF<JCF>
    {
        public D_JCF(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_JCF<T> : ParentGate
    {
        public IEnumerable<JCF> ListNotStartWith(string compareStr)
        {
            var query = _das.DataConn.GetTable<JCF>().Where(c => !c.JCF_JOB_ID.StartsWith(compareStr));
            return query.ToList();
        }
    }
}