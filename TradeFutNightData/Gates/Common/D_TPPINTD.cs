using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_TPPINTD : D_TPPINTD<TPPINTD>
    {
        public D_TPPINTD(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_TPPINTD<T> : ParentGate
    {
        public IList<TPPINTD> ListAll()
        {
            var query = _das.DataConn.GetTable<TPPINTD>()
                .OrderBy(c => c.TPPINTD_FIRST_KIND_ID).ThenBy(c => c.TPPINTD_FIRST_KIND_ID).ThenBy(c => c.TPPINTD_SECOND_KIND_ID);

            return query.ToList();
        }

        /// <summary>
        /// 抓取單式資料
        /// </summary>
        public IList<TPPINTD> ListSingleKind()
        {
            var query = _das.DataConn.GetTable<TPPINTD>().Where(c => c.TPPINTD_SECOND_KIND_ID == "" && c.TPPINTD_SECOND_MONTH == 0)
                .OrderBy(c => c.TPPINTD_FIRST_KIND_ID).ThenBy(c => c.TPPINTD_FIRST_MONTH);

            return query.ToList();
        }

        public IEnumerable<TPPINTD> ListByKindID(string TPPINTD_FIRST_KIND_ID, string TPPINTD_SECOND_KIND_ID)
        {
            IQueryable<TPPINTD> query = _das.DataConn.GetTable<TPPINTD>()
                .OrderByDescending(c => c.TPPINTD_FIRST_KIND_ID).ThenBy(c => c.TPPINTD_FIRST_MONTH).ThenByDescending(c => c.TPPINTD_SECOND_KIND_ID).ThenBy(c => c.TPPINTD_SECOND_MONTH);
            if (TPPINTD_FIRST_KIND_ID != "%")
            {
                query = query.Where(c => c.TPPINTD_FIRST_KIND_ID == TPPINTD_FIRST_KIND_ID);
            }
            if (TPPINTD_SECOND_KIND_ID != "%")
            {
                query = query.Where(c => c.TPPINTD_SECOND_KIND_ID == TPPINTD_SECOND_KIND_ID);
            }
            return query.ToList();
        }

        public void Update(IEnumerable<TPPINTD> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<TPPINTD>()
                    .Where(c => c.TPPINTD_FIRST_KIND_ID == item.OriginalData.TPPINTD_FIRST_KIND_ID &&
                                              c.TPPINTD_FIRST_MONTH == item.OriginalData.TPPINTD_FIRST_MONTH &&
                                              c.TPPINTD_SECOND_KIND_ID == item.OriginalData.TPPINTD_SECOND_KIND_ID &&
                                              c.TPPINTD_SECOND_MONTH == item.OriginalData.TPPINTD_SECOND_MONTH)
                    .Set(c => c.TPPINTD_FIRST_KIND_ID, item.TPPINTD_FIRST_KIND_ID)
                    .Set(c => c.TPPINTD_FIRST_MONTH, item.TPPINTD_FIRST_MONTH)
                    .Set(c => c.TPPINTD_SECOND_KIND_ID, item.TPPINTD_SECOND_KIND_ID)
                    .Set(c => c.TPPINTD_SECOND_MONTH, item.TPPINTD_SECOND_MONTH)
                    .Set(c => c.TPPINTD_M_PRICE_LIMIT, item.TPPINTD_M_PRICE_LIMIT)
                    .Set(c => c.TPPINTD_M_PRICE_LIMIT_F, item.TPPINTD_M_PRICE_LIMIT_F)
                    .Set(c => c.TPPINTD_M_INTERVAL, item.TPPINTD_M_INTERVAL)
                    .Set(c => c.TPPINTD_ACCU_QNTY, item.TPPINTD_ACCU_QNTY)
                    .Set(c => c.TPPINTD_M_PRICE_FILTER, item.TPPINTD_M_PRICE_FILTER)
                    .Set(c => c.TPPINTD_BS_PRICE_FILTER, item.TPPINTD_BS_PRICE_FILTER)
                    .Set(c => c.TPPINTD_UNIT, item.TPPINTD_UNIT)
                    .Set(c => c.TPPINTD_FOREIGN_INTERVAL, item.TPPINTD_FOREIGN_INTERVAL)
                    .Set(c => c.TPPINTD_USER_ID, item.TPPINTD_USER_ID)
                    .Set(c => c.TPPINTD_W_TIME, DateTime.Now)
                    .Update();
            }
        }

        public IEnumerable<TPPINTD> ListFirstKindID()
        {
            var query = _das.DataConn.GetTable<TPPINTD>()
                .OrderBy(c => c.TPPINTD_FIRST_KIND_ID)
                .Select(c => new TPPINTD() { TPPINTD_FIRST_KIND_ID = c.TPPINTD_FIRST_KIND_ID }).Distinct();

            return query.ToList();
        }

        public IEnumerable<TPPINTD> ListSecondKindID()
        {
            var query = _das.DataConn.GetTable<TPPINTD>()
                .OrderBy(c => c.TPPINTD_SECOND_KIND_ID)
                .Where(c => c.TPPINTD_SECOND_KIND_ID != "")
                .Select(c => new TPPINTD() { TPPINTD_SECOND_KIND_ID = c.TPPINTD_SECOND_KIND_ID }).Distinct();

            return query.ToList();
        }
    }
}