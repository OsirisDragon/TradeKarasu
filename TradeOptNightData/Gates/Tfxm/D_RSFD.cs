using Dapper;
using DataEngine;
using System;
using TradeOptNightData.Models.Tfxm;

namespace TradeOptNightData.Gates.Tfxm
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
                        --增加日期條件，減少資料搜尋量
                        SELECT TOP 1 *
                        FROM RSFD
                        WHERE RSFD_SID = @STOCK_ID
                        AND RSFD_DATE > DATEADD(DAY, -30, @OCF_DATE)
                        ORDER BY RSFD_DATE DESC
                       ";

            var p = new DynamicParameters();
            p.Add("@STOCK_ID", stockId);
            p.Add("@OCF_DATE", ocfDate);

            var result = _das.Conn.QueryFirstOrDefault<RSFD>(BuildCommand<RSFD>(sql, p));

            return result;
        }
    }
}