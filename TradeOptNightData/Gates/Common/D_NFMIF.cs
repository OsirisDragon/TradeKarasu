using DataEngine;
using LinqToDB;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_NFMIF : D_NFMIF<NFMIF>
    {
        public D_NFMIF(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_NFMIF<T> : ParentGate
    {
        public int UpdateSettlePrice(string prodId, decimal? prodSettlePrice)
        {
            int affectedRows = -1;

            affectedRows = _das.DataConn.GetTable<NFMIF>()
                .Where(c => c.NFMIF_PROD_ID == prodId)
                .Set(c => c.NFMIF_SETTLE_PRICE, prodSettlePrice)
                .Update();

            return affectedRows;
        }
    }
}