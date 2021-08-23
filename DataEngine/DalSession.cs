using AdoNetCore.AseClient;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.DataProvider.Sybase;

//using Oracle.ManagedDataAccess.Client;
using Shield.Mapping;
using System;
using System.Data;

namespace DataEngine
{
    public class DalSession : IDisposable
    {
        private DataConnection _dataConnection = null;
        private IDbConnection _connection = null;
        private UnitOfWork _unitOfWork = null;
        private ConnectionObject _connectionObject = null;

        public DalSession(SettingDatabaseInfo dbInfo = null)
        {
            if (dbInfo == null)
                dbInfo = EngineSetting.DataBaseInfo;

            _connectionObject = ConnectionFactory(GetDataBaseBrand(dbInfo), dbInfo, this);
            _connection = _connectionObject.DbConnection;

            _connection.Open();
            _unitOfWork = new UnitOfWork(_connection);
        }

        public UnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// LinqToDb的DataConnection
        /// </summary>
        public DataConnection DataConn
        {
            get
            {
                if (_dataConnection == null)
                {
                    _dataConnection = new LinqToDbDataConnection(_connectionObject.LinqToDbConnectionOptions);
                }

                return _dataConnection;
            }
        }

        public IDbConnection Conn
        {
            get
            {
                return _connection;
            }
        }

        public void Begin()
        {
            _unitOfWork.Begin();
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Rollback()
        {
            _unitOfWork.Rollback();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _connection.Dispose();
        }

        private DataBaseBrand GetDataBaseBrand(SettingDatabaseInfo dbInfo)
        {
            DataBaseBrand result = DataBaseBrand.None;

            if (dbInfo != null)
            {
                switch (dbInfo.ProviderName)
                {
                    case "Sybase.Data.AseClient":
                        result = DataBaseBrand.SybaseAse;
                        break;

                    case "Oracle.ManagedDataAccess.Client":
                        result = DataBaseBrand.Oracle;
                        break;

                    case "Npgsql":
                        result = DataBaseBrand.Npgsql;
                        break;

                    default:
                        throw new Exception("找不到這個ProviderName:" + dbInfo.ProviderName);
                }
            }
            else
            {
                throw new Exception("尚未設定EngineSetting.DataBaseInfo喔");
            }

            return result;
        }

        private ConnectionObject ConnectionFactory(DataBaseBrand dbBrand, SettingDatabaseInfo dbInfo, DalSession das)
        {
            ConnectionObject resultConnObj = new ConnectionObject();
            var builder = new LinqToDbConnectionOptionsBuilder();

            if (dbBrand != DataBaseBrand.None)
            {
                if (dbInfo != null)
                {
                    switch (dbBrand)
                    {
                        case DataBaseBrand.SybaseAse:
                            resultConnObj.DbConnection = new AseConnection(dbInfo.ConnectionString);
                            resultConnObj.LinqToDbConnectionOptions = builder.UseConnectionFactory(SybaseTools.GetDataProvider(SybaseProviderAdapter.NativeProviderFactoryName),
                            () =>
                            {
                                return das.Conn;
                            }).Build();
                            break;

                        case DataBaseBrand.Oracle:
                            // 先註解起來，如果要用Oracle這個資料庫的話再打開，記得下載相關套件
                            //resultConnObj.DbConnection = new OracleConnection(dbInfo.ConnectionString);

                            break;

                        case DataBaseBrand.Npgsql:
                            // 先註解起來，如果要用Npgsql這個資料庫的話再打開，記得Nuget下載Npgsql相關套件
                            //resultConnObj.DbConnection = new NpgsqlConnection(dbInfo.ConnectionString);
                            //resultConnObj.LinqToDbConnectionOptions = builder.UseConnectionFactory(PostgreSQLTools.GetDataProvider(PostgreSQLVersion.v95),
                            //() =>
                            //{
                            //    return das.Conn;
                            //}).Build();
                            break;

                        case DataBaseBrand.None:
                            break;

                        default:
                            throw new Exception("找不到這個ProviderName:" + dbInfo.ProviderName);
                    }
                }
                else
                {
                    throw new Exception("尚未設定EngineSetting.DataBaseInfo喔");
                }
            }

            return resultConnObj;
        }
    }

    public class ConnectionObject
    {
        public IDbConnection DbConnection { get; set; }

        public LinqToDbConnectionOptions LinqToDbConnectionOptions { get; set; }
    }

    public enum DataBaseBrand
    {
        SybaseAse,
        Oracle,
        Npgsql,
        None
    }
}