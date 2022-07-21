using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.PrefixC
{
    public class D_C2100<T> : ParentGate
    {
        public D_C2100(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListAllData(DateTime prodDate)
        {
            var sql = @"
                        SELECT
	                            TPPBP_RT_MARKET_CLOSE,
	                            TPPBP_PROD_ID,
	                            TPPBP_THERICAL_P,
	                            TPPBP_THERICAL_P_REF,
	                            PDK_KIND_ID,
	                            STR_REPLACE(PDK_NAME,'期貨','')AS PDK_NAME,
	                            PDK_STOCK_ID,
	                            PDK_PARAM_KEY,
	                            PDK_MARKET_CLOSE,
	                            PDK_ON_CODE,
	                            PDK_PROD_IDX,
	                            PROD_SETTLE_DATE,
	                            PROD_END_DATE,
	                            PROD_TPPBP_THERICAL_P,
	                            PROD_RAISE_PRICE,
	                            PROD_FALL_PRICE,
	                            0.0000 AS TARGET_PRICE,
	                            0.0000 AS CHINASTOCK_FLUCTUATION,
	                            0 AS IS_ADJUST_NEXT_DATE,
	                            0 AS IS_NEW_PRODUCT_OR_MONTH,
                                0 AS COMPUTE_SPREAD_PERCENT
                        FROM TPPBP A
                        LEFT JOIN PDK B ON SUBSTRING(A.TPPBP_PROD_ID, 1, 3) = B.PDK_KIND_ID
                        LEFT JOIN PROD C ON A.TPPBP_PROD_ID = C.PROD_ID
                        WHERE PDK_SUBTYPE = 'S'
                        AND PROD_END_DATE > @PROD_END_DATE
                        AND PROD_EXPIRE_CODE <> 'Y'
                        ORDER BY TPPBP_PROD_ID
                        ";

            var p = new DynamicParameters();
            p.Add("@PROD_END_DATE", prodDate);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }

        public IEnumerable<T> ListCadjAndSdi(DateTime baseDate)
        {
            var sql = @"
                        SELECT * FROM CADJ A
                        LEFT JOIN SDI B ON A.CADJ_BF_STOCK_ID = B.SDI_STOCK_ID AND B.SDI_BASE_DATE = @BASE_DATE
                        WHERE CADJ_BASE_DATE = @BASE_DATE
                        ";

            var p = new DynamicParameters();
            p.Add("@BASE_DATE", baseDate);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}