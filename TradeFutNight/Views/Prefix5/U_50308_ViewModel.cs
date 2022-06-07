using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Sp;
using TradeUtility;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50308_ViewModel : ViewModelParent<UIModel_50308>
    {
        public XtraReport Report
        {
            get { return GetProperty(() => Report); }
            set { SetProperty(() => Report, value); }
        }

        public U_50308_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50308>();
        }

        public void Open()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dSp = new D_StoredProcedure<UIModel_50308>(das);
                MainGridData = dSp.proc_everyday_tal().ToList();
            }
        }
    }

    [HighlightedClass]
    public class UIModel_50308 : DTO_SP_proc_everyday_tal
    {
    }
}