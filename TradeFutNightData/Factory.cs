using DataEngine;
using Shield.Mapping;

namespace TradeFutNightData
{
    public class Factory
    {
        public static DalSession CreateDalSession(SettingDatabaseInfo dbInfo = null)
        {
            return new DalSession(dbInfo);
        }
    }
}