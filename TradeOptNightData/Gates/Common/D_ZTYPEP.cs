using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_ZTYPEP : D_ZTYPEP<ZTYPEP>
    {
        public D_ZTYPEP(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_ZTYPEP<T> : ParentGate
    {
        public IList<ZTYPEP> ListAll()
        {
            var query = _das.DataConn.GetTable<ZTYPEP>()
                .OrderBy(c => c.ZTYPEP_PROD);

            return query.ToList();
        }
    }
}