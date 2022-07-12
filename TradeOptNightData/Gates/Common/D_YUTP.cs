using Dapper;
using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_YUTP : D_YUTP<YUTP>
    {
        public D_YUTP(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_YUTP<T> : ParentGate
    {
        public void Delete(string TXN_ID)
        {
            _das.DataConn.GetTable<YUTP>()
                .Where(c => c.YUTP_YTXN_ID == TXN_ID)
            .Delete();
        }

        public void DeleteUser(string USER_ID)
        {
            _das.DataConn.GetTable<YUTP>()
                .Where(c => c.YUTP_USER_ID == USER_ID)
            .Delete();
        }

        public void Update(IEnumerable<YUTP> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<YUTP>()
                    .Where(c => c.YUTP_USER_ID == item.OriginalData.YUTP_USER_ID &&
                                c.YUTP_YTXN_ID == item.OriginalData.YUTP_YTXN_ID)
                    .Set(c => c.YUTP_USER_ID, item.YUTP_USER_ID)
                    .Set(c => c.YUTP_YTXN_ID, item.YUTP_YTXN_ID)
                    .Set(c => c.YUTP_YN_CODE, item.YUTP_YN_CODE)
                    .Set(c => c.YUTP_W_DATE, item.YUTP_W_DATE)
                    .Set(c => c.YUTP_W_USER_ID, item.YUTP_W_USER_ID)
                    .Update();
            }
        }

        public int InsertBySelectYtxnUpf(string W_USER_ID)
        {
            var sql = @"
                        INSERT YUTP
                        SELECT UPF_USER_ID, YTXN_ID, '', GETDATE(), @W_USER_ID
                        FROM YTXN, UPF
                        WHERE YTXN_ID NOT IN
                        (SELECT YUTP_YTXN_ID FROM YUTP WHERE YUTP_USER_ID = UPF.UPF_USER_ID)
                        ";

            var p = new DynamicParameters();
            p.Add("@W_USER_ID", W_USER_ID);

            var result = _das.Conn.Execute(BuildCommand<T>(sql, p));

            return result;
        }
    }
}