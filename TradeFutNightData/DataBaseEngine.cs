using DataEngine;
using Shield.File;

namespace TradeFutNightData
{
    public class DataBaseEngine
    {
        public static void Initial()
        {
            EngineSetting.DataBaseInfo = SettingFile.Database.Futures_AH;
        }
    }
}
