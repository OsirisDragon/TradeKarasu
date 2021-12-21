using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_TPPBP : D_TPPBP<TPPBP>
    {
        public D_TPPBP(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_TPPBP<T> : ParentGate
    {
        public void Update(IEnumerable<TPPBP> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<TPPBP>()
                    .Where(c => c.TPPBP_PROD_ID == item.OriginalData.TPPBP_PROD_ID)
                    .Set(c => c.TPPBP_THERICAL_P_REF, item.TPPBP_THERICAL_P_REF)
                    .Update();
            }
        }
    }
}