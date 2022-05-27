using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_LOGF : D_LOGF<LOGF>
    {
        public D_LOGF(DalSession das)
        {
            this._das = das;
            _das.DataConn.InlineParameters = true;
        }
    }

    public class D_LOGF<T> : ParentGate
    {
        public int Insert(string LOGF_USER_ID, string LOGF_ITEM, string LOGF_KEY_DATA, DateTime LOGF_TIME = default(DateTime), char? LOGF_FILLER = null)
        {
            int affectedRows = -1;

            if (LOGF_TIME == default(DateTime))
                LOGF_TIME = DateTime.Now;

            var logf = new LOGF()
            {
                LOGF_TIME = LOGF_TIME,
                LOGF_USER_ID = LOGF_USER_ID,
                LOGF_ITEM = LOGF_ITEM,
                LOGF_KEY_DATA = LOGF_KEY_DATA,
                LOGF_FILLER = LOGF_FILLER
            };

            affectedRows = _das.DataConn.Insert(logf);

            return affectedRows;
        }

        public bool IsCompleted(string logfItem, string likeCondition)
        {
            DateTime dateToday = DateTime.Now.Date;
            int count = _das.DataConn.GetTable<LOGF>().Where(c => c.LOGF_TIME >= dateToday && c.LOGF_TIME < dateToday.AddDays(1) && c.LOGF_ITEM == logfItem && Sql.Like(c.LOGF_KEY_DATA, likeCondition)).Count();
            return (count > 0) ? true : false;
        }

        public string GetKeyData(string logfItem, string likeCondition)
        {
            string keyData = _das.DataConn.GetTable<LOGF>()
                .Where(c => c.LOGF_ITEM == logfItem && Sql.Like(c.LOGF_KEY_DATA, likeCondition))
                .OrderByDescending(c => c.LOGF_TIME)
                .Select(c => c.LOGF_KEY_DATA).FirstOrDefault();
            return keyData;
        }

        public string GetKeyData(string logfItem, string likeCondition1, string likeCondition2)
        {
            string keyData = _das.DataConn.GetTable<LOGF>()
                .Where(c => c.LOGF_ITEM == logfItem &&
                (Sql.Like(c.LOGF_KEY_DATA, likeCondition1) || Sql.Like(c.LOGF_KEY_DATA, likeCondition2)))
                .OrderByDescending(c => c.LOGF_TIME)
                .Select(c => c.LOGF_KEY_DATA).FirstOrDefault();
            return keyData;
        }

        public IEnumerable<LOGF> ListByDateAndKey(string logfItem1, string logfItem2, DateTime sdate, DateTime edate, string likeCondition)
        {
            var query = _das.DataConn.GetTable<LOGF>()
                .Where(c => (c.LOGF_ITEM == logfItem1 || c.LOGF_ITEM == logfItem2) &&
                c.LOGF_TIME >= sdate &&
                c.LOGF_TIME < edate.AddDays(1) &&
                !string.IsNullOrEmpty(c.LOGF_KEY_DATA) &&
                Sql.Like(c.LOGF_KEY_DATA, likeCondition))
                .OrderByDescending(c => c.LOGF_TIME);
            return query;
        }
    }
}