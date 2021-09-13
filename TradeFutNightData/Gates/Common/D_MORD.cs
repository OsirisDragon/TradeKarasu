using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_MORD : D_MORD<MORD>
    {
        public D_MORD(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_MORD<T> : ParentGate
    {
        public IList<MORD> ListAll()
        {
            var query = _das.DataConn.GetTable<MORD>();

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

        public void Update(IEnumerable<MORD> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<MORD>()
                    .Where(c => c.MORD_KIND_ID == item.OriginalData.MORD_KIND_ID &&
                                              c.MORD_SPREAD_CODE == item.OriginalData.MORD_SPREAD_CODE)
                    .Set(c => c.MORD_KIND_ID, item.MORD_KIND_ID)
                    .Set(c => c.MORD_KIND_ID_TYPE, item.MORD_KIND_ID_TYPE)
                    .Set(c => c.MORD_SPREAD_CODE, item.MORD_SPREAD_CODE)
                    .Set(c => c.MORD_MAX_QNTY, item.MORD_MAX_QNTY)
                    .Set(c => c.MORD_USER_ID, item.MORD_USER_ID)
                    .Set(c => c.MORD_W_TIME, item.MORD_W_TIME)
                    .Update();
            }
        }
    }
}