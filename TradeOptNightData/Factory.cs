using DataEngine;
using Shield.Mapping;

namespace TradeOptNightData
{
    public class Factory
    {
        public static DalSession CreateDalSession(SettingDatabaseInfo dbInfo = null)
        {
            return new DalSession(dbInfo);
        }
    }
}