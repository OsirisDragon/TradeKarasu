using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.Prefix5
{
    public class D_50302<T> : ParentGate
    {
        public D_50302(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListData()
        {
            var sql = @"
                        SELECT  FMIFSTG.FMIFSTG_PROD_ID ,
                                SUBSTRING(FMIFSTG.FMIFSTG_PROD_ID ,1,3) AS FMIFSTG_PROD_ID_3,
		                        F.PROD_SETTLE_DATE AS FIRST_SETTLE_DATE,
		                        S.PROD_SETTLE_DATE AS SECOND_SETTLE_DATE,
		                        CONVERT(NUMERIC(10,4),FMIFSTG.FMIFSTG_OPEN_PRICE) AS FMIFSTG_OPEN_PRICE,
		                        CONVERT(NUMERIC(10,4),FMIFSTG.FMIFSTG_HIGH_PRICE) AS FMIFSTG_HIGH_PRICE,
		                        CONVERT(NUMERIC(10,4),FMIFSTG.FMIFSTG_LOW_PRICE) AS FMIFSTG_LOW_PRICE,
		                        CONVERT(NUMERIC(10,4),FMIFSTG.FMIFSTG_CLOSE_PRICE) AS FMIFSTG_CLOSE_PRICE,
		                        CONVERT(NUMERIC(10,4),FMIFSTG.FMIFSTG_LAST_BUY_PRICE) AS FMIFSTG_LAST_BUY_PRICE,
		                        CONVERT(NUMERIC(10,4),FMIFSTG.FMIFSTG_LAST_SELL_PRICE) AS FMIFSTG_LAST_SELL_PRICE,
		                        CONVERT(NUMERIC(10,4),FMIFSTG.FMIFSTG_TERM_HIGH_PRICE) AS FMIFSTG_TERM_HIGH_PRICE,
		                        CONVERT(NUMERIC(10,4),FMIFSTG.FMIFSTG_TERM_LOW_PRICE) AS FMIFSTG_TERM_LOW_PRICE,
		                        FMIFSTG.FMIFSTG_M_QNTY_TAL,P.PDK_SUBTYPE
                        FROM FMIFSTG, SODF, PROD F, PROD S, PDK P,OSWCUR O
                        WHERE FMIFSTG.FMIFSTG_M_QNTY_TAL > 0
                            AND FMIFSTG_PROD_ID = SODF_PROD_ID
                            AND SODF_BS_CODE = 'B'
                            AND SODF_FIRST_PROD = F.PROD_ID
                            AND SODF_SECOND_PROD = S.PROD_ID
                            AND SUBSTRING(FMIFSTG.FMIFSTG_PROD_ID,1,3) = P.PDK_KIND_ID
                            AND P.PDK_MARKET_CLOSE = convert(char(2),OSWCUR_OSW_GRP)
                            AND OSWCUR_CURR_OPEN_SW >= 110
                        ORDER BY SUBSTRING(FMIFSTG.FMIFSTG_PROD_ID,1,3), FIRST_SETTLE_DATE,SECOND_SETTLE_DATE
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}