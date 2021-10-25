using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_MPAR : D_MPAR<MPAR>
    {
        public D_MPAR(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_MPAR<T> : ParentGate
    {
        public MPAR Get()
        {
            var query = _das.DataConn.GetTable<MPAR>();
            return query.FirstOrDefault();
        }
    }
}