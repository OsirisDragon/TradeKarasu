using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeFutNightData.Gates.Specific.Prefix5
{
    public class D_50333<T> : ParentGate
    {
        public D_50333(DalSession das)
        {
            this._das = das;
        }

        public IEnumerable<T> List()
        {
            var sql = @"
                                    SELECT  MTF_PRICE,
                                                    MTF_ORIG_TIME,
                                                    MTF_QNTY,
                                                    PROD_ID_OUT,
                                                    PROD_SETTLE_DATE,
                                                    PDK_MARKET_CLOSE,
                                                    PGRP_OSW_GRP,
                                                    PDK_SUBTYPE
                                    FROM MTF M , PROD ,PDK,PGRP
                                    WHERE PROD.PROD_ID = MTF_PROD_ID
                                    AND SUBSTRING(PROD.PROD_ID,1,3) = PDK_KIND_ID
                                    AND MTF_SEQ_NO IN (
                                                    SELECT MAX(MTF_SEQ_NO)
                                                    FROM MTF
                                                    WHERE MTF_PROD_ID1  = NULL
                                                    AND MTF_QNTY > 0
                                                    AND MTF_ORDER_KIND = 'I' OR ( MTF_ORDER_KIND = 'Z'  )
                                                    GROUP BY MTF_PROD_ID
                                    )
                                    AND SUBSTRING(PDK_KIND_ID,1,2)=PGRP_KIND_ID
                                    ORDER BY PROD_ID_OUT,PROD_SETTLE_DATE
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql, null));

            return result;
        }
    }
}