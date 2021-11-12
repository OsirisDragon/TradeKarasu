using Dapper;
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
        public IEnumerable<JCF> ListByID(string JCF_JOB_ID)
        {
            var query = _das.DataConn.GetTable<JCF>().Where(c => !c.JCF_JOB_ID.Contains(JCF_JOB_ID));
            return query.ToList();
        }
    }
}