using DataEngine;
using LinqToDB;
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
    }
}