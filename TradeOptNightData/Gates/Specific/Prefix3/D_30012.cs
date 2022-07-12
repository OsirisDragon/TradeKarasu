using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.Prefix3
{
    public class D_30012<T> : ParentGate
    {
        public D_30012(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT  PUT_KIND_ID,
                                PUT_MAX,
                                PUT_MIN,
                                PUT_UNIT
                        FROM PUT JOIN
                        (
                            SELECT DISTINCT PDK_PARAM_KEY FROM PDK
                            WHERE PDK_STATUS_CODE <> '0'
                        )B
                         ON PUT_KIND_ID = B.PDK_PARAM_KEY
                         ORDER BY PUT_KIND_ID, PUT_MIN
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}