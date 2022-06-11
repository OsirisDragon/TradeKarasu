using DataEngine;
using LinqToDB;
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
            var query = _das.DataConn.GetTable<TPPST>()
                .Where(c => c.TPPST_KIND_ID == kindId);

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
    }
}