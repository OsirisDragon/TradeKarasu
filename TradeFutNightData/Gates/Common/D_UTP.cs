using DataEngine;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_UTP : D_UTP<UTP>
    {
        public D_UTP(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_UTP<T> : ParentGate
    {
    }
}