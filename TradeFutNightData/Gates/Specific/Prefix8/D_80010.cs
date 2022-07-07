using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix8
{
    public class D_80010<T> : ParentGate
    {
        public D_80010(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByTxnId(string txnId)
        {
            var sql = @"
                      SELECT TXN.TXN_NAME,
                             UTP.UTP_USER_ID,
                             UTP.UTP_YN_CODE,
                             UTP.UTP_TXN_ID,
                             UPF.UPF_USER_NAME
                        FROM TXN,
                             UTP,
                             UPF
                       WHERE ( TXN.TXN_ID = UTP.UTP_TXN_ID )
                       AND ( UTP.UTP_USER_ID = UPF.UPF_USER_ID )
                       AND ( ( UTP.UTP_YN_CODE = 'Y' )
                       AND ( TXN.TXN_ID = @TXN_ID ) )
                       ORDER BY UTP_USER_ID
                        ";
            var p = new DynamicParameters();
            p.Add("@TXN_ID", txnId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<T> ListByUserId(string userId)
        {
            var sql = @"
                        SELECT  UTP.UTP_USER_ID,
                                UTP.UTP_TXN_ID,
                                TXN.TXN_NAME,
                                UPF.UPF_USER_NAME,
                                UPF.UPF_DEPT_ID
                        FROM TXN,
                             UTP,
                             UPF
                        WHERE TXN.TXN_ID = UTP.UTP_TXN_ID
                        AND UTP_YN_CODE = 'Y'
                        AND UTP.UTP_USER_ID = UPF.UPF_USER_ID
                        AND UPF_USER_ID =  @USER_ID
                        ORDER BY UTP_USER_ID, UTP_TXN_ID
                        ";
            var p = new DynamicParameters();
            p.Add("@USER_ID", userId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<T> ListByDptId(string dptId)
        {
            var sql = @"
                      SELECT UTP.UTP_USER_ID,
                             UTP.UTP_TXN_ID,
                             TXN.TXN_NAME,
                             UPF.UPF_USER_NAME,
                             UPF.UPF_DEPT_ID
                        FROM TXN,
                             UTP,
                             UPF
                       WHERE TXN.TXN_ID = UTP.UTP_TXN_ID
                             AND UTP_YN_CODE = 'Y'
                             AND UTP.UTP_USER_ID = UPF.UPF_USER_ID
                             AND UPF_DEPT_ID = @DPT_ID
                       ORDER BY UTP_USER_ID, UTP_TXN_ID
                        ";
            var p = new DynamicParameters();
            p.Add("@DPT_ID", dptId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}