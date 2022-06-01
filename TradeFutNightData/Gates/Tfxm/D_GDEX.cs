using DataEngine;
using System;
using System.Linq;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNightData.Gates.Tfxm
{
    public class D_GDEX : D_GDEX<GDEX>
    {
        public D_GDEX(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_GDEX<T> : ParentGate
    {
        public GDEX GetTopOne(int GDEX_ID, DateTime GDEX_DATE)
        {
            var query = _das.DataConn.GetTable<GDEX>().Where(c => c.GDEX_ID == GDEX_ID && c.GDEX_DATE == GDEX_DATE).Take(1);
            return query.FirstOrDefault();
        }
    }
}