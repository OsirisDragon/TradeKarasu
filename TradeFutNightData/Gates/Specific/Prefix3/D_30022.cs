using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix3
{
    public class D_30022<T> : ParentGate
    {
        public D_30022(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT  PMF_KIND_ID,
                                PMF_TYPE,
                                PMF_MONTH,
                                PMF_END_DATE,
                                CASE WHEN PMF_KIND_ID = 'STF' OR PMF_KIND_ID = 'ETF' THEN PMF_VALID_DATE ELSE NULL END AS PMF_VALID_DATE,
                                PMF_BEGIN_DATE,
                                PMF_DELIVERY_DATE,
                                PMF_REF_PRICE,
                                PMF_USER_ID,
                                PMF_W_TIME,
                                PMF_T1_DATE,
                                PMF_COMPOUND
                        FROM PMF JOIN
                        (
                            SELECT DISTINCT PDK_PARAM_KEY
                            FROM PDK
                            WHERE PDK_STATUS_CODE <> '0'
                        )B ON PMF_KIND_ID = B.PDK_PARAM_KEY
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}