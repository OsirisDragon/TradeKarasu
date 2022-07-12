using Dapper;
using DataEngine;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_BMF512 : D_BMF512<BMF512>
    {
        public D_BMF512(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_BMF512<T> : ParentGate
    {
    }
}