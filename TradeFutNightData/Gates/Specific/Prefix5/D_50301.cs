using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix5
{
    public class D_50301<T> : ParentGate
    {
        public D_50301(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                        SELECT  FMIF.FMIF_PROD_ID ,
                                convert(numeric(10,4),FMIF.FMIF_OPEN_PRICE) AS FMIF_OPEN_PRICE,
                                convert(numeric(10,4),FMIF.FMIF_HIGH_PRICE) AS FMIF_HIGH_PRICE,
                                convert(numeric(10,4),FMIF.FMIF_LOW_PRICE) AS FMIF_LOW_PRICE,
                                convert(numeric(10,4),FMIF.FMIF_CLOSE_PRICE) AS FMIF_CLOSE_PRICE,
                                convert(numeric(10,4),FMIF.FMIF_LAST_BUY_PRICE) AS FMIF_LAST_BUY_PRICE,
                                convert(numeric(10,4),FMIF.FMIF_LAST_SELL_PRICE) AS FMIF_LAST_SELL_PRICE,
                                convert(numeric(10,4),FMIF.FMIF_UP_DOWN_VAL) AS FMIF_UP_DOWN_VAL,
                                convert(numeric(10,4),CLSPRC.CLSPRC_SETTLE_PRICE) AS CLSPRC_SETTLE_PRICE,
                                convert(numeric(10,4),FMIF.FMIF_TERM_HIGH_PRICE) AS FMIF_TERM_HIGH_PRICE,
                                convert(numeric(10,4),FMIF.FMIF_TERM_LOW_PRICE) AS FMIF_TERM_LOW_PRICE,
                                FMIF.FMIF_M_QNTY_TAL,
                                convert(numeric(10,4),FMIF.FMIF_SETTLE_PRICE) AS FMIF_SETTLE_PRICE,
                                FMIF.FMIF_OPEN_INTEREST,
                                PROD.PROD_SETTLE_DATE,
                                PROD.PROD_PC_CODE,
                                PROD.PROD_STRIKE_PRICE,
                                PROD.PROD_ID_OUT,
                                FMIF.FMIF_M_QNTY_TAL_S,
                                BMIF_M_QNTY_TAL
                        FROM FMIF, CLSPRC, PROD, BMIF
                        WHERE (FMIF.FMIF_PROD_ID = CLSPRC.CLSPRC_PROD_ID)
                        AND (PROD.PROD_ID = CLSPRC.CLSPRC_PROD_ID)
                        AND (FMIF_PROD_ID *= BMIF_PROD_ID )
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}