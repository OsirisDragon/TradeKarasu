using CrossModel;
using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_EXRT : D_EXRT<EXRT>
    {
        public D_EXRT(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_EXRT<T> : ParentGate
    {
        public IEnumerable<EXRT> ListAll()
        {
            var query = _das.DataConn.GetTable<EXRT>().OrderBy(c => c.EXRT_CURRENCY_TYPE);
            return query.ToList();
        }

        public void Save(Operate<EXRT> items)
        {
            if (items.AddedItems != null && items.AddedItems.Count() != 0)
                Insert(items.AddedItems);

            if (items.DeletedItems != null && items.DeletedItems.Count() != 0)
                Delete(items.DeletedItems);

            if (items.ChangedItems != null && items.ChangedItems.Count() != 0)
            {
                foreach (var item in items.ChangedItems)
                {
                    char originEXRT_CURRENCY_TYPE = (item.OriginalData == null) ? item.EXRT_CURRENCY_TYPE : item.OriginalData.EXRT_CURRENCY_TYPE;
                    char originEXRT_COUNT_CURRENCY = (item.OriginalData == null) ? item.EXRT_COUNT_CURRENCY : item.OriginalData.EXRT_COUNT_CURRENCY;

                    _das.DataConn.GetTable<EXRT>()
                        .Where(c => c.EXRT_CURRENCY_TYPE == originEXRT_CURRENCY_TYPE && c.EXRT_COUNT_CURRENCY == originEXRT_COUNT_CURRENCY)
                        .Set(c => c.EXRT_CURRENCY_TYPE, item.EXRT_CURRENCY_TYPE)
                        .Set(c => c.EXRT_COUNT_CURRENCY, item.EXRT_COUNT_CURRENCY)
                        .Set(c => c.EXRT_EXCHANGE_RATE, item.EXRT_EXCHANGE_RATE)
                        .Set(c => c.EXRT_MARKET_EXCHANGE_RATE, item.EXRT_MARKET_EXCHANGE_RATE)
                        .Set(c => c.EXRT_USER_ID, item.EXRT_USER_ID)
                        .Set(c => c.EXRT_W_TIME, item.EXRT_W_TIME)
                        .Update();
                }
            }
        }
    }
}