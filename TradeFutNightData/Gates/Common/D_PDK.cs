﻿using Dapper;
using DataEngine;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_PDK : D_PDK<PDK>
    {
        public D_PDK(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_PDK<T> : ParentGate
    {
        public PDK getByParamKey(string paramKey)
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_PARAM_KEY == paramKey);

            return query.SingleOrDefault();
        }

        public IEnumerable<PDK> ListAll()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_STATUS_CODE != '0')
                .OrderBy(c => c.PDK_KIND_ID);

            return query.ToList();
        }

        public IEnumerable<PDK> ListDistinctParamKey()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_STATUS_CODE != '0')
                .OrderBy(c => c.PDK_PARAM_KEY)
                .Select(c => new PDK() { PDK_PARAM_KEY = c.PDK_PARAM_KEY }).Distinct();

            return query.ToList();
        }

        public IList<PDK> ListDistinctParamKeyCanQuote()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_STATUS_CODE != '0' && c.PDK_QUOTE_CODE == 'Y')
                .OrderBy(c => c.PDK_PARAM_KEY)
                .Select(c => new PDK() { PDK_PARAM_KEY = c.PDK_PARAM_KEY }).Distinct();

            return query.ToList();
        }
    }
}