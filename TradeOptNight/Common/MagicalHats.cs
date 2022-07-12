using CrossModel.Enum;
using DataEngine;
using Eagle;
using System;
using System.IO;
using System.Windows.Threading;
using TradeOptNightData.Gates.Common;
using TradeOptNightData.Models.Common;

namespace TradeOptNight.Common
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
        public static bool CheckMsgServerConnection()
        {
            try
            {
                IEagleGate eagleGate = new MexGate(MsgSysType.FutNight, "CheckMsgServerConnection", "all");
                EagleArgs ea = new EagleArgs();
                ea.AddEagleContent(new EagleContent() { Item = "Hello", Value = "World" });
                eagleGate.AddArgument(ea);
                eagleGate.Send();
                return true;
            }
            catch (Exception ex)
            {
                MessageBoxExService.Instance().Error(ex.Message);
                return false;
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