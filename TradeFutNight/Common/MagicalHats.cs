using CrossModel.Enum;
using DataEngine;
using Eagle;
using System;
using System.Windows.Threading;
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
        public static string UserAD { get; set; }
        public static string UserName { get; set; }

        /// <summary>
        /// 送出一個Mex訊息測試一下看有沒有成功送出去
        /// </summary>
        public static void CheckMsgServerConnection()
        {
            IEagleGate eagleGate = new MexGate() { Subject = "CheckMsgServerConnection", Key = "all" };
            EagleArgs ea = new EagleArgs();
            ea.AddEagleContent(new EagleContent() { Item = "Hello", Value = "World" });
            eagleGate.Send(ea);
            eagleGate.Stop();
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