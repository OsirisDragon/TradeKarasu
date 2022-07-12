using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.Prefix5
{
    public class D_50306<T> : ParentGate
    {
        public D_50306(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT
                                TPPLMT_PROD_ID,
                                TPPLMT_FIRST_PROD,
                                PDK_SUBTYPE,
                                PDK_KIND_ID,
                                PROD_SETTLE_DATE,
                                PROD_SETTLE_DATE_SECOND = PROD_SETTLE_DATE ,
                                PDK_STOCK_ID,
                                00000.0000 as TARGET_PRICE,
                                TPPINTD_UNIT,
                                TPPLMT_LIMIT
                        FROM TPPLMT A
                        LEFT JOIN PDK B ON PDK_KIND_ID=SUBSTRING(TPPLMT_PROD_ID, 1, 3)
                        LEFT JOIN PROD C ON A.TPPLMT_PROD_ID = C.PROD_ID
                        JOIN TPPINTD T ON T.TPPINTD_FIRST_KIND_ID = PDK_KIND_ID AND T.TPPINTD_FIRST_MONTH = PROD_SPREAD_CODE AND T.TPPINTD_SECOND_MONTH = 0

                        UNION

                        SELECT
                                TPPLMT_PROD_ID,
                                TPPLMT_FIRST_PROD,
                                PDK_SUBTYPE,
                                PDK_KIND_ID,
                                P1.PROD_SETTLE_DATE,
                                P2.PROD_SETTLE_DATE,
                                PDK_STOCK_ID,
                                00000.0000 as TARGET_PRICE,
                                TPPINTD_UNIT,
                                TPPLMT_LIMIT
                        FROM TPPLMT A
                        LEFT JOIN PDK B ON PDK_KIND_ID=SUBSTRING(TPPLMT_PROD_ID, 1, 3)
                        LEFT JOIN
                        (SELECT SODF_PROD_ID, SODF_FIRST_PROD, SODF_SECOND_PROD FROM SODF GROUP BY SODF_PROD_ID, SODF_FIRST_PROD, SODF_SECOND_PROD) C ON A.TPPLMT_PROD_ID = C.SODF_PROD_ID
                        LEFT JOIN PROD P1 ON P1.PROD_ID = C.SODF_FIRST_PROD
                        LEFT JOIN PROD P2 ON P2.PROD_ID = C.SODF_SECOND_PROD
                        JOIN TPPINTD T ON
                        (
                            T.TPPINTD_FIRST_KIND_ID = SUBSTRING(C.SODF_FIRST_PROD,1,3) AND T.TPPINTD_SECOND_KIND_ID = SUBSTRING(C.SODF_SECOND_PROD,1,3) AND
                            T.TPPINTD_FIRST_MONTH = P1.PROD_SPREAD_CODE AND T.TPPINTD_SECOND_MONTH = P2.PROD_SPREAD_CODE AND T.TPPINTD_SECOND_MONTH <> 0
                        )
                        ORDER BY PDK_SUBTYPE ASC, PDK_KIND_ID ASC, TPPLMT_FIRST_PROD ASC, PROD_SETTLE_DATE, PROD_SETTLE_DATE_SECOND ASC
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}