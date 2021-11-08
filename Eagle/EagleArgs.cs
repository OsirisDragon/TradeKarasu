//using iMAppX;
using System.Collections.Generic;

namespace Eagle
{
    public class EagleArgs
    {
        private List<EagleContent> _contents = new List<EagleContent>();

        public string CurrentDirectory;
        public string Path;

        public int OnMessageArrivedNumber;
        //public OnMessageArrived OnMessageArrived;

        public int OnStatusMsg;
        public int OnStatusParam1;

        public List<EagleContent> ListEagleContent
        {
            get
            {
                return _contents;
            }

            set
            {
                _contents = value;
            }
        }

        public void AddEagleContent(EagleContent ec)
        {
            if (!_contents.Contains(ec))
            {
                _contents.Add(ec);
            }
        }
    }

    //public class OnMessageArrived
    //{
    //    public Subscriber OnMessageArrivedSubscriber;
    //    public MTreeX OnMessageArrivedMTreeX;

    //    public OnMessageArrived(MTreeX tree, Subscriber subscriber)
    //    {
    //        OnMessageArrivedMTreeX = tree;
    //        OnMessageArrivedSubscriber = subscriber;
    //    }

    //    public T GetValue<T>(string item)
    //    {
    //        object myResult = null;

    //        if (typeof(T) == typeof(int))
    //        {
    //            myResult = OnMessageArrivedMTreeX.get_int(item);
    //        }
    //        else if (typeof(T) == typeof(string))
    //        {
    //            myResult = OnMessageArrivedMTreeX.get_char(item).ToString().TrimEnd('\0');
    //        }
    //        else if (typeof(T) == typeof(double))
    //        {
    //            myResult = OnMessageArrivedMTreeX.get_double(item);
    //        }

    //        return (T)myResult;
    //    }
    //}
}