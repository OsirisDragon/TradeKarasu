using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNightData.Gates.Tfxm
{
    public class D_CMEMD_SETTLE : D_CMEMD_SETTLE<CMEMD_SETTLE>
    {
        public D_CMEMD_SETTLE(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_CMEMD_SETTLE<T> : ParentGate
    {
        public T GetLatest(DateTime whichDate, string symbol, string transferMonth)
        {
            var sql = @"
                        SELECT TOP 1 *
                        FROM CMEMD_SETTLE
                        WHERE CMEMD_SETTLE_UPDATE_TIME <= @CMEMD_SETTLE_UPDATE_TIME
                        AND CMEMD_SETTLE_SYMBOL LIKE @CMEMD_SETTLE_SYMBOL
                        AND CMEMD_SETTLE_CRT_MNTH = @CMEMD_SETTLE_CRT_MNTH
                        ORDER BY CMEMD_SETTLE_UPDATE_TIME DESC
                        ";

            var p = new DynamicParameters();
            p.Add("@CMEMD_SETTLE_UPDATE_TIME", whichDate);
            p.Add("@CMEMD_SETTLE_SYMBOL", symbol);
            p.Add("@CMEMD_SETTLE_CRT_MNTH", transferMonth);

            var result = _das.Conn.QueryFirstOrDefault<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<T> ListBySymbol(DateTime whichDate, string symbol)
        {
            // 我先預設抓取30天的資料
            DateTime untilDate = whichDate.AddDays(-30);

            var sql = @"
                        SELECT *
                        FROM CMEMD_SETTLE
                        WHERE CMEMD_SETTLE_UPDATE_TIME <= @CMEMD_SETTLE_UPDATE_TIME
                        AND CMEMD_SETTLE_UPDATE_TIME > @CMEMD_SETTLE_UPDATE_TIME
                        AND CMEMD_SETTLE_SYMBOL LIKE @CMEMD_SETTLE_SYMBOL
                        ORDER BY CMEMD_SETTLE_UPDATE_TIME DESC
                        ";

            var p = new DynamicParameters();
            p.Add("@CMEMD_SETTLE_UPDATE_TIME", whichDate);
            p.Add("@CMEMD_SETTLE_UPDATE_TIME", untilDate);
            p.Add("@CMEMD_SETTLE_SYMBOL", symbol);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}