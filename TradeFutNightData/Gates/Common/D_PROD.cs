using Dapper;
using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_PROD : D_PROD<PROD>
    {
        public D_PROD(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_PROD<T> : ParentGate
    {
        public IEnumerable<T> ListByKindIDAndMonth(string kindID, string month)
        {
            var sql = @"
                        SELECT *
                        FROM PROD A
                        LEFT JOIN PDK B ON SUBSTRING(PROD_ID,1,3) = B.PDK_KIND_ID
                        WHERE (SUBSTRING(PROD_ID,1,3) LIKE @KIND_ID)
                        AND (PROD_SETTLE_DATE LIKE @PROD_SETTLE_DATE)
                        AND (B.PDK_STATUS_CODE <> '0')
                        ORDER BY PROD_ID_OUT, PROD_SETTLE_DATE
                        ";

            var p = new DynamicParameters();
            p.Add("@KIND_ID", kindID + "%");
            p.Add("@PROD_SETTLE_DATE", month + "%");

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<T> ListDistinctMonth()
        {
            var sql = @"
                        SELECT DISTINCT PROD_SETTLE_DATE
                        FROM PROD A
                        LEFT JOIN PDK B ON SUBSTRING(PROD_ID, 1, 3) = PDK_KIND_ID
                        WHERE PDK_STATUS_CODE <> '0'
                        ORDER BY PROD_SETTLE_DATE
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql));

            return result;
        }

        public int UpdateSettlePrice(string prodId, decimal? prodSettlePrice)
        {
            int affectedRows = -1;

            affectedRows = _das.DataConn.GetTable<PROD>()
                    .Where(c => c.PROD_ID == prodId)
                    .Set(c => c.PROD_SETTLE_PRICE, prodSettlePrice)
                    .Update();

            return affectedRows;
        }
    }
}