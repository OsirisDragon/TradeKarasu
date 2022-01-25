using Dapper;
using DataEngine;
using LinqToDB;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_FMIF : D_FMIF<FMIF>
    {
        public D_FMIF(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_FMIF<T> : ParentGate
    {
        public int ClearSettlePriceByMarketClose(string pdkMarketClose)
        {
            var sql = @"
                        UPDATE FMIF SET FMIF_SETTLE_PRICE = 0
                        FROM PDK
                        WHERE substring(FMIF_PROD_ID, 1, 3) = PDK_KIND_ID
                        AND PDK_MARKET_CLOSE = @PDK_MARKET_CLOSE
                        ";

            var p = new DynamicParameters();
            p.Add("@PDK_MARKET_CLOSE", pdkMarketClose);

            var result = _das.Conn.Execute(BuildCommand<T>(sql, p));

            return result;
        }

        public int UpdateSettlePrice(string fmifProdId, decimal? fmifSettlePrice)
        {
            int affectedRows = -1;

            affectedRows = _das.DataConn.GetTable<FMIF>()
                .Where(c => c.FMIF_PROD_ID == fmifProdId)
                .Set(c => c.FMIF_SETTLE_PRICE, fmifSettlePrice)
                .Update();

            return affectedRows;
        }
    }
}