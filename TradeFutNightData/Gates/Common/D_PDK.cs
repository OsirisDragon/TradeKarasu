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

        public IEnumerable<PDK> ListByParamKeyAndBtrdCode(string paramKey)
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_PARAM_KEY == paramKey && c.PDK_BTRD_CODE == 'Y');

            return query.ToList();
        }

        public IEnumerable<PDK> ListAll()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_STATUS_CODE != '0')
                .OrderBy(c => c.PDK_KIND_ID);

            return query.ToList();
        }

        public IEnumerable<PDK> ListNotExpire()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_EXPIRE_CODE != 'Y')
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

        public IList<PDK> ListDistinctParamKeyStock()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_SUBTYPE == 'S')
                .OrderBy(c => c.PDK_PARAM_KEY)
                .Select(c => new PDK() { PDK_PARAM_KEY = c.PDK_PARAM_KEY }).Distinct();

            return query.ToList();
        }

        public IList<PDK> ListDistinctKindIdNotStock()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c => c.PDK_SUBTYPE != 'S')
                .Select(c => new PDK() { PDK_KIND_ID = c.PDK_KIND_ID }).Distinct();

            return query.ToList();
        }

        public IList<PDK> ListDistinctKindIdAndSubtypeForPcm()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Where(c =>
                        (c.PDK_EXPIRY_TYPE != 'W' && (c.PDK_STATUS_CODE == 'N' || c.PDK_STATUS_CODE == 'P')) ||
                        c.PDK_EXPIRY_TYPE == 'W'
                )
                .Select(c => new PDK() { PDK_SUBTYPE = c.PDK_SUBTYPE, PDK_KIND_ID = c.PDK_KIND_ID }).Distinct();

            return query.ToList();
        }

        public IList<PDK> ListKindId()
        {
            var query = _das.DataConn.GetTable<PDK>()
                .Select(c => new PDK() { PDK_KIND_ID = c.PDK_KIND_ID });

            return query.ToList();
        }
    }
}