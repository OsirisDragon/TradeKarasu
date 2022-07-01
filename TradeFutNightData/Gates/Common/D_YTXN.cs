using Dapper;
using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_YTXN : D_YTXN<YTXN>
    {
        public D_YTXN(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_YTXN<T> : ParentGate
    {
        public IEnumerable<YTXN> ListAll()
        {
            var query = _das.DataConn.GetTable<YTXN>().OrderBy(c => c.YTXN_ID);
            return query.ToList();
        }

        public IEnumerable<T> ListByUser(string userID)
        {
            var sql = @"
                        SELECT  YTXN_ID,
                                YTXN_NAME,
                                UTP_FILLER
                        FROM YTXN, UTP
                        WHERE YTXN_ID = UTP_YTXN_ID
                        AND UTP_USER_ID = @USER_ID
                        AND ISNULL(UTP_YN_CODE, '') = 'Y'

                        UNION

                        SELECT  YTXN_ID,
                                YTXN_NAME,
                                UTP_FILLER
                        FROM YTXN, UTP
                        WHERE YTXN_ID = UTP_YTXN_ID
                        AND UTP_USER_ID = @USER_ID
                        AND ISNULL(UTP_YN_CODE, '') <> 'Y'
                        AND ISNULL(YTXN.YTXN_DEFAULT, '') = 'Y'
                        ";

            var p = new DynamicParameters();
            p.Add("@USER_ID", userID);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public YTXN Get(string txnID)
        {
            var query = _das.DataConn.GetTable<YTXN>().Where(c => c.YTXN_ID == txnID);
            return query.SingleOrDefault();
        }
    }
}