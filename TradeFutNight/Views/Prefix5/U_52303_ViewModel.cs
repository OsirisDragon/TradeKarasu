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
    public class U_52303_ViewModel : ViewModelParent<UIModel_52303>
    {
        public XtraReport Report
        {
            get { return GetProperty(() => Report); }
            set { SetProperty(() => Report, value); }
        }

        public U_52303_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_52303>();
        }

        public void Open()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dSp = new D_StoredProcedure<UIModel_52303>(das);
                var spData = dSp.proc_BTRD_mtf_detail().ToList();
                var pdkData = new D_PDK(das).ListDataByOSWCUR();
                MainGridData = spData.Join(pdkData,
                    sp => sp.PROD_ID_OUT,
                    pdk => pdk.PDK_KIND_ID,
                    (sp, pdk) => new UIModel_52303
                    {
                        PROD_ID_OUT = sp.PROD_ID_OUT,
                        PROD_SETTLE_DATE = sp.PROD_SETTLE_DATE,
                        BMTF_FCM_NO = sp.BMTF_FCM_NO,
                        BRK_NAME = sp.BRK_NAME,
                        BMTF_BUY_QNTY = sp.BMTF_BUY_QNTY,
                        BMTF_PRICE = sp.BMTF_PRICE,
                        BMTF_SELL_QNTY = sp.BMTF_SELL_QNTY,
                        PDK_SUBTYPE = pdk.PDK_SUBTYPE.AsString()
                    }).ToList();
            }
        }
    }

    [HighlightedClass]
    public class UIModel_52303 : DTO_SP_proc_BTRD_mtf_detail
    {
        public string PDK_SUBTYPE { get; set; }
    }
}