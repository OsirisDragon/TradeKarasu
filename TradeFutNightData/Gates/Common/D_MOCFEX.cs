using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_MOCFEX : D_MOCFEX<MOCFEX>
    {
        public D_MOCFEX(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_MOCFEX<T> : ParentGate
    {
        public List<T> EmptyData()
        {
            var items = new List<T>();
            return items;
        }

        public IEnumerable<MOCFEX> ListByDate(DateTime startDate, DateTime endDate)
        {
            var query = _das.DataConn.GetTable<MOCFEX>().Where(c => c.MOCFEX_DATE >= startDate && c.MOCFEX_DATE <= endDate);
            return query.ToList();
        }

        public bool HasYearData(int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year + 1, 1, 1).AddDays(-1);

            var items = ListByDate(startDate, endDate);

            return (items.Count() != 0) ? true : false;
        }

        public void Update(IEnumerable<MOCFEX> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<MOCFEX>()
                .Where(c => c.MOCFEX_DATE == item.OriginalData.MOCFEX_DATE)
                .Set(c => c.MOCFEX_CBOE_OPEN_CODE, item.MOCFEX_CBOE_OPEN_CODE)
                .Set(c => c.MOCFEX_USER_ID, item.MOCFEX_USER_ID)
                .Set(c => c.MOCFEX_W_TIME, item.MOCFEX_W_TIME)
                .Update();
            }
        }
    }
}