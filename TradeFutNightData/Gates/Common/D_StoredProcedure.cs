using Dapper;
using DataEngine;
using System;
using System.Collections.Generic;
using System.Data;

namespace TradeFutNightData.Gates.Common
{
    public class D_StoredProcedure<T> : ParentGate
    {
        public D_StoredProcedure(DalSession das)
        {
            this._das = das;
        }

        private void AddReturnParameter(DynamicParameters p)
        {
            p.Add(name: "@RetVal", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
        }

        private int GetReturnParameter(DynamicParameters p)
        {
            return p.Get<int>("@RetVal");
        }

        private void ThrowErrorByReturnValue(int retVal)
        {
            if (retVal != 0)
            {
                throw new Exception("ReturnValue為" + retVal);
            }
        }

        public IEnumerable<T> proc_AH_settle_price(string xact_type, int osw_grp, int dsp_grp)
        {
            var spName = "proc_AH_settle_price";

            try
            {
                var p = new DynamicParameters();
                p.Add("@xact_type", xact_type);
                p.Add("@osw_grp", osw_grp);
                p.Add("@dsp_grp", dsp_grp);
                AddReturnParameter(p);
                var result = _das.Conn.Query<T>(BuildCommand<T>(spName, p, commandType: CommandType.StoredProcedure));
                var retVal = GetReturnParameter(p);
                ThrowErrorByReturnValue(retVal);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SP錯誤(" + spName + ")，" + ex.Message);
            }
        }
    }
}