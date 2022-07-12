using Dapper;
using DataEngine;
using System;
using TradeOptNightData.Models.Tfxm;

namespace TradeOptNightData.Gates.Tfxm
{
    public class D_CMEMD : D_CMEMD<CMEMD>
    {
        public D_CMEMD(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_CMEMD<T> : ParentGate
    {
        public T GetLatest(DateTime whichDate, string symbol, string transferMonth)
        {
            var sql = @"
                        SELECT TOP 1 CMEMD_PX
                        FROM CMEMD
                        WHERE CMEMD_UPDATE_TIME <= @CMEMD_UPDATE_TIME
                        AND CMEMD_SYMBOL LIKE @CMEMD_SYMBOL
                        AND CMEMD_CRT_MNTH = @CMEMD_CRT_MNTH
                        ORDER BY CMEMD_UPDATE_TIME DESC
                        ";

            var p = new DynamicParameters();
            p.Add("@CMEMD_UPDATE_TIME", whichDate);
            p.Add("@CMEMD_SYMBOL", symbol);
            p.Add("@CMEMD_CRT_MNTH", transferMonth);

            var result = _das.Conn.QueryFirstOrDefault<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}