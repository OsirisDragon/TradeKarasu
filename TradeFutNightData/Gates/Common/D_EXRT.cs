using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_EXRT : D_EXRT<EXRT>
    {
        public D_EXRT(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_EXRT<T> : ParentGate
    {
        public IEnumerable<EXRT> ListAll()
        {
            var query = _das.DataConn.GetTable<EXRT>().OrderBy(c => c.EXRT_CURRENCY_TYPE);
            return query.ToList();
        }
    }
}