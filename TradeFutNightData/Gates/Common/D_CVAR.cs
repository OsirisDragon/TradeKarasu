using DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_CVAR : D_CVAR<CVAR>
    {
        public D_CVAR(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_CVAR<T> : ParentGate
    {
        public IList<CVAR> ListByDate(DateTime CVAR_BASE_DATE)
        {
            var query = _das.DataConn.GetTable<CVAR>()
                .Where(c => c.CVAR_BASE_DATE == CVAR_BASE_DATE &&
                            c.CVAR_VAR_CODE == '2' &&
                            c.CVAR_CONFIRM_CODE == 'Y');

            return query.ToList();
        }
    }
}