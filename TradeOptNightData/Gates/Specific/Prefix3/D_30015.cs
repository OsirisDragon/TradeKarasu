using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.Prefix3
{
    public class D_30015<T> : ParentGate
    {
        public D_30015(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT  BLOT_KIND_ID,
                                BLOT_MAX,
                                BLOT_MIN,
                                BLOT_MIN_QNTY
                        FROM BLOT
                        JOIN
                        (
                            SELECT DISTINCT PDK_PARAM_KEY
                            FROM PDK WHERE PDK_STATUS_CODE <> '0'
                        ) B
                        ON BLOT_KIND_ID = B.PDK_PARAM_KEY
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}