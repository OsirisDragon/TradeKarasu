using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix8
{
    public class D_80014<T> : ParentGate
    {
        public D_80014(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByUserId(string userId)
        {
            var sql = @"
                        SELECT  UTP_USER_ID,
                                UTP_TXN_ID,
                                TXN_NAME,
                                UTP_YN_CODE
                        FROM TXN, UTP
                        WHERE  TXN_ID = UTP_TXN_ID
                        AND UTP_USER_ID = @USER_ID
                        AND UTP_YN_CODE='Y'
                        AND ISNULL(TXN_DEFAULT,'') <> 'Y'
                        ";
            var p = new DynamicParameters();
            p.Add("@USER_ID", userId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}