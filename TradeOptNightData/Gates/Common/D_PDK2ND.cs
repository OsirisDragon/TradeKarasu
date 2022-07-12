using DataEngine;
using LinqToDB;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_PDK2ND : D_PDK2ND<PDK2ND>
    {
        public D_PDK2ND(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_PDK2ND<T> : ParentGate
    {
        public PDK2ND GetByKindId(string kindId)
        {
            var query = _das.DataConn.GetTable<PDK2ND>()
                .Where(c => Sql.Like(c.PDK2ND_KIND_ID, kindId + "%"));

            return query.SingleOrDefault();
        }
    }
}