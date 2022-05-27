using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_UPF : D_UPF<UPF>
    {
        public D_UPF(DalSession das)
        {
            _das = das;
        }
    }

    public class D_UPF<T> : ParentGate
    {
        public IEnumerable<UPF> ListAll()
        {
            var query = _das.DataConn.GetTable<UPF>().OrderBy(c => c.UPF_USER_ID);
            return query.ToList();
        }

        public UPF Get(string userID)
        {
            var query = _das.DataConn.GetTable<UPF>().Where(c => c.UPF_USER_ID == userID);
            return query.SingleOrDefault();
        }

        public UPF GetByUserAdAccount(string adAccount)
        {
            var query = _das.DataConn.GetTable<UPF>().Where(c => c.UPF_USER_AD == adAccount);
            return query.SingleOrDefault();
        }
    }
}