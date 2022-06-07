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
    public class U_50311_ViewModel : ViewModelParent<UIModel_50311>
    {
        public XtraReport Report
        {
            get { return GetProperty(() => Report); }
            set { SetProperty(() => Report, value); }
        }

        public U_50311_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50311>();
        }

        public void Open()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dSp = new D_StoredProcedure<UIModel_50311>(das);
                var spData = dSp.proc_day_fee().ToList();
                MainGridData = spData;
            }
        }
    }

    [HighlightedClass]
    public class UIModel_50311 : DTO_SP_proc_day_fee
    {
    }
}