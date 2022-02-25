using Dapper;
using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_BRK : D_BRK<BRK>
    {
        public D_BRK(DalSession das) : base(das)

        {
            this._das = das;
        }
    }

    public class D_BRK<T> : ParentGate
    {
        public D_BRK(DalSession das)
        {
            this._das = das;
        }

        public IList<BRK> ListAll()
        {
            var query = _das.DataConn.GetTable<BRK>()
                .OrderBy(c => c.BRK_NO);

            return query.ToList();
        }

        public IEnumerable<T> ListByBrkNo(string BRK_NO)
        {
            var sql = @"
                        SELECT  BRK_NO,
                                BRK_NAME,

                                CASE BRK_TYPE
                                    WHEN '1' THEN '經紀'
                                    WHEN '2' THEN '自營'
                                    WHEN '3' THEN '兼營期貨'
                                    WHEN '4' THEN '兼營股價期'
                                ELSE 'OTHER'
                                END AS BRK_TYPE,

                                CASE BRK_OPEN_CODE_F
                                    WHEN 'Y' THEN '可營業'
                                    WHEN 'N' THEN '不可營業'
                                ELSE 'OTHER'
                                END AS BRK_OPEN_CODE_F,

                                CASE BRK_OPEN_CODE_O
                                    WHEN 'Y' THEN '可營業'
                                    WHEN 'N' THEN '不可營業'
                                ELSE 'OTHER'
                                END AS BRK_OPEN_CODE_O,

                                CASE BRK_OPEN_CODE_3
                                    WHEN 'Y' THEN '可營業'
                                    WHEN 'N' THEN '不可營業'
                                ELSE 'OTHER'
                                END AS BRK_OPEN_CODE_3,

                                BRK_CRE_DATE
                        FROM BRK
                        WHERE (BRK_OPEN_CODE_F = 'Y' OR BRK_OPEN_CODE_O='Y' OR BRK_OPEN_CODE_3='Y')
                        AND BRK_NO LIKE @BRK_NO
                        ORDER BY BRK_NO
                        ";

            var p = new DynamicParameters();
            p.Add("@BRK_NO", $"%{BRK_NO}%");

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}