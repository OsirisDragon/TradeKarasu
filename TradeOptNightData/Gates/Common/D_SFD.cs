using DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_SFD : D_SFD<SFD>
    {
        public D_SFD(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_SFD<T> : ParentGate
    {
        public IList<SFD> ListByDate(DateTime date)
        {
            var query = _das.DataConn.GetTable<SFD>().Where(c => c.SFD_TRADE_DATE == date);

            return query.ToList();
        }
    }
}