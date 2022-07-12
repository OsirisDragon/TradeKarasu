using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_UTP : D_UTP<UTP>
    {
        public D_UTP(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_UTP<T> : ParentGate
    {
        public void Delete(string TXN_ID)
        {
            _das.DataConn.GetTable<UTP>()
                .Where(c => c.UTP_TXN_ID == TXN_ID)
            .Delete();
        }

        public void DeleteUser(string USER_ID)
        {
            _das.DataConn.GetTable<UTP>()
                .Where(c => c.UTP_USER_ID == USER_ID)
            .Delete();
        }

        public int InsertBySelectTxnUpf(string W_USER_ID)
        {
            var sql = @"
                        INSERT UTP
                        SELECT UPF_USER_ID,TXN_ID,'','',@DATE_TIME,@W_USER_ID
                        FROM TXN,UPF
                        WHERE TXN_ID NOT IN
                        (SELECT UTP_TXN_ID FROM UTP WHERE UTP_USER_ID = UPF.UPF_USER_ID)
                        ";

            var p = new DynamicParameters();
            p.Add("@DATE_TIME", DateTime.Now);
            p.Add("@W_USER_ID", W_USER_ID);

            var result = _das.Conn.Execute(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<UTP> ListByTxnAndUser(string TXN_ID, string USER_ID)
        {
            var query = _das.DataConn.GetTable<UTP>()
                .Where(c => c.UTP_TXN_ID == TXN_ID && c.UTP_USER_ID == USER_ID);
            return query.ToList();
        }

        public void InsertItem(UTP item)
        {
            _das.DataConn.Insert(item);
        }

        public void UpdateItem(UTP item)
        {
            _das.DataConn.GetTable<UTP>()
                .Where(c => c.UTP_USER_ID == item.UTP_USER_ID && c.UTP_TXN_ID == item.UTP_TXN_ID)
                .Set(c => c.UTP_YN_CODE, item.UTP_YN_CODE)
                .Set(c => c.UTP_W_DATE, item.UTP_W_DATE)
                .Set(c => c.UTP_W_USER_ID, item.UTP_W_USER_ID)
                .Update();
        }
    }
}