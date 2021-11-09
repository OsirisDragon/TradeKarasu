using System.Collections.Generic;

namespace Eagle
{
    public interface IEagleGate
    {
        string Subject { get; set; }

        string Key { get; set; }

        EagleResult Send();

        EagleResult SendAndReceiveData(string subscribeSubject, string subscribeKey, int waitSecond);

        void AddArgument(EagleArgs args);

        List<List<EagleContent>> GetListEagleContent();
    }
}