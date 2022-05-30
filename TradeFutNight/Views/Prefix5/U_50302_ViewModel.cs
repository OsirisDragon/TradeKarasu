using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix5;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50302_ViewModel : ViewModelParent<UIModel_50302>
    {
        public XtraReport Report
        {
            get { return GetProperty(() => Report); }
            set { SetProperty(() => Report, value); }
        }

        public U_50302_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50302>();
        }

        public void Open()
        {
            using (var das = Factory.CreateDalSession())
            {
                var d50302 = new D_50302<UIModel_50302>(das);
                MainGridData = d50302.ListData().ToList();
            }
        }
    }

    [HighlightedClass]
    public class UIModel_50302 : FMIFSTG
    {
        public virtual string FIRST_SETTLE_DATE { get; set; }
        public virtual string SECOND_SETTLE_DATE { get; set; }

        public virtual string PDK_SUBTYPE { get; set; }

        public virtual string FMIFSTG_PROD_ID_3 { get; set; }
    }
}