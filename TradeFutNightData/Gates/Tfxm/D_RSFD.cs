using Dapper;
using DataEngine;
using System;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNightData.Gates.Tfxm
{
    public class D_RSFD : D_RSFD<RSFD>
    {
        public D_RSFD(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_RSFD<T> : ParentGate
    {
        public RSFD GetLatestByStockId(DateTime ocfDate, string stockId)
        {
            var sql = @"
                                     SELECT TOP 1 * FROM RSFD
                                    WHERE RSFD_SID = @stockId
                                    AND RSFD_DATE > DATEADD(DAY, -30, @ocfDate)
                                    ORDER BY RSFD_DATE DESC
                                   ";

            var p = new DynamicParameters();
            p.Add("@stockId", stockId);
            p.Add("@ocfDate", ocfDate);

            var result = _das.Conn.QueryFirstOrDefault<RSFD>(BuildCommand<RSFD>(sql, p));

            return result;
        }
    }
}