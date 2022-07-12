using System.Threading.Tasks;

namespace CrossModel.Interfaces
{
    public interface IViewSword
    {
        Task<bool> IsCanRun();

        void ToolButtonSetting();

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