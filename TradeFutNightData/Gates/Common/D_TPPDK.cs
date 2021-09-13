using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_TPPDK : D_TPPDK<TPPDK>
    {
        public D_TPPDK(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_TPPDK<T> : ParentGate
    {
        public IList<TPPDK> ListAll()
        {
            var query = _das.DataConn.GetTable<TPPDK>();

            return query.ToList();
        }

        public void Insert(IEnumerable<T> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.Insert(item);
            }
        }

        public void Delete(IEnumerable<T> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.Delete(item);
            }
        }

        public void Update(IEnumerable<TPPDK> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<TPPDK>()
                    .Where(c => c.TPPDK_KIND_ID == item.OriginalData.TPPDK_KIND_ID)
                    .Set(c => c.TPPDK_KIND_ID, item.TPPDK_KIND_ID)
                    .Set(c => c.TPPDK_INDEX_GRP, item.TPPDK_INDEX_GRP)
                    .Set(c => c.TPPDK_MULTI, item.TPPDK_MULTI)
                    .Set(c => c.TPPDK_USER_ID, item.TPPDK_USER_ID)
                    .Set(c => c.TPPDK_W_TIME, item.TPPDK_W_TIME)
                    .Set(c => c.TPPDK_THERICAL_P_INTERVAL, item.TPPDK_THERICAL_P_INTERVAL)
                    .Update();
            }
        }
    }
}