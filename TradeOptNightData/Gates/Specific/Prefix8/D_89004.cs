using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.Prefix8
{
    public class D_89004<T> : ParentGate
    {
        public D_89004(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListByUserId(string userId)
        {
            var sql = @"
                        SELECT  YTXN.YTXN_NAME,
                                YUTP.YUTP_USER_ID,
                                YUTP.YUTP_YN_CODE,
                                YUTP.YUTP_W_DATE,
                                YUTP.YUTP_W_USER_ID,
                                YUTP.YUTP_YTXN_ID,
                                ' ' AS CHOOSE,
                                ISNULL(YTXN.YTXN_DEFAULT, '') AS YTXN_DEFAULT
                        FROM YTXN, YUTP
                        WHERE(YTXN.YTXN_ID = YUTP.YUTP_YTXN_ID)
                        AND ((YUTP.YUTP_USER_ID = @USER_ID) )
                        AND ISNULL(YTXN.YTXN_DEFAULT, '') <> 'Y'
                        ";
            var p = new DynamicParameters();
            p.Add("@USER_ID", userId);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}