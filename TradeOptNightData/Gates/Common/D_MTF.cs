using Dapper;
using DataEngine;
using System.Collections.Generic;

namespace TradeOptNightData.Gates.Common
{
    public class D_MTF<T> : ParentGate
    {
        public D_MTF(DalSession das)
        {
            this._das = das;
        }

        /// <summary>
        /// 列出每個PROD_ID的最後一筆成交價格和成交時間
        /// </summary>
        public IEnumerable<T> ListEveryProdLastRecord()
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
                        FROM MTF M, PROD, PDK, PGRP
                        WHERE PROD.PROD_ID = MTF_PROD_ID
                        AND SUBSTRING(PROD.PROD_ID, 1, 3) = PDK_KIND_ID
                        AND MTF_SEQ_NO IN (
                            SELECT MAX(MTF_SEQ_NO)
                            FROM MTF
                            WHERE MTF_PROD_ID1 = NULL
                            AND MTF_QNTY > 0
                            AND MTF_ORDER_KIND = 'I' OR (MTF_ORDER_KIND = 'Z')
                            GROUP BY MTF_PROD_ID
                        )
                        AND SUBSTRING(PDK_KIND_ID, 1, 2) = PGRP_KIND_ID
                        ORDER BY PROD_ID_OUT, PROD_SETTLE_DATE
                        ";

            var result = _das.Conn.Query<T>(BuildCommand<T>(sql));

            return result;
        }
    }
}