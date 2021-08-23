using CrossModel.Enum;
using DataEngine;
using System;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Common
{
    public class MagicalHats
    {
        private static OCF _ocf;

        public static OCF Ocf
        {
            get
            {
                if (_ocf == null)
                {
                    using (var das = new DalSession())
                    {
                        var dOCF = new D_OCF(das);
                        var ocf = dOCF.Get();
                        _ocf = ocf;
                    }
                }

                return _ocf;
            }
        }

        public static string UserID { get; set; }
        public static string UserName { get; set; }

        public static void LogToDb(string userID, string logItem, string keyData)
        {
            using (var das = new DalSession())
            {
                var dLogf = new D_LOGF(das);
                dLogf.Insert(userID, logItem, keyData);
            }
        }

        public static string UniformFileName(SystemType systemType, string programID, string fileDescription, FileType fileType)
        {
            string result = "";

            if (fileDescription != "")
            {
                fileDescription = fileDescription + "_";
            }

            result = Enum.GetName(typeof(SystemType), systemType) + "_" + programID + "_" +
                fileDescription + DateTime.Now.ToString("yyyyMMdd_HHmmssFFF") + "." + Enum.GetName(typeof(FileType), fileType).ToLower();

            return result;
        }
    }
}