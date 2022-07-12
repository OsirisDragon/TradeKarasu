using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.Prefix5
{
    public class D_50305<T> : ParentGate
    {
        public D_50305(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT  PDK_SUBTYPE,
                                MWPLMT_KIND_ID,
                                PDK2ND_PRICE_FLUC,
                                PDK2ND_UNIT,
                                PDK2ND_UNIT_SPREAD,
                                MWPLMT_LIMIT,
                                MWPLMT_LIMIT_SPREAD,
                                00000.0000 AS ACTUALS_PRICE,
                                PDK_STOCK_ID
                        FROM MWPLMT, PDK2ND, PDK
                        WHERE MWPLMT_KIND_ID = PDK2ND_KIND_ID
                        AND PDK_KIND_ID = MWPLMT_KIND_ID
                        AND PDK_STATUS_CODE <>  '0'
                        ORDER BY PDK_SUBTYPE, MWPLMT_KIND_ID
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}