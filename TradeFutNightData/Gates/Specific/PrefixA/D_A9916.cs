using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.PrefixA
{
    public class D_A9916<T> : ParentGate
    {
        public D_A9916(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByDate(DateTime START_DATE, DateTime END_DATE, string PROD_ID)
        {
            var sql = @"
                                    SELECT 	A.*,
                                                    B.PDK_SUBTYPE
                                    FROM TPPHALT A
                                    LEFT JOIN PDK B ON SUBSTRING(A.TPPHALT_PROD_ID, 1, 3) = B.PDK_KIND_ID
                                    WHERE (
                                                        (@START_DATE  IS NULL OR TPPHALT_TRADE_DATE >= @START_DATE)
                                              AND (@END_DATE  IS NULL OR TPPHALT_TRADE_DATE <= @END_DATE )
                                    )
                                    AND ( @PROD_ID = '' OR TPPHALT_PROD_ID LIKE @PROD_ID )
                                    ORDER BY TPPHALT_TRADE_DATE ASC, TPPHALT_W_TIME ASC
                                    ";

            var p = new DynamicParameters();
            p.Add("@START_DATE", START_DATE);
            p.Add("@END_DATE", END_DATE);
            p.Add("@PROD_ID", PROD_ID);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}