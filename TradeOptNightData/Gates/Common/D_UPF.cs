using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
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

        public IEnumerable<UPF> ListByDpt(char dptId)
        {
            var query = _das.DataConn.GetTable<UPF>().Where(c => c.UPF_DEPT_ID == dptId);
            return query.ToList();
        }

        public UPF GetByUserAdAccount(string adAccount)
        {
            var query = _das.DataConn.GetTable<UPF>().Where(c => c.UPF_USER_AD == adAccount);
            return query.SingleOrDefault();
        }

        public void Update(UPF upf)
        {
            _das.DataConn.InlineParameters = true;
            _das.DataConn.GetTable<UPF>()
                   .Where(c => c.UPF_USER_ID == upf.UPF_USER_ID)
                   .Set(c => c.UPF_USER_NAME, upf.UPF_USER_NAME)
                   .Set(c => c.UPF_USER_AD, upf.UPF_USER_AD)
                   .Set(c => c.UPF_DEPT_ID, upf.UPF_DEPT_ID)
                   .Set(c => c.UPF_EMPLOYEE_ID, upf.UPF_EMPLOYEE_ID)
                   .Set(c => c.UPF_W_DATE, upf.UPF_W_DATE)
                   .Set(c => c.UPF_W_USER_ID, upf.UPF_W_USER_ID)
                   .Update();
        }
    }
}