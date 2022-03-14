using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix9
{
    public class D_90209<T> : ParentGate
    {
        public D_90209(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT  BRK AS BROKER_ID ,
                                XTCS_COUNT,
                                'F' AS SYS_TYPE
                        FROM
                        (
                            SELECT  BRK_NO AS BRK
                            FROM BRK
                            WHERE BRK_OPEN_CODE='Y'
                            AND SUBSTRING(BRK_NO,5,3) = '000'
                            GROUP BY BRK_NO

                            UNION ALL

                            SELECT SUBSTRING(BRK_NO,1,4) + '999' AS BRK
                            FROM BRK
                            WHERE SUBSTRING(BRK_NO,1,4) NOT IN
                            (
                                SELECT  SUBSTRING(BRK_NO,1,4)
                                FROM BRK WHERE BRK_OPEN_CODE='Y'
                                AND SUBSTRING(BRK_NO,5,3) = '000'
                                GROUP BY  SUBSTRING(BRK_NO,1,4)
                            )
                            GROUP BY SUBSTRING(BRK_NO,1,4)
                        )M ,
                        (
                            SELECT  SUBSTRING(XFCM_FCM_NO,1,4) AS XTCS_BRK,
                                    COUNT(XFCM_PORT_NO) AS XTCS_COUNT,
                                    'F' AS SYS_TYPE
                            FROM XFCM
                            WHERE XFCM_PROTOCOL <> '5'
                            AND XFCM_START_FLAG IN ('s', 'Y')
                            AND XFCM_SESSION_ID <= 900
                            GROUP BY SUBSTRING(XFCM_FCM_NO,1,4)
                        )N
                        WHERE SUBSTRING(M.BRK,1,4) = N.XTCS_BRK
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}