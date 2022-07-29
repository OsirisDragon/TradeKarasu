using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_PHALT : D_PHALT<PHALT>
    {
        public D_PHALT(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_PHALT<T> : ParentGate
    {
        public void Update(IEnumerable<PHALT> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<PHALT>()
                    .Where(c => c.PHALT_TRADE_DATE == item.OriginalData.PHALT_TRADE_DATE &&
                                              c.PHALT_PROD_ID == item.OriginalData.PHALT_PROD_ID &&
                                              c.PHALT_MSG_TYPE == item.OriginalData.PHALT_MSG_TYPE)
                    .Set(c => c.PHALT_TRADE_DATE, item.PHALT_TRADE_DATE)
                    .Set(c => c.PHALT_PROD_ID, item.PHALT_PROD_ID)
                    .Set(c => c.PHALT_MSG_TYPE, item.PHALT_MSG_TYPE)
                    .Set(c => c.PHALT_TYPE, item.PHALT_TYPE)
                    .Set(c => c.PHALT_TRADE_PAUSE_DATE, item.PHALT_TRADE_PAUSE_DATE)
                    .Set(c => c.PHALT_TRADE_PAUSE_TIME, item.PHALT_TRADE_PAUSE_TIME)
                    .Set(c => c.PHALT_TRADE_RESUME_DATE, item.PHALT_TRADE_RESUME_DATE)
                    .Set(c => c.PHALT_TRADE_RESUME_TIME, item.PHALT_TRADE_RESUME_TIME)
                    .Set(c => c.PHALT_ORDER_RESUME_TIME, item.PHALT_ORDER_RESUME_TIME)
                    .Set(c => c.PHALT_MATCH_RESUME_TIME, item.PHALT_MATCH_RESUME_TIME)
                    .Set(c => c.PHALT_STOCK_ID, item.PHALT_STOCK_ID)
                    .Set(c => c.PHALT_USER_ID, item.PHALT_USER_ID)
                    .Set(c => c.PHALT_W_TIME, DateTime.Now)
                    .Update();
            }
        }

        public IList<PHALT> ListByDate(DateTime startDate, DateTime endDate)
        {
            var query = _das.DataConn.GetTable<PHALT>()
                .Where(c => c.PHALT_TRADE_DATE >= startDate &&
                          c.PHALT_TRADE_DATE <= endDate &&
                          c.PHALT_TYPE == 'T')
                .OrderByDescending(c => c.PHALT_TRADE_DATE).ThenByDescending(c => c.PHALT_W_TIME);

            return query.ToList();
        }

        public IList<PHALT> ListByDateAndProd(DateTime tradeDate, DateTime pauseDate, string prodId)
        {
            var query = _das.DataConn.GetTable<PHALT>()
                .Where(c => c.PHALT_TRADE_DATE == tradeDate &&
                          c.PHALT_TRADE_PAUSE_DATE == pauseDate &&
                          c.PHALT_PROD_ID == prodId);

            return query.ToList();
        }

        public IList<PHALT> ListNotResume()
        {
            var query = _das.DataConn.GetTable<PHALT>()
                .Where(c => c.PHALT_TRADE_RESUME_DATE == null &&
                          c.PHALT_TYPE == 'T')
                .OrderBy(c => c.PHALT_TRADE_PAUSE_DATE).ThenBy(c => c.PHALT_TRADE_PAUSE_TIME);

            return query.ToList();
        }
    }
}