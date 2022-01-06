using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix5
{
    public class D_50304<T> : ParentGate
    {
        public D_50304(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT  MPR_ORIG_TIME,
                                PROD_ID_OUT,
                                MPR_M_PRICE,
                                MPR_M_QNTY,
                                PROD_SETTLE_DATE
                        FROM MPR, PROD
                        WHERE MPR_PROD_ID = PROD_ID
                        AND MPR_M_QNTY <> 0
                        ORDER BY PROD_ID_OUT, PROD_SETTLE_DATE, MPR_ORIG_TIME
                        AT ISOLATION 0
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}