using CrossModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeFutNight.Interfaces
{
    public interface IViewSword
    {
        Task<bool> IsCanRun();

        void ControlSetting();

        Task Open();

        void Insert();

        void Delete();

        Task<bool> CheckField();

        Task Save();

        Task Export();

        Task Print();

        Task PrintIndex();

        Task PrintStock();
    }
}