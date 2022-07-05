using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
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
                    .Where(c => c.YUTP_USER_ID == item.YUTP_USER_ID && c.YUTP_YTXN_ID == item.YUTP_YTXN_ID)
                    .Set(c => c.YUTP_YN_CODE, item.YUTP_YN_CODE)
                    .Set(c => c.YUTP_W_DATE, item.YUTP_W_DATE)
                    .Set(c => c.YUTP_W_USER_ID, item.YUTP_W_USER_ID)
                    .Update();
            }
        }
    }
}