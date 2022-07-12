using DataEngine;
using System.Data;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_PUT : D_PUT<PUT>
    {
        public D_PUT(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_PUT<T> : ParentGate
    {
        public PUT Get(string PUT_KIND_ID, decimal? INPUT_VALUE)
        {
            var query = _das.DataConn.GetTable<PUT>()
                .Where(c => c.PUT_KIND_ID == PUT_KIND_ID && c.PUT_MIN <= INPUT_VALUE && c.PUT_MAX > INPUT_VALUE)
                .Select(c => new PUT { PUT_UNIT = c.PUT_UNIT });

            return query.SingleOrDefault();
        }

        public decimal GetPutUnit(string PARAM_KEY, decimal? INPUT_VALUE)
        {
            decimal result = 0;

            var put = Get(PARAM_KEY, INPUT_VALUE);

            if (put != null)
            {
                result = put.PUT_UNIT;
            }

            return result;
        }
    }
}