using DataEngine;
using Shield.File;
using Shield.Mapping;

namespace TradeOptNightData
{
    public class DataBaseEngine
    {
        public static void Initial()
        {
            EngineSetting.DataBaseInfo = SettingFile.Database.Options_AH;
        }

        /// <summary>
        /// 取得另一邊的DB，如果是期貨就取選擇權，如果是選擇權就取期貨
        /// </summary>
        /// <returns></returns>
        public static SettingDatabaseInfo GetOppositeDb()
        {
            SettingDatabaseInfo result = null;

            if (EngineSetting.DataBaseInfo == SettingFile.Database.Futures)
            {
                result = SettingFile.Database.Options;
            }
            else if (EngineSetting.DataBaseInfo == SettingFile.Database.Options)
            {
                result = SettingFile.Database.Futures;
            }
            else if (EngineSetting.DataBaseInfo == SettingFile.Database.Futures_AH)
            {
                result = SettingFile.Database.Options_AH;
            }
            else if (EngineSetting.DataBaseInfo == SettingFile.Database.Options_AH)
            {
                result = SettingFile.Database.Futures_AH;
            }

            return result;
        }
    }
}