using Dapper;
using DataEngine.Extension;
using Shield.Mapping;

namespace DataEngine
{
    public static class EngineSetting
    {
        public static SettingDatabaseInfo DataBaseInfo;

        /// <summary>
        /// 只執行一次
        /// </summary>
        static EngineSetting()
        {
            SqlMapper.AddTypeHandler(new TrimmedStringHandler());
        }
    }
}