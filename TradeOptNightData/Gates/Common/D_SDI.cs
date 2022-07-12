using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_SDI : D_SDI<SDI>
    {
        public D_SDI(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_SDI<T> : ParentGate
    {
        public IList<SDI> ListByDate(DateTime SDI_BASE_DATE)
        {
            //只要除息的就好，除權0、增資0、減資0、股份轉換0
            var query = _das.DataConn.GetTable<SDI>()
                .Where(c => c.SDI_BASE_DATE == SDI_BASE_DATE &&
                            c.SDI_DIVIDEND_CASH != 0 &&
                            c.SDI_DIVIDEND_SHARE == 0 &&
                            c.SDI_INCREASE_RATE == 0 &&
                            c.SDI_DISINVEST_RATE == 0 &&
                            c.SDI_COMB_RATE == 0)
                .OrderBy(c => c.SDI_STOCK_ID);

            return query.ToList();
        }

        public void Update(IEnumerable<SDI> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<SDI>()
                    .Where(c => c.SDI_BASE_DATE == item.OriginalData.SDI_BASE_DATE &&
                                c.SDI_STOCK_ID == item.OriginalData.SDI_STOCK_ID)
                    .Set(c => c.SDI_BASE_DATE, item.SDI_BASE_DATE)
                    .Set(c => c.SDI_STOCK_ID, item.SDI_STOCK_ID)
                    .Set(c => c.SDI_DIVIDEND_SHARE, item.SDI_DIVIDEND_SHARE)
                    .Set(c => c.SDI_DIVIDEND_CASH, item.SDI_DIVIDEND_CASH)
                    .Set(c => c.SDI_INCREASE_PRICE, item.SDI_INCREASE_PRICE)
                    .Set(c => c.SDI_INCREASE_RATE, item.SDI_INCREASE_RATE)
                    .Set(c => c.SDI_INCREASE_DATE, item.SDI_INCREASE_DATE)
                    .Set(c => c.SDI_DISINVEST_RATE, item.SDI_DISINVEST_RATE)
                    .Set(c => c.SDI_COMB_STOCK_ID, item.SDI_COMB_STOCK_ID)
                    .Set(c => c.SDI_COMB_RATE, item.SDI_COMB_RATE)
                    .Set(c => c.SDI_OTH_STOCK_ID, item.SDI_OTH_STOCK_ID)
                    .Set(c => c.SDI_OTH_RATE, item.SDI_OTH_RATE)
                    .Set(c => c.SDI_DIVIDEND_DATE, item.SDI_DIVIDEND_DATE)
                    .Set(c => c.SDI_U, item.SDI_U)
                    .Set(c => c.SDI_N1, item.SDI_N1)
                    .Set(c => c.SDI_ADJUST_TYPE_CH, item.SDI_ADJUST_TYPE_CH)
                    .Set(c => c.SDI_ADJUST_CONTENT_CH, item.SDI_ADJUST_CONTENT_CH)
                    .Set(c => c.SDI_WEBSITE_CH, item.SDI_WEBSITE_CH)
                    .Set(c => c.SDI_ADJUST_TYPE_EN, item.SDI_ADJUST_TYPE_EN)
                    .Set(c => c.SDI_ADJUST_CONTENT_EN, item.SDI_ADJUST_CONTENT_EN)
                    .Set(c => c.SDI_WEBSITE_EN, item.SDI_WEBSITE_EN)
                    .Set(c => c.SDI_POSITION_DEADLINE, item.SDI_POSITION_DEADLINE)
                    .Set(c => c.SDI_POSITION_STOCK_QNTY, item.SDI_POSITION_STOCK_QNTY)
                    .Update();
            }
        }
    }
}