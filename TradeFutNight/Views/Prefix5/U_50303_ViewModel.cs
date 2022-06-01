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
    public class U_50303_ViewModel : ViewModelParent<UIModel_50303>
    {
        public XtraReport Report
        {
            get { return GetProperty(() => Report); }
            set { SetProperty(() => Report, value); }
        }

        public U_50303_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_50303>();
        }

        public void Open()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dSp = new D_StoredProcedure<UIModel_50303>(das);
                var spData = dSp.proc_mtf_detail().ToList();
                var pdkData = new D_PDK(das).ListDataByOSWCUR();
                MainGridData = spData.Join(pdkData,
                    sp => sp.PROD_ID_OUT,
                    pdk => pdk.PDK_KIND_ID,
                    (sp, pdk) => new UIModel_50303
                    {
                        PROD_ID_OUT = sp.PROD_ID_OUT,
                        PROD_SETTLE_DATE = sp.PROD_SETTLE_DATE,
                        MTF_FCM_NO = sp.MTF_FCM_NO,
                        BRK_NAME = sp.BRK_NAME,
                        MTF_BUY_QNTY = sp.MTF_BUY_QNTY,
                        MTF_PRICE = sp.MTF_PRICE,
                        MTF_SELL_QNTY = sp.MTF_SELL_QNTY,
                        PDK_SUBTYPE = pdk.PDK_SUBTYPE.AsString()
                    }).ToList();
            }
        }
    }

    [HighlightedClass]
    public class UIModel_50303 : DTO_SP_proc_mtf_detail
    {
        public string PDK_SUBTYPE { get; set; }
    }
}