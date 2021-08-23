using Dapper;
using DataEngine;
using System;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNightData.Gates.Tfxm
{
    public class D_CMEMD : D_CMEMD<CMEMD>
    {
        public D_CMEMD(DalSession das){ this._das = das; }
    }

    public class D_CMEMD<T> : ParentGate
    {
        public T GetLatest(DateTime whichDate, string symbol, string transferMonth)
        {
            var sql = @"
                        SELECT TOP 1 CMEMD_PX 
                        FROM  CMEMD
                        WHERE CMEMD_UPDATE_TIME <= @whichDate 
                        AND CMEMD_SYMBOL LIKE @symbol 
                        AND CMEMD_CRT_MNTH = @transferMonth 
                        ORDER BY CMEMD_UPDATE_TIME DESC
                        ";

            var p = new DynamicParameters();
            p.Add("@whichDate", whichDate);
            p.Add("@symbol", symbol);
            p.Add("@transferMonth", transferMonth);

            var result = _das.Conn.QueryFirstOrDefault<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}
