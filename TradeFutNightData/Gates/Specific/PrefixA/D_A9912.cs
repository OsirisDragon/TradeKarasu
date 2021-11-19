using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.PrefixA
{
    public class D_A9912<T> : ParentGate
    {
        public D_A9912(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByDate(DateTime START_DATE, DateTime END_DATE, string PROD_ID)
        {
            var sql = @"
SELECT 	A.*,
			'' AS op_type,
			B.PDK_SUBTYPE
FROM PHALT A
LEFT JOIN PDK B ON A.PHALT_PROD_ID = B.PDK_KIND_ID
WHERE
( (@START_DATE  IS NULL OR PHALT_TRADE_DATE >= @START_DATE) AND (@END_DATE IS NULL OR PHALT_TRADE_DATE <= @END_DATE) )
AND ( @PROD_ID = '' OR PHALT_PROD_ID = @PROD_ID )
ORDER BY PHALT_TRADE_DATE DESC, PHALT_PROD_ID ASC, PHALT_TRADE_PAUSE_DATE DESC
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