using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_XFCM : D_XFCM<XFCM>
    {
        public D_XFCM(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_XFCM<T> : ParentGate
    {
        public IList<XFCM> ListAll()
        {
            var query = _das.DataConn.GetTable<XFCM>()
                        .OrderBy(c => c.XFCM_FCM_NO)
                        .ThenBy(c => c.XFCM_SYS_ID)
                        .ThenBy(c => c.XFCM_SESSION_ID);

            return query.ToList();
        }
    }
}