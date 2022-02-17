using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix5
{
    public class D_50307<T> : ParentGate
    {
        public D_50307(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT  CASE WHEN PGRP_DSP_CONFIRM = 'N' THEN 0 ELSE PROD_SETTLE_PRICE END AS PROD_SETTLE_PRICE,
                                CLSPRC_OPEN_INTEREST,
                                PROD_ID_OUT,
                                PROD_SETTLE_DATE
                        FROM CLSPRC, PROD, PGRP
                        WHERE (CLSPRC_PROD_ID = PROD_ID)
                        AND PGRP_KIND_ID = SUBSTRING(PROD_ID, 1, 2)
                        ORDER BY PROD_ID_OUT, PROD_SETTLE_DATE
                        AT ISOLATION 0
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}