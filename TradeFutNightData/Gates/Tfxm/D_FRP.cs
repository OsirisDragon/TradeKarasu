using Dapper;
using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNightData.Gates.Tfxm
{
    public class D_FRP : D_FRP<FRP>
    {
        public D_FRP(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_FRP<T> : ParentGate
    {
        public FRP GetLatestByProdId(string prodId)
        {
            var sql = @"
                        SELECT TOP 1 *
                        FROM FRP
                        WHERE FRP_PROD_ID LIKE @PROD_ID
                        ORDER BY FRP_UPDATE_TIME DESC
                        ";

            var p = new DynamicParameters();
            p.Add("@PROD_ID", prodId);

            var result = _das.Conn.QueryFirstOrDefault<FRP>(BuildCommand<FRP>(sql, p));

            return result;
        }

        public void Update(IList<FRP> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.GetTable<FRP>()
                    .Where(c => c.FRP_PROD_ID == item.OriginalData.FRP_PROD_ID && c.FRP_UPDATE_TIME == item.OriginalData.FRP_UPDATE_TIME)
                    .Set(c => c.FRP_PX, item.FRP_PX)
                    .Set(c => c.FRP_USER_ID, item.FRP_USER_ID)
                    .Set(c => c.FRP_W_TIME, item.FRP_W_TIME)
                    .Set(c => c.FRP_CONFIRM, item.FRP_CONFIRM)
                    .Update();
            }
        }
    }
}