using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Specific.Prefix5
{
    public class D_50301<T> : ParentGate
    {
        public D_50301(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> ListData()
        {
            var sql = @"
                        SELECT  FMIF_PROD_ID ,
                                FMIF_OPEN_PRICE,
                                FMIF_HIGH_PRICE,
                                FMIF_LOW_PRICE,
                                FMIF_CLOSE_PRICE,
                                FMIF_LAST_BUY_PRICE,
                                FMIF_LAST_SELL_PRICE,
                                FMIF_UP_DOWN_VAL,
                                FMIF_TERM_HIGH_PRICE,
                                FMIF_TERM_LOW_PRICE,
                                FMIF_M_QNTY_TAL,
                                FMIF_SETTLE_PRICE,
                                FMIF_OPEN_INTEREST,
                                FMIF_M_QNTY_TAL_S,
                                CLSPRC_SETTLE_PRICE,
                                PROD_SETTLE_DATE,
                                PROD_PC_CODE,
                                PROD_STRIKE_PRICE,
                                PROD_ID_OUT,
                                PDK_SUBTYPE,
                                BMIF_M_QNTY_TAL
                        FROM FMIF
                        INNER JOIN CLSPRC ON FMIF_PROD_ID = CLSPRC_PROD_ID
                        INNER JOIN PROD ON FMIF_PROD_ID = PROD_ID
                        INNER JOIN PDK ON SUBSTRING(PROD_ID, 1, 3) = PDK_KIND_ID
                        INNER JOIN OSWCUR ON CAST(PDK_MARKET_CLOSE AS INT) = OSWCUR_OSW_GRP
                        LEFT JOIN BMIF ON FMIF_PROD_ID = BMIF_PROD_ID
                        --只列出已經收盤的
                        WHERE OSWCUR_CURR_OPEN_SW>=110
                        ORDER BY PROD_ID_OUT ASC, PROD_SETTLE_DATE ASC
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }

        public IEnumerable<T> ListForGenerateFile()
        {
            var sql = @"
                        SELECT  PDK_KIND_ID,
                                PDK_STOCK_ID,
                                PROD_SETTLE_DATE,
                                FMIF_SETTLE_PRICE
                        FROM FMIF, CLSPRC, PROD, PDK
                        WHERE FMIF_PROD_ID = CLSPRC_PROD_ID
                        AND PROD_ID = CLSPRC_PROD_ID
                        AND SUBSTRING(PROD_ID,1,3) = PDK_KIND_ID
                        AND PDK_PARAM_KEY IN ('STF','ETF')
                        ORDER BY PDK_KIND_ID
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }

        public IEnumerable<T> ListAdjForSumOiZero()
        {
            var sql = @"
                        SELECT SUBSTRING(FMIF_PROD_ID,1,2) AS KIND_ID_FOR_TWO, PDK_STOCK_ID, PDK_NAME
                        FROM FMIF, PROD, PDK
                        WHERE FMIF_PROD_ID = PROD_ID
                        AND SUBSTRING(FMIF_PROD_ID, 1, 3) = PDK_KIND_ID
                        AND SUBSTRING(FMIF_PROD_ID, 3, 1) <>'F'
                        GROUP BY SUBSTRING(FMIF_PROD_ID, 1, 2), PDK_STOCK_ID, PDK_NAME
                        HAVING SUM(FMIF_OPEN_INTEREST) = 0
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }

        public IEnumerable<T> ListStandardForSumOiZero()
        {
            var sql = @"
                        SELECT SUBSTRING(FMIF_PROD_ID,1,2) AS KIND_ID_FOR_TWO, PDK_STOCK_ID, PDK_NAME
                        FROM FMIF, PROD, PDK
                        WHERE FMIF_PROD_ID = PROD_ID
                        AND SUBSTRING(FMIF_PROD_ID, 1, 3) = PDK_KIND_ID
                        AND SUBSTRING(FMIF_PROD_ID, 3, 1) ='F'
                        GROUP BY SUBSTRING(FMIF_PROD_ID, 1, 2), PDK_STOCK_ID, PDK_NAME
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}