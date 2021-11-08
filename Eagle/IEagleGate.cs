using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Eagle.MexGate;

namespace Eagle
{
    public interface IEagleGate
    {
        string Subject { get; set; }

        string Key { get; set; }

        EagleResult Send();

        EagleResult SendAndReceiveData(string subscribeSubject, string subscribeKey, int waitSecond);

        //EagleResult SendMultiple();

        void AddArgument(EagleArgs args);

        List<List<EagleContent>> GetListEagleContent();

        //void GenerateBatchJson(EagleArgs args);

        //EagleResult Receive(EagleArgs args);

        //event OnMessageArrivedEventHandler OnMessageArrived;

        //event OnStatusdEventHandler OnStatus;
    }
}