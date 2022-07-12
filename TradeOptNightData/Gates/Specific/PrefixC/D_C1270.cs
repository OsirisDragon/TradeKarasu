using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.PrefixC
{
    public class D_C1270<T> : ParentGate
    {
        public D_C1270(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByGrp(int oswGrp)
        {
            var sql = @"
                        SELECT  A.TPPBP_PROD_ID,
                                A.TPPBP_THERICAL_P,
                                A.TPPBP_THERICAL_P_REF,
                                B.PDK_KIND_ID,
                                B.PDK_STOCK_ID,
                                B.PDK_UNDERLYING_MARKET,
                                B.PDK_SUBTYPE,
                                C.PROD_SETTLE_DATE,
                                NULL AS ACTUALS_CLOSE_PRICE,
                                '          ' AS ACTUALS_CLOSE_PRICE_DATE,
                                0 AS COMPUTE_SUBTRACT
                        FROM TPPBP A
                        LEFT JOIN PDK B ON SUBSTRING(A.TPPBP_PROD_ID, 1, 3) = B.PDK_KIND_ID
                        LEFT JOIN PROD C ON A.TPPBP_PROD_ID = C.PROD_ID
                        LEFT JOIN PGRP D ON SUBSTRING(A.TPPBP_PROD_ID, 1, 2) = D.PGRP_KIND_ID
                        WHERE PROD_EXPIRE_CODE <> 'Y'
                        AND (@OSW_GRP = 0 OR PGRP_OSW_GRP = @OSW_GRP)
                        ORDER BY SUBSTRING(TPPBP_PROD_ID, 1, 3) ASC, PROD_SETTLE_DATE ASC
                        ";

            var p = new DynamicParameters();
            p.Add("@OSW_GRP", oswGrp);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}