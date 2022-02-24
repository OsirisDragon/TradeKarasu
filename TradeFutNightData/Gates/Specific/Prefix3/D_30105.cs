using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix3
{
    public class D_30105<T> : ParentGate
    {
        public D_30105(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"SELECT  A.*,
                                (RTRIM(MPD_PROD_ID) || ' (' || RTRIM(PDK_NAME) || ')') AS PROD_DISPLAY,
                                (RTRIM(MPD_FCM_NO) || '—' ||  RTRIM(FCM_NAME)) AS FCM_DISPLAY
                        FROM (
                                SELECT  MPD_FCM_NO,
                                        MPD_ACC_NO,
                                        MPD_PROD_ID,
                                        ISNULL( (SELECT
                                                        min(CASE PDK_SUBTYPE
                                                        WHEN 'S' THEN  'S'
                                                        ELSE PDK.PDK_NAME
                                                        END)
                                                 FROM PDK
                                                 WHERE SUBSTRING(PDK_PARAM_KEY, 1, 3) = SUBSTRING(MPD.MPD_PROD_ID, 1, 3)
                                                ), MPD_PROD_ID
                                        ) AS PDK_NAME,
                                        (SELECT BRK_ABBR_NAME
                                        FROM BRK
                                        WHERE BRK_NO = MPD.MPD_FCM_NO) AS FCM_NAME
                                FROM MPD JOIN
                                (
                                    SELECT DISTINCT PDK_PARAM_KEY
                                    FROM PDK
                                    WHERE PDK_STATUS_CODE <> '0'
                                )B ON MPD_PROD_ID = B.PDK_PARAM_KEY
                        )A
                        ORDER BY MPD_FCM_NO, MPD_ACC_NO, MPD_PROD_ID
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }

        public IEnumerable<T> ListPDK()
        {
            var sql = @"SELECT  DISTINCT MPD_PROD_ID,
                                CASE WHEN MPD_PROD_ID = 'XXX' THEN 1 ELSE 99 END  AS 'SORT',
                                (RTRIM(A.MPD_PROD_ID) + CASE WHEN A.MPD_PROD_ID = 'ETF' OR A.MPD_PROD_ID = 'STF' THEN '股票' ELSE B.PDK_NAME END) AS PDK_NAME
                        FROM MPD A
                        LEFT JOIN PDK B ON A.MPD_PROD_ID = B.PDK_KIND_ID
                        ORDER BY SORT, MPD_PROD_ID
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}