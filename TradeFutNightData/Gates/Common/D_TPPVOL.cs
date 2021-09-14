using Dapper;
using DataEngine;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_TPPVOL : D_TPPVOL<TPPVOL>
    {
        public D_TPPVOL(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_TPPVOL<T> : ParentGate
    {
        public IList<TPPVOL> ListAll()
        {
            var query = _das.DataConn.GetTable<TPPVOL>();

            return query.ToList();
        }

        public void Update(IEnumerable<TPPVOL> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<TPPVOL>()
                    .Where(c => c.TPPVOL_TYPE == item.OriginalData.TPPVOL_TYPE &&
                                              c.TPPVOL_INDEX_GRP == item.OriginalData.TPPVOL_INDEX_GRP)
                    .Set(c => c.TPPVOL_TYPE, item.TPPVOL_TYPE)
                    .Set(c => c.TPPVOL_REMOVE_COUNT, item.TPPVOL_REMOVE_COUNT)
                    .Set(c => c.TPPVOL_GROWTH_RATE, item.TPPVOL_GROWTH_RATE)
                    .Set(c => c.TPPVOL_INCREASE_RATE, item.TPPVOL_INCREASE_RATE)
                    .Set(c => c.TPPVOL_SEND_COUNT, item.TPPVOL_SEND_COUNT)
                    .Set(c => c.TPPVOL_USER_ID, item.TPPVOL_USER_ID)
                    .Set(c => c.TPPVOL_W_TIME, item.TPPVOL_W_TIME)
                    .Set(c => c.TPPVOL_SEND_THRESHOLD, item.TPPVOL_SEND_THRESHOLD)
                    .Set(c => c.TPPVOL_INDEX_GRP, item.TPPVOL_INDEX_GRP)
                    .Set(c => c.TPPVOL_UP_DOWN_RATE, item.TPPVOL_UP_DOWN_RATE)
                    .Update();
            }
        }
    }
}