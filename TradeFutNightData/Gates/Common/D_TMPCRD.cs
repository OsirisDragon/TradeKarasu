using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_TMPCRD : D_TMPCRD<TMPCRD>
    {
        public D_TMPCRD(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_TMPCRD<T> : ParentGate
    {
        public IList<TMPCRD> ListById(string id)
        {
            var query = _das.DataConn.GetTable<TMPCRD>().Where(c => c.TMPCRD_USER_ID == id);

            return query.ToList();
        }
    }
}