using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle
{
    public class EagleResult
    {
        public EagleStatus EagleStatus = EagleStatus.Fail;
        public string ErrorMessage = "";
    }

    public enum EagleStatus
    {
        Success,
        Fail
    }
}
