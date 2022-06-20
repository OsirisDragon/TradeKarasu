using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_TPPST : D_TPPST<TPPST>
    {
        public D_TPPST(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_TPPST<T> : ParentGate
    {
        public IList<TPPST> ListAll()
        {
            var query = _das.DataConn.GetTable<TPPST>();

            return query.ToList();
        }

        public IList<TPPST> ListByKindId(string kindId)
        {
            IQueryable<TPPST> query = _das.DataConn.GetTable<TPPST>()
                                    .OrderBy(c => c.TPPST_KIND_ID)
                                    .ThenBy(c => c.TPPST_MONTH);
            if (kindId != "%")
            {
                query = query.Where(c => c.TPPST_KIND_ID == kindId);
            }

            return query.ToList();
        }

        /// <summary>
        /// 抓取單式月份的資料，排除掉TPPST_MONTH為0的，因為0的代表是複式商品的設定
        /// </summary>
        public IList<TPPST> ListSingleMonth()
        {
            var query = _das.DataConn.GetTable<TPPST>().Where(c => c.TPPST_MONTH != 0)
                .OrderBy(c => c.TPPST_KIND_ID).ThenBy(c => c.TPPST_MONTH);

            return query.ToList();
        }

        public IEnumerable<TPPST> ListKindID()
        {
            var query = _das.DataConn.GetTable<TPPST>()
                .OrderBy(c => c.TPPST_KIND_ID)
                .Select(c => new TPPST() { TPPST_KIND_ID = c.TPPST_KIND_ID }).Distinct();

            return query.ToList();
        }

        public void Update(IEnumerable<TPPST> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<TPPST>()
                    .Where(c => c.TPPST_KIND_ID == item.OriginalData.TPPST_KIND_ID &&
                                c.TPPST_MONTH == item.OriginalData.TPPST_MONTH)
                    .Set(c => c.TPPST_KIND_ID, item.TPPST_KIND_ID)
                    .Set(c => c.TPPST_MONTH, item.TPPST_MONTH)
                    .Set(c => c.TPPST_M_PRICE_LIMIT, item.TPPST_M_PRICE_LIMIT)
                    .Set(c => c.TPPST_M_PRICE_LIMIT_F, item.TPPST_M_PRICE_LIMIT_F)
                    .Set(c => c.TPPST_M_INTERVAL, item.TPPST_M_INTERVAL)
                    .Set(c => c.TPPST_ACCU_QNTY, item.TPPST_ACCU_QNTY)
                    .Set(c => c.TPPST_M_PRICE_FILTER, item.TPPST_M_PRICE_FILTER)
                    .Set(c => c.TPPST_BS_PRICE_FILTER, item.TPPST_BS_PRICE_FILTER)
                    .Set(c => c.TPPST_INDEX_ID, item.TPPST_INDEX_ID)
                    .Set(c => c.TPPST_UNIT, item.TPPST_UNIT)
                    .Set(c => c.TPPST_UNDERLYING_MARKET, item.TPPST_UNDERLYING_MARKET)
                    .Set(c => c.TPPST_USER_ID, item.TPPST_USER_ID)
                    .Set(c => c.TPPST_W_TIME, DateTime.Now)
                    .Update();
            }
        }
    }
}