using LinqToDB.Configuration;
using LinqToDB.Data;

namespace DataEngine
{
    internal class LinqToDbDataConnection : DataConnection
    {
        public LinqToDbDataConnection(LinqToDbConnectionOptions option) : base(option)
        {
        }
    }
}