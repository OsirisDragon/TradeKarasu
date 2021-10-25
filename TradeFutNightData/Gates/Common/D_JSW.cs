using Dapper;
using DataEngine;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_JSW : D_JSW<JSW>
    {
        public D_JSW(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_JSW<T> : ParentGate
    {
        public IEnumerable<JSW> ListByID(string JSW_ID)
        {
            var query = _das.DataConn.GetTable<JSW>().Where(c => c.JSW_ID == JSW_ID);
            return query.ToList();
        }

        /// <summary>
        /// 根據JRF裡面設定的內容來更新JSW
        /// </summary>
        public void UpdateJswByJrf(string TXN_ID)
        {
            int affectedRows = -1;

            var dJRF = new D_JRF(_das);
            var jrf = dJRF.GetTopOne(TXN_ID);

            if (jrf != null)
            {
                char jobType = jrf.JRF_ORIG_JOB_TYPE;

                var p = new DynamicParameters();
                p.Add("@job_id", TXN_ID);
                p.Add("@job_type", jobType);

                affectedRows = _das.Conn.Execute(BuildCommand<T>("proc_job_update", p, commandType: CommandType.StoredProcedure));
            }
        }
    }
}