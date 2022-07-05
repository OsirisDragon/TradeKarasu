using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix8
{
    public class D_89010<T> : ParentGate
    {
        public D_89010(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByTxnId(string ytxnId)
        {
            var sql = @"
                        SELECT  YTXN.YTXN_NAME,
                                YUTP.YUTP_USER_ID,
                                YUTP.YUTP_YN_CODE,
                                YUTP.YUTP_YTXN_ID,
                                UPF.UPF_USER_NAME
                        FROM YTXN,
                             YUTP,
                             UPF
                        WHERE ( YTXN.YTXN_ID = YUTP.YUTP_YTXN_ID )
                        AND ( YUTP.YUTP_USER_ID = UPF.UPF_USER_ID )
                        AND ( ( YUTP.YUTP_YN_CODE = 'Y' )
                        AND ( YTXN.YTXN_ID = @YTXN_ID ) )
                        ORDER BY YUTP_USER_ID
                        ";
            var p = new DynamicParameters();
            p.Add("@YTXN_ID", ytxnId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<T> ListByUserId(string userId)
        {
            var sql = @"
                        SELECT  YUTP.YUTP_USER_ID,
                                YUTP.YUTP_YTXN_ID,
                                YTXN.YTXN_NAME,
                                UPF.UPF_USER_NAME,
                                UPF.UPF_DEPT_ID
                        FROM YTXN,
                             YUTP,
                             UPF
                        WHERE YTXN.YTXN_ID = YUTP.YUTP_YTXN_ID
                        AND YUTP_YN_CODE = 'Y'
                        AND YUTP.YUTP_USER_ID = UPF.UPF_USER_ID
                        AND UPF_USER_ID =  @USER_ID
                        ORDER BY YUTP_USER_ID, YUTP_YTXN_ID
                        ";
            var p = new DynamicParameters();
            p.Add("@USER_ID", userId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<T> ListByDptId(string dptId)
        {
            var sql = @"
                      SELECT YUTP.YUTP_USER_ID,
                             YUTP.YUTP_YTXN_ID,
                             YTXN.YTXN_NAME,
                             UPF.UPF_USER_NAME,
                             UPF.UPF_DEPT_ID
                        FROM YTXN,
                             YUTP,
                             UPF
                       WHERE YTXN.YTXN_ID = YUTP.YUTP_YTXN_ID
                             AND YUTP_YN_CODE = 'Y'
                             AND YUTP.YUTP_USER_ID = UPF.UPF_USER_ID
                             AND UPF_DEPT_ID = @DPT_ID
                       ORDER BY YUTP_USER_ID, YUTP_YTXN_ID
                        ";
            var p = new DynamicParameters();
            p.Add("@DPT_ID", dptId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}