using Dapper;
using DataEngine;
using System;
using TradeOptNightData.Models.Tfxm;

namespace TradeOptNightData.Gates.Tfxm
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
    }
}