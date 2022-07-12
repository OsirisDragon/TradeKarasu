using Dapper;
using DataEngine;
using System.Collections.Generic;

//using TradeOptNightData.Models.Specific.PrefixA;

namespace TradeOptNightData.Gates.Specific.PrefixA
{
    //public class D_A9920 : D_A9920<T>
    //{
    //    public D_A9920(DalSession das)
    //    {
    //        this._das = das;
    //    }
    //}

    public class D_A9920<T> : ParentGate
    {
        public D_A9920(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListProdPdkTppbp()
        {
            var sql = @"
                        SELECT * FROM PROD A
                        LEFT JOIN PDK B ON SUBSTRING(A.PROD_ID, 1, 3) = B.PDK_KIND_ID
                        LEFT JOIN TPPBP C ON A.PROD_ID = C.TPPBP_PROD_ID
                        WHERE PROD_EXPIRE_CODE <> 'Y' AND PDK_SUBTYPE <> 'E'
                        ORDER BY PROD_ID_OUT ASC, PROD_SETTLE_DATE ASC
                        ";

            var p = new DynamicParameters();
            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}