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

        Task<EagleResult> Send(EagleArgs args);

        EagleResult SendByExternal(EagleArgs args);

        EagleResult Receive(EagleArgs args);

        void Stop();

        event OnMessageArrivedEventHandler OnMessageArrived;

        event OnStatusdEventHandler OnStatus;
    }
}