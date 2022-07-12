using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_CCM : D_CCM<CCM>
    {
        public D_CCM(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_CCM<T> : ParentGate
    {
        public IList<CCM> ListAll()
        {
            var query = _das.DataConn.GetTable<CCM>()
                .OrderBy(c => c.CCM_FCM_NO).ThenBy(c => c.CCM_SUBTYPE);

            return query.ToList();
        }

        public IList<CCM> ListByCmNo(string CCM_CM_NO)
        {
            IQueryable<CCM> query = _das.DataConn.GetTable<CCM>()
                .OrderBy(c => c.CCM_FCM_NO).ThenBy(c => c.CCM_SUBTYPE);
            if (!string.IsNullOrEmpty(CCM_CM_NO))
            {
                query = query.Where(c => c.CCM_CM_NO == CCM_CM_NO);
            }

            return query.ToList();
        }

        public void Update(IEnumerable<CCM> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<CCM>()
                    .Where(c => c.CCM_FCM_NO == item.OriginalData.CCM_FCM_NO &&
                                              c.CCM_SUBTYPE == item.OriginalData.CCM_SUBTYPE)
                    .Set(c => c.CCM_FCM_NO, item.CCM_FCM_NO)
                    .Set(c => c.CCM_CM_NO, item.CCM_CM_NO)
                    .Set(c => c.CCM_SUBTYPE, item.CCM_SUBTYPE)
                    .Set(c => c.CCM_USER_ID, item.CCM_USER_ID)
                    .Set(c => c.CCM_W_TIME, DateTime.Now)
                    .Update();
            }
        }
    }
}