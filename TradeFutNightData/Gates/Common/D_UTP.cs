using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
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
    }
}