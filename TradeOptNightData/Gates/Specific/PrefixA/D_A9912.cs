using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.PrefixA
{
    public class D_A9912<T> : ParentGate
    {
        public D_A9912(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByDate(DateTime startDate, DateTime endDate, string prodId)
        {
            var sql = @"
                        SELECT 	A.*,
                                B.PDK_SUBTYPE
                        FROM PHALT A
                        LEFT JOIN PDK B ON A.PHALT_PROD_ID = B.PDK_KIND_ID
                        WHERE(
                                (@START_DATE IS NULL OR PHALT_TRADE_DATE >= @START_DATE)
                            AND (@END_DATE IS NULL OR PHALT_TRADE_DATE <= @END_DATE)
                        )
                        AND (@PROD_ID = '' OR PHALT_PROD_ID = @PROD_ID)
                        ORDER BY PHALT_TRADE_DATE DESC, PHALT_PROD_ID ASC, PHALT_TRADE_PAUSE_DATE DESC
                        ";

            var p = new DynamicParameters();
            p.Add("@START_DATE", startDate);
            p.Add("@END_DATE", endDate);
            p.Add("@PROD_ID", prodId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}