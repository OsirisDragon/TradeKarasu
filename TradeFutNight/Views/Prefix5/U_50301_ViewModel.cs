using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix5;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix5
{
    public class U_50301_ViewModel : ViewModelParent<UIModel_50301>
    {
        public XtraReport Report
        {
            get { return GetProperty(() => Report); }
            set { SetProperty(() => Report, value); }
        }

        public U_50301_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50301>();
        }

        public void Open()
        {
            using (var das = Factory.CreateDalSession())
            {
                var d50301 = new D_50301<UIModel_50301>(das);
                MainGridData = d50301.ListData().ToList();
            }
        }

        public IEnumerable<ExportModel_50301> ListForGenerateFile()
        {
            using (var das = Factory.CreateDalSession())
            {
                var d50301 = new D_50301<ExportModel_50301>(das);
                var data = d50301.ListForGenerateFile();
                return data;
            }
        }
    }

    [HighlightedClass]
    public class UIModel_50301 : FMIF
    {
        public virtual decimal CLSPRC_SETTLE_PRICE { get; set; }
        public virtual string PROD_SETTLE_DATE { get; set; }
        public virtual string PROD_PC_CODE { get; set; }
        public virtual decimal PROD_STRIKE_PRICE { get; set; }
        public virtual string PROD_ID_OUT { get; set; }
        public virtual string PDK_SUBTYPE { get; set; }
        public virtual decimal BMIF_M_QNTY_TAL { get; set; }
        public virtual string KIND_ID_FOR_TWO { get; set; }
        public virtual string PDK_STOCK_ID { get; set; }
        public virtual string PDK_NAME { get; set; }
    }

    public class ExportModel_50301
    {
        public virtual string PDK_KIND_ID { get; set; }

        public virtual string PDK_STOCK_ID { get; set; }

        public virtual string PROD_SETTLE_DATE { get; set; }

        public virtual decimal FMIF_SETTLE_PRICE { get; set; }
    }
}