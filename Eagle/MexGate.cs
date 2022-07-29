using System;
using System.Collections.Generic;
using System.IO;

namespace Eagle
{
    public class MexGate : IEagleGate
    {
        private string _externalExePath;
        private List<List<EagleContent>> _listEagleContent = new List<List<EagleContent>>();

        public string Subject { get; set; }

        public string Key { get; set; }

        public MexGate(MsgSysType msgSysType, string subject, string key)
        {
            Subject = subject;
            Key = key;
            switch (msgSysType)
            {
                case MsgSysType.FutDay:
                    _externalExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MexDragon\\future\\MexDragonSendForm.exe");
                    break;

                case MsgSysType.FutNight:
                    _externalExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MexDragon\\future_night\\MexDragonSendForm.exe");
                    break;

                case MsgSysType.OptDay:
                    _externalExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MexDragon\\option\\MexDragonSendForm.exe");
                    break;

                case MsgSysType.OptNight:
                    _externalExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MexDragon\\option_night\\MexDragonSendForm.exe");
                    break;
            }
        }

        private EagleResult Check()
        {
            if (String.IsNullOrEmpty(_externalExePath))
            {
                throw new Exception("請設定Mex執行檔路徑");
            }

            if (!File.Exists(_externalExePath))
            {
                throw new Exception("找不到MexDragon資料夾與檔案");
            }

            FileAttributes fileAttr = File.GetAttributes(_externalExePath);

            string directory = _externalExePath;

            if (fileAttr.HasFlag(FileAttributes.Directory))
            {
            }
            else
            {
                directory = Path.GetDirectoryName(_externalExePath);
            }

            EagleResult result = new EagleResult();

            if (!File.Exists(Path.Combine(directory, "mapp.xml")))
            {
                throw new Exception(directory + "裡面無mapp.xml的檔案");
            }

            if (!File.Exists(Path.Combine(directory, "mex_ip.xml")))
            {
                throw new Exception(directory + "裡面無mex_ip.xml的檔案");
            }

            return result;
        }

        public EagleResult Send()
        {
            Check();

            EagleResult result = new EagleResult();
            string jsonStr = GenerateJsonStr();

            int commandReturn = Command.Run(_externalExePath, jsonStr);

            if (commandReturn == 0)
            {
                result.EagleStatus = EagleStatus.Success;
            }

            return result;
        }

        public EagleResult SendAndReceiveData(string subscribeSubject, string subscribeKey, int waitSecond)
        {
            Check();

            EagleResult result = new EagleResult();
            string jsonStr = GenerateJsonStr();

            string commandReturn = Command.RunAndReceiveData(_externalExePath, jsonStr + " " + subscribeSubject + " " + subscribeKey + " " + waitSecond);

            if (commandReturn != "")
            {
                result.EagleStatus = EagleStatus.Success;
                result.ReceiveData = commandReturn;
            }
            else
            {
                result.EagleStatus = EagleStatus.Fail;
            }

            return result;
        }

        private string GenerateJsonStr()
        {
            string jsonStr = "";
            jsonStr += "\"[";

            foreach (var everyList in GetListEagleContent())
            {
                jsonStr += "{'Subject':'" + Subject + "','Key':'" + Key + "','Contents':[";

                foreach (EagleContent ec in everyList)
                {
                    if (ec.Value is char)
                    {
                        jsonStr += "{'Item':'" + ec.Item + "','Type':'String','Value':'" + ((char)ec.Value).ToString() + "'}";
                    }
                    else if (ec.Value is string)
                    {
                        jsonStr += "{'Item':'" + ec.Item + "','Type':'String','Value':'" + ec.Value + "'}";
                    }
                    else if (ec.Value is int)
                    {
                        jsonStr += "{'Item':'" + ec.Item + "','Type':'Int','Value':'" + ec.Value + "'}";
                    }
                    else if (ec.Value is double)
                    {
                        jsonStr += "{'Item':'" + ec.Item + "','Type':'Double','Value':'" + ec.Value + "'}";
                    }

                    jsonStr += ",";
                }

                jsonStr = jsonStr.TrimEnd(',');

                jsonStr += "]}";
                jsonStr += ",";
            }

            jsonStr = jsonStr.TrimEnd(',');
            jsonStr += "]\"";
            return jsonStr;
        }

        public void AddArgument(EagleArgs args)
        {
            _listEagleContent.Add(args.ListEagleContent);
        }

        public List<List<EagleContent>> GetListEagleContent()
        {
            return _listEagleContent;
        }
    }
}