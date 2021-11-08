using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Eagle
{
    public class MexGate : IEagleGate
    {
        //private iMAppClass _ima;
        //private Subscriber _subscriber;
        private string _externalExePath;

        private List<List<EagleContent>> _listEagleContent = new List<List<EagleContent>>();

        public delegate void OnMessageArrivedEventHandler(EagleArgs args);

        public event OnMessageArrivedEventHandler OnMessageArrived;

        public delegate void OnStatusdEventHandler(EagleArgs args);

        public event OnStatusdEventHandler OnStatus;

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

        //public async Task<EagleResult> SendBackup(EagleArgs args)
        //{
        //    var result = new EagleResult();

        //    /*
        //    這裡用Thread包起來是因為，如果不用Thread的話，執行這個老舊的mex com元件後，
        //    再去畫面上的Grid裡面的Cell點個兩下，就會出現不知名的錯誤
        //    錯誤是OverflowException，HResult=0x80131516
        //    Message = 數學運算導致溢位
        //    Source = PresentationCore
        //    StackTrace: Render，RenderRecursive，UpdateChildren
        //    */
        //    var task = Task.Run(() =>
        //    {
        //        args.Path = Directory.GetCurrentDirectory();

        //        Check(args);

        //        if (_ima == null)
        //        {
        //            _ima = new iMAppClass();
        //            _ima.setPgmid(80, 100);
        //            _ima.start();
        //        }

        //        if (_ima.ConnectStatus() == false)
        //        {
        //            _ima.stop();
        //            _ima = null;
        //            result.EagleStatus = EagleStatus.Fail;
        //            throw new Exception("Mex ConnectStatus Fail");
        //        }

        //        PublisherClass ipub = new PublisherClass();
        //        MTreeXClass itree = (MTreeXClass)_ima.NewMTree();

        //        ipub = (PublisherClass)_ima.NewPublisher(Subject, Key);

        //        foreach (EagleContent ec in args.ListEagleContent)
        //        {
        //            if (ec.Value is string)
        //            {
        //                itree.append_char(ec.Item, ec.Value);
        //            }
        //            else if (ec.Value is char)
        //            {
        //                itree.append_char(ec.Item, ((char)ec.Value).ToString());
        //            }
        //            else if (ec.Value is int)
        //            {
        //                itree.append_int(ec.Item, (int)ec.Value);
        //            }
        //            else if (ec.Value is double)
        //            {
        //                itree.append_double(ec.Item, (double)ec.Value);
        //            }
        //        }

        //        int returnCode = ipub.send(itree);
        //        if (returnCode == 0)
        //        {
        //            result.EagleStatus = EagleStatus.Success;
        //        }
        //        else
        //        {
        //            result.EagleStatus = EagleStatus.Fail;
        //            throw new Exception("Mex的Subject:" + Subject + "與Key:" + Key + "傳送失敗，returnCode:" + returnCode);
        //        }

        //        itree.clearMtree();

        //        return result;
        //    });
        //    await task;

        //    return result;
        //}

        public EagleResult SendBBB(EagleArgs args)
        {
            Check();

            EagleResult result = new EagleResult();

            string jsonStr = "";

            jsonStr += "{'Subject':'" + Subject + "','Key':'" + Key + "','Contents':[";

            foreach (EagleContent ec in args.ListEagleContent)
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

            int commandReturn = Command.Run(_externalExePath, jsonStr);

            if (commandReturn == 0)
            {
                result.EagleStatus = EagleStatus.Success;
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
            jsonStr += "[";

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
            jsonStr += "]";
            return jsonStr;
        }

        public EagleResult SendToExeWithBatchJson()
        {
            Check();

            EagleResult result = new EagleResult();

            string jsonStr = "";

            jsonStr += "{'Subject':'" + Subject + "','Key':'" + Key + "','BatchContents':[";

            for (int i = 0; i < _listEagleContent.Count; i++)
            {
                var everyList = _listEagleContent[i];

                jsonStr += "[";

                foreach (var ec in everyList)
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

                jsonStr += "]";
                jsonStr += ",";
            }

            jsonStr = jsonStr.TrimEnd(',');
            jsonStr += "]}";

            int commandReturn = Command.Run(_externalExePath, jsonStr);

            if (commandReturn == 0)
            {
                result.EagleStatus = EagleStatus.Success;
            }

            return result;
        }

        public void Stop()
        {
            //if (_ima != null)
            //{
            //    _ima.stop();
            //    _ima = null;
            //}
        }

        //public EagleResult Receive(EagleArgs args)
        //{
        //    args.Path = Directory.GetCurrentDirectory();

        //    Check(args);

        //    EagleResult result = new EagleResult();

        //    if (_ima == null)
        //    {
        //        _ima = new iMAppClass();
        //        _ima.setPgmid(80, 100);
        //        _ima.start();
        //    }

        //    if (_ima.ConnectStatus() == false)
        //    {
        //        _ima.stop();
        //        _ima = null;
        //        result.EagleStatus = EagleStatus.Fail;
        //        return result;
        //    }
        //    else
        //    {
        //    }

        //    _subscriber = _ima.NewSubscriber(Subject, Key);
        //    _ima.OnMessageArrived += new IiMAppEvents_OnMessageArrivedEventHandler(imaA_OnMessageArrived);
        //    _ima.OnStatus += new IiMAppEvents_OnStatusEventHandler(imaA_OnStatus);

        //    return result;
        //}

        //private void imaA_OnMessageArrived(int number, Subscriber pMSubscriberX, MTreeX pMTreeX)
        //{
        //    EagleArgs args = new EagleArgs();
        //    args.OnMessageArrivedNumber = number;

        //    OnMessageArrived box = new OnMessageArrived(pMTreeX, pMSubscriberX);
        //    args.OnMessageArrived = box;

        //    if (_subscriber.getNumber() == number)
        //    {
        //        OnMessageArrived(args);
        //    }
        //}

        private void imaA_OnStatus(int msg, int Param1)
        {
            EagleArgs args = new EagleArgs();
            args.OnStatusMsg = msg;
            args.OnStatusParam1 = Param1;

            OnStatus(args);
        }

        public void AddBatchContent(List<EagleContent> listEagleContent)
        {
            _listEagleContent.Add(listEagleContent);
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