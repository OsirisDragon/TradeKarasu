using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_PCM : D_PCM<PCM>
    {
        public D_PCM(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_PCM<T> : ParentGate
    {
        public void Delete(string PCM_FCM_NO, string PCM_PROD_ID)
        {
            _das.DataConn.GetTable<PCM>()
                .Where(c => c.PCM_FCM_NO == PCM_FCM_NO &&
                                          c.PCM_PROD_ID == PCM_PROD_ID)
            .Delete();
        }

        public IList<PCM> ListByCmNo(string PCM_CM_NO)
        {
            IQueryable<PCM> query = _das.DataConn.GetTable<PCM>()
                .OrderBy(c => c.PCM_FCM_NO).ThenBy(c => c.PCM_PROD_ID);
            if (!string.IsNullOrEmpty(PCM_CM_NO))
            {
                query = query.Where(c => c.PCM_CM_NO == PCM_CM_NO);
            }

            return query.ToList();
        }
    }
}