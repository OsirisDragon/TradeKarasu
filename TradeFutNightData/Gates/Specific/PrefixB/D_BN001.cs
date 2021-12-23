using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;
using TradeFutNightData.Models.Specific.PrefixB;

namespace TradeFutNightData.Gates.Specific.PrefixB
{
    public class D_BN001 : D_BN001<DTO_BN001>
    {
        public D_BN001(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_BN001<T> : ParentGate
    {
        public IEnumerable<T> ListByPgrpDspGrp(int pgrpDspGrp, DateTime specificDate, DateTime? ocfPrevDate, int applySecret)
        {
            var sql = @"
                        SELECT  FMIF_SETTLE_PRICE,
                                FMIF_PROD_ID,
                                FMIF_M_QNTY_TAL,
                                FMIF_M_COUNT_TAL,
                                PROD_SETTLE_DATE,
                                PROD_ID_OUT,
                                PROD_DELIVERY_DATE,
                                PROD_ID,
                                PROD_RAISE_PRICE,
                                PROD_RAISE_PRICE1,
                                PROD_RAISE_PRICE2,
                                PROD_FALL_PRICE,
                                PROD_FALL_PRICE1,
                                PROD_FALL_PRICE2,
                                PROD_THERICAL_P,
                                PDK_KIND_ID,
                                PDK_EXPIRY_TYPE,
                                PDK_PARAM_KEY,
                                PDK_NAME,
                                PDK_STOCK_ID,
                                PDK_PROD_IDX,
                                PDK_REMARK,
                                PDK_QUOTE_CODE,
                                PGRP_DSP_GRP,
                                0.0000                    AS CLSPRC_SETTLE_PRICE,
                                0.0000                    AS LAST_ONE_MIN_WEIGHT_AVG_PRICE,
                                0.0000                    AS LAST_BUY_PRICE,
                                0.0000                    AS LAST_SELL_PRICE,
                                0.0000                    AS BUY_SELL_MIDDLE,
                                0.0000                    AS ADJUST_TYPE,
                                0.0000                    AS INITIAL_SETTLE_PRICE,
                                000.00                    AS PERCENT,
                                0000.0000                 AS ACTUALS_CLOSE_PRICE_FLUCTUATION,
                                0000.0000                 AS LAST_DEAL_PRICE,
                                '1900/01/01'              AS LAST_DEAL_TIME,
                                0000000                   AS CLSPRC_OPEN_INTEREST,
                                ' '                       AS REMARK_FIRST,
                                ' '                       AS REMARK_SECOND,
                                SLT_SPREAD                AS SLT_SPREAD_NEAR_MONTH
                            FROM FMIF, PROD, PGRP, PDK, PMFCME, SLT
                            WHERE PGRP_DSP_GRP = @PGRP_DSP_GRP
                            AND FMIF_PROD_ID = PROD_ID
                            AND SUBSTRING(PROD_ID, 1, 3) = PDK_KIND_ID
                            AND SUBSTRING(PROD_ID, 1, 2) = PGRP_KIND_ID
                            AND PMFCME_KIND_ID = PDK_KIND_ID
                            AND PMFCME_MONTH = PROD_SETTLE_DATE
                            AND PDK_END_SESSION = '1'
                            AND PDK_PARAM_KEY = SLT_KIND_ID AND SLT_SPREAD_LONG = 1
                            AND PROD_END_DATE < @SPECIFIC_DATE

                            --如果使用密技是1的話，就會走這個條件，因為1 = 0是false
                            AND( @APPLY_SECRET = 0 OR(PROD_DELIVERY_DATE >= @SPECIFIC_DATE) )
                            --如果沒使用密技是0的話，就會走這個條件，因為0 = 1是false
                            AND( @APPLY_SECRET = 1 OR(PROD_DELIVERY_DATE > @SPECIFIC_DATE) )
                            --如果使用密技是1的話，忽略下面兩個條件
                            AND( @APPLY_SECRET = 1 OR(@OCF_PREV_DATE <= PROD_END_DATE) )
	                        AND( @APPLY_SECRET = 1 OR(PMFCME_END_DATE < @SPECIFIC_DATE) )
                            ORDER BY PDK_KIND_ID, PROD_SETTLE_DATE
                        ";

            var p = new DynamicParameters();
            p.Add("@PGRP_DSP_GRP", pgrpDspGrp);
            p.Add("@SPECIFIC_DATE", specificDate);
            p.Add("@OCF_PREV_DATE", ocfPrevDate);
            p.Add("@APPLY_SECRET", applySecret);

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, p));

            return result;
        }
    }
}