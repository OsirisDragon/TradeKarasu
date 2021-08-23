using Dapper;
using DataEngine;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_FRQ : D_FRQ<FRQ>
    {
        public D_FRQ(DalSession das) { this._das = das; }
    }

    public class D_FRQ<T> : ParentGate
    {
       
    }
}
