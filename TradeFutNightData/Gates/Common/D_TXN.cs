using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_TXN : D_TXN<TXN>
    {
        public D_TXN(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_TXN<T> : ParentGate
    {
        public IEnumerable<TXN> ListAll()
        {
            var query = _das.DataConn.GetTable<TXN>().OrderBy(c => c.TXN_ID);
            return query.ToList();
        }

        public IEnumerable<T> ListByUser(string userID)
        {
            var sql = @"
                        SELECT  TXN_ID,
                                TXN_NAME,
                                UTP_FILLER
                        FROM TXN, UTP
                        WHERE TXN_ID = UTP_TXN_ID
                        AND UTP_USER_ID = @USER_ID
                        AND ISNULL(UTP_YN_CODE, '') = 'Y'

                        UNION

                        SELECT  TXN_ID,
                                TXN_NAME,
                                UTP_FILLER
                        FROM TXN, UTP
                        WHERE TXN_ID = UTP_TXN_ID
                        AND UTP_USER_ID = @USER_ID
                        AND ISNULL(UTP_YN_CODE, '') <> 'Y'
                        AND ISNULL(TXN.TXN_DEFAULT, '') = 'Y'
                        ";

            var p = new DynamicParameters();
            p.Add("@USER_ID", userID);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public TXN Get(string txnID)
        {
            var query = _das.DataConn.GetTable<TXN>().Where(c => c.TXN_ID == txnID);
            return query.SingleOrDefault();
        }

        public void Update(IEnumerable<TXN> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<TXN>()
                    .Where(c => c.TXN_ID == item.OriginalData.TXN_ID)
                    .Set(c => c.TXN_ID, item.TXN_ID)
                    .Set(c => c.TXN_NAME, item.TXN_NAME)
                    .Set(c => c.TXN_REMARK, item.TXN_REMARK)
                    .Set(c => c.TXN_TYPE, item.TXN_TYPE)
                    .Set(c => c.TXN_DEFAULT, item.TXN_DEFAULT)
                    .Set(c => c.TXN_OSW_GRP, item.TXN_OSW_GRP)
                    .Set(c => c.TXN_W_USER_ID, item.TXN_W_USER_ID)
                    .Set(c => c.TXN_W_TIME, DateTime.Now)
                    .Update();
            }
        }
    }
}