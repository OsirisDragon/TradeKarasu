using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.Prefix8
{
    public class D_80002<T> : ParentGate
    {
        public D_80002(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List(string dptId)
        {
            var sql = @"
                        SELECT
                                UPF_USER_ID,
                                UPF_USER_NAME,
                                UPF_DEPT_ID,
                                UPF_W_DATE,
                                UPF_W_USER_ID,
                                UPF_PASSWORD,
                                UPF_EMPLOYEE_ID,
                                UPF_USER_AD,
                                UPFCRD_CARD_NO
                        FROM UPF A
                        LEFT JOIN UPFCRD B ON A.UPF_USER_ID = B.UPFCRD_USER_ID AND B.UPFCRD_CARD_TYPE = 'N'
                        WHERE ( UPF_DEPT_ID like @DPT_ID )
                        ORDER BY UPF_DEPT_ID, UPF_USER_ID
                        ";
            var p = new DynamicParameters();
            p.Add("@DPT_ID", dptId + "%");

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}