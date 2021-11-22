using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.PrefixA
{
    public class D_A9919<T> : ParentGate
    {
        public D_A9919(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByDate(DateTime START_DATE, DateTime END_DATE, string KIND_ID)
        {
            var sql = @"
                                    SELECT 	A.*,
			                                        B.PDK_SUBTYPE
                                    FROM MHALT A
                                    LEFT JOIN PDK B ON A.MHALT_KIND_ID = B.PDK_KIND_ID
                                    WHERE (
                                                        (@START_DATE  IS NULL OR MHALT_TRADE_DATE >= @START_DATE)
                                             AND (@END_DATE IS NULL OR MHALT_TRADE_DATE <= @END_DATE)
                                    )
                                    AND ( @KIND_ID = '' OR MHALT_KIND_ID = @KIND_ID )
                                    ORDER BY MHALT_TRADE_DATE DESC, MHALT_KIND_ID ASC, MHALT_PAUSE_DATE DESC
                                    ";

            var p = new DynamicParameters();
            p.Add("@START_DATE", START_DATE);
            p.Add("@END_DATE", END_DATE);
            p.Add("@KIND_ID", KIND_ID);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}