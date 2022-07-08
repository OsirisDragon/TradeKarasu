using DataEngine;
using LinqToDB;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_UPFCRD : D_UPFCRD<UPFCRD>
    {
        public D_UPFCRD(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_UPFCRD<T> : ParentGate
    {
        public void DeleteWithNormalType(string USER_ID)
        {
            _das.DataConn.GetTable<UPFCRD>()
                .Where(c => c.UPFCRD_USER_ID == USER_ID && c.UPFCRD_CARD_TYPE == 'N')
            .Delete();
        }
    }
}