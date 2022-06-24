using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_CADJ : D_CADJ<CADJ>
    {
        public D_CADJ(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_CADJ<T> : ParentGate
    {
        public IList<CADJ> ListByDate(DateTime CADJ_BASE_DATE)
        {
            var query = _das.DataConn.GetTable<CADJ>()
                .Where(c => c.CADJ_BASE_DATE == CADJ_BASE_DATE)
                .OrderBy(c => c.CADJ_BF_STOCK_ID)
                .ThenByDescending(c => c.CADJ_BF_KIND_ID);

            return query.ToList();
        }

        public void Update(IEnumerable<CADJ> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<CADJ>()
                    .Where(c => c.CADJ_BASE_DATE == item.OriginalData.CADJ_BASE_DATE &&
                                c.CADJ_BF_KIND_ID == item.OriginalData.CADJ_BF_KIND_ID)
                    .Set(c => c.CADJ_BASE_DATE, item.CADJ_BASE_DATE)
                    .Set(c => c.CADJ_BF_KIND_ID, item.CADJ_BF_KIND_ID)
                    .Set(c => c.CADJ_BF_STOCK_ID, item.CADJ_BF_STOCK_ID)
                    .Set(c => c.CADJ_BF_STOCK_QNTY, item.CADJ_BF_STOCK_QNTY)
                    .Set(c => c.CADJ_BF_STOCK_CASH2, item.CADJ_BF_STOCK_CASH2)
                    .Set(c => c.CADJ_BF_STOCK_CASH3, item.CADJ_BF_STOCK_CASH3)
                    .Set(c => c.CADJ_BF_STOCK_ID4, item.CADJ_BF_STOCK_ID4)
                    .Set(c => c.CADJ_BF_STOCK_QNTY4, item.CADJ_BF_STOCK_QNTY4)
                    .Set(c => c.CADJ_AF_KIND_ID, item.CADJ_AF_KIND_ID)
                    .Set(c => c.CADJ_AF_STOCK_ID, item.CADJ_AF_STOCK_ID)
                    .Set(c => c.CADJ_AF_STOCK_QNTY, item.CADJ_AF_STOCK_QNTY)
                    .Set(c => c.CADJ_AF_STOCK_CASH2, item.CADJ_AF_STOCK_CASH2)
                    .Set(c => c.CADJ_AF_STOCK_PRICE3, item.CADJ_AF_STOCK_PRICE3)
                    .Set(c => c.CADJ_AF_STOCK_QNTY3, item.CADJ_AF_STOCK_QNTY3)
                    .Set(c => c.CADJ_AF_STOCK_DATE3, item.CADJ_AF_STOCK_DATE3)
                    .Set(c => c.CADJ_AF_STOCK_ID4, item.CADJ_AF_STOCK_ID4)
                    .Set(c => c.CADJ_AF_STOCK_QNTY4, item.CADJ_AF_STOCK_QNTY4)
                    .Set(c => c.CADJ_DIVIDEND_DATE, item.CADJ_DIVIDEND_DATE)
                    .Set(c => c.CADJ_USER_ID, item.CADJ_USER_ID)
                    .Set(c => c.CADJ_W_TIME, item.CADJ_W_TIME)
                    .Set(c => c.CADJ_BF_STOCK_PRICE3, item.CADJ_BF_STOCK_PRICE3)
                    .Set(c => c.CADJ_BF_STOCK_QNTY3, item.CADJ_BF_STOCK_QNTY3)
                    .Set(c => c.CADJ_BF_STOCK_DATE3, item.CADJ_BF_STOCK_DATE3)
                    .Set(c => c.CADJ_BF_KIND_ID_LONG, item.CADJ_BF_KIND_ID_LONG)
                    .Set(c => c.CADJ_AF_KIND_ID_LONG, item.CADJ_AF_KIND_ID_LONG)
                    .Update();
            }
        }
    }
}