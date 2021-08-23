using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine.Extension
{
    class TrimmedStringHandler : SqlMapper.TypeHandler<string>
    {
        public override string Parse(object value)
        {
            string result = (value as string)?.Trim();
            return result;
        }

        public override void SetValue(IDbDataParameter parameter, string value)
        {
            parameter.Value = value;
        }
    }
}
