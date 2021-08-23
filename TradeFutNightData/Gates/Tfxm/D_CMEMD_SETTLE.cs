using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNightData.Gates.Tfxm
{
    public class D_CMEMD_SETTLE : D_CMEMD_SETTLE<CMEMD_SETTLE>
    {
        public D_CMEMD_SETTLE(DalSession das){ this._das = das; }
    }

    public class D_CMEMD_SETTLE<T> : ParentGate
    {
        public T GetLatest(DateTime whichDate, string symbol, string transferMonth)
        {
            var sql = @"
                        SELECT TOP 1 * 
                        FROM  CMEMD_SETTLE
                        WHERE CMEMD_SETTLE_UPDATE_TIME <= @whichDate 
                        AND CMEMD_SETTLE_SYMBOL LIKE @symbol 
                        AND CMEMD_SETTLE_CRT_MNTH = @transferMonth 
                        ORDER BY CMEMD_SETTLE_UPDATE_TIME DESC
                        ";

            var p = new DynamicParameters();
            p.Add("@whichDate", whichDate);
            p.Add("@symbol", symbol);
            p.Add("@transferMonth", transferMonth);

            var result = _das.Conn.QueryFirstOrDefault<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<T> ListBySymbol(DateTime whichDate, string symbol)
        {
            // 我先預設抓取30天的資料
            DateTime untilDate = whichDate.AddDays(-30);

            var sql = @"
                        SELECT * 
                        FROM  CMEMD_SETTLE
                        WHERE CMEMD_SETTLE_UPDATE_TIME <= @whichDate 
                        AND CMEMD_SETTLE_UPDATE_TIME > @untilDate
                        AND CMEMD_SETTLE_SYMBOL LIKE @symbol 
                        ORDER BY CMEMD_SETTLE_UPDATE_TIME DESC
                        ";

            var p = new DynamicParameters();
            p.Add("@whichDate", whichDate);
            p.Add("@untilDate", untilDate);
            p.Add("@symbol", symbol);                               

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}
