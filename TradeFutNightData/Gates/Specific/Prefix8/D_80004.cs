using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix8
{
    public class D_80004<T> : ParentGate
    {
        public D_80004(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByUserId(string userId)
        {
            var sql = @"
                        SELECT  TXN.TXN_NAME,
                                UTP.UTP_USER_ID,
                                UTP.UTP_YN_CODE,
                                UTP.UTP_W_DATE,
                                UTP.UTP_W_USER_ID,
                                UTP.UTP_TXN_ID,
                                ' ' AS CHOOSE,
                                ISNULL(TXN.TXN_DEFAULT, '') AS TXN_DEFAULT
                        FROM TXN, UTP
                        WHERE(TXN.TXN_ID = UTP.UTP_TXN_ID)
                        AND ((UTP.UTP_USER_ID = @USER_ID) )
                        AND ISNULL(TXN.TXN_DEFAULT, '') <> 'Y'
                        ";
            var p = new DynamicParameters();
            p.Add("@USER_ID", userId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}