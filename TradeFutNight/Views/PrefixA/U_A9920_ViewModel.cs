using AutoMapper;
using ChangeTracking;
using CrossModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Specific.PrefixA;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixA
{
    public class U_A9920_ViewModel : ViewModelParent<UIModel_A9920>
    {
        public bool IsReadOnlyTPPADJ_M_PRICE_LIMIT
        {
            get { return GetProperty(() => IsReadOnlyTPPADJ_M_PRICE_LIMIT); }
            set { SetProperty(() => IsReadOnlyTPPADJ_M_PRICE_LIMIT, value); }
        }

        public bool IsReadOnlyTPPADJ_M_PRICE_LIMIT_F
        {
            get { return GetProperty(() => IsReadOnlyTPPADJ_M_PRICE_LIMIT_F); }
            set { SetProperty(() => IsReadOnlyTPPADJ_M_PRICE_LIMIT_F, value); }
        }

        public bool IsReadOnlyTPPADJ_M_INTERVAL
        {
            get { return GetProperty(() => IsReadOnlyTPPADJ_M_INTERVAL); }
            set { SetProperty(() => IsReadOnlyTPPADJ_M_INTERVAL, value); }
        }

        public bool IsReadOnlyTPPADJ_ACCU_QNTY
        {
            get { return GetProperty(() => IsReadOnlyTPPADJ_ACCU_QNTY); }
            set { SetProperty(() => IsReadOnlyTPPADJ_ACCU_QNTY, value); }
        }

        public bool IsReadOnlyTPPADJ_M_PRICE_FILTER
        {
            get { return GetProperty(() => IsReadOnlyTPPADJ_M_PRICE_FILTER); }
            set { SetProperty(() => IsReadOnlyTPPADJ_M_PRICE_FILTER, value); }
        }

        public bool IsReadOnlyTPPADJ_BS_PRICE_FILTER
        {
            get { return GetProperty(() => IsReadOnlyTPPADJ_BS_PRICE_FILTER); }
            set { SetProperty(() => IsReadOnlyTPPADJ_BS_PRICE_FILTER, value); }
        }

        public bool IsReadOnlyTPPADJ_THERICAL_P_REF
        {
            get { return GetProperty(() => IsReadOnlyTPPADJ_THERICAL_P_REF); }
            set { SetProperty(() => IsReadOnlyTPPADJ_THERICAL_P_REF, value); }
        }

        public bool IsReadOnlyTPPADJ_SPREAD
        {
            get { return GetProperty(() => IsReadOnlyTPPADJ_SPREAD); }
            set { SetProperty(() => IsReadOnlyTPPADJ_SPREAD, value); }
        }

        public U_A9920_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_A9920>();

            IsReadOnlyTPPADJ_M_PRICE_LIMIT = true;
            IsReadOnlyTPPADJ_M_PRICE_LIMIT_F = true;
            IsReadOnlyTPPADJ_M_INTERVAL = true;
            IsReadOnlyTPPADJ_ACCU_QNTY = true;
            IsReadOnlyTPPADJ_M_PRICE_FILTER = true;
            IsReadOnlyTPPADJ_BS_PRICE_FILTER = true;
            IsReadOnlyTPPADJ_THERICAL_P_REF = true;
            IsReadOnlyTPPADJ_SPREAD = true;
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPADJ, UIModel_A9920>().ReverseMap();
            }));

            var result = new List<UIModel_A9920>();

            using (var das = Factory.CreateDalSession())
            {
                var tppintd = new D_TPPINTD(das).ListSingleKind();
                var tppst = new D_TPPST(das).ListSingleMonth();
                var prodPdkTppbp = new D_A9920<UIModel_A9920>(das).ListProdPdkTppbp();

                foreach (var item in prodPdkTppbp)
                {
                    int count = 0;

                    // 如果是非股票類，有在TPPINTD裡面才要顯示
                    if (item.PDK_SUBTYPE != "S")
                    {
                        count = tppintd.Count(c => c.TPPINTD_FIRST_KIND_ID == item.PDK_KIND_ID);
                        if (count <= 0) continue;
                    }
                    // 如果是股票類，有在TPPST裡面才要顯示
                    else if (item.PDK_SUBTYPE == "S")
                    {
                        count = tppst.Count(c => c.TPPST_KIND_ID == item.PDK_KIND_ID.Substring(0, 2));
                        if (count <= 0) continue;
                    }

                    var newRow = new UIModel_A9920();
                    newRow.TPPADJ_TRADE_DATE = MagicalHats.Ocf.OCF_DATE;
                    newRow.TPPADJ_TYPE = 'I';
                    newRow.TPPADJ_PROD_ID = item.PROD_ID;
                    newRow.TPPADJ_SETTLE_DATE = item.PROD_SETTLE_DATE;
                    newRow.TPPADJ_THERICAL_P_REF = item.TPPBP_THERICAL_P_REF;
                    newRow.PDK_KIND_ID = item.PDK_KIND_ID;
                    newRow.PDK_SUBTYPE = item.PDK_SUBTYPE;
                    newRow.PDK_STOCK_ID = item.PDK_STOCK_ID;
                    result.Add(newRow);
                }

                int everyKindIdMonthIndex = 0;
                string preKindId = "";
                IList<TPPST> tppstFilter = new List<TPPST>();
                IList<TPPINTD> tppintdFilter = new List<TPPINTD>();

                // 填入TPPINTD或TPPST相關資料，照著月份序號順序填入
                foreach (var item in result)
                {
                    // 跟前一筆的商品不一樣
                    if (item.PDK_KIND_ID != preKindId)
                    {
                        everyKindIdMonthIndex = 0;

                        // 每個商品的第一個月份就是近月
                        item.IS_NEAR_MONTH = true;

                        // 過濾資料
                        if (item.PDK_SUBTYPE == "S")
                        {
                            tppstFilter = tppst.Where(c => c.TPPST_KIND_ID == item.PDK_KIND_ID.Substring(0, 2)).ToList();
                        }
                        else
                        {
                            tppintdFilter = tppintd.Where(c => c.TPPINTD_FIRST_KIND_ID == item.PDK_KIND_ID).ToList();
                        }
                    }

                    if (item.PDK_SUBTYPE == "S")
                    {
                        if (everyKindIdMonthIndex < tppstFilter.Count())
                        {
                            item.TPPADJ_M_PRICE_LIMIT = tppstFilter[everyKindIdMonthIndex].TPPST_M_PRICE_LIMIT;
                            item.TPPADJ_M_PRICE_LIMIT_F = tppstFilter[everyKindIdMonthIndex].TPPST_M_PRICE_LIMIT_F;
                            item.TPPADJ_M_INTERVAL = tppstFilter[everyKindIdMonthIndex].TPPST_M_INTERVAL;
                            item.TPPADJ_ACCU_QNTY = tppstFilter[everyKindIdMonthIndex].TPPST_ACCU_QNTY;
                            item.TPPADJ_M_PRICE_FILTER = tppstFilter[everyKindIdMonthIndex].TPPST_M_PRICE_FILTER;
                            item.TPPADJ_BS_PRICE_FILTER = tppstFilter[everyKindIdMonthIndex].TPPST_BS_PRICE_FILTER;
                        }
                    }
                    else
                    {
                        if (everyKindIdMonthIndex < tppintdFilter.Count())
                        {
                            item.TPPADJ_M_PRICE_LIMIT = tppintdFilter[everyKindIdMonthIndex].TPPINTD_M_PRICE_LIMIT;
                            item.TPPADJ_M_PRICE_LIMIT_F = tppintdFilter[everyKindIdMonthIndex].TPPINTD_M_PRICE_LIMIT_F;
                            item.TPPADJ_M_INTERVAL = tppintdFilter[everyKindIdMonthIndex].TPPINTD_M_INTERVAL;
                            item.TPPADJ_ACCU_QNTY = tppintdFilter[everyKindIdMonthIndex].TPPINTD_ACCU_QNTY;
                            item.TPPADJ_M_PRICE_FILTER = tppintdFilter[everyKindIdMonthIndex].TPPINTD_M_PRICE_FILTER;
                            item.TPPADJ_BS_PRICE_FILTER = tppintdFilter[everyKindIdMonthIndex].TPPINTD_BS_PRICE_FILTER;
                        }
                    }

                    everyKindIdMonthIndex++;
                    preKindId = item.PDK_KIND_ID;
                }
            }

            MainGridData = result.AsTrackable();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_A9920());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_A9920)item);
        }

        public async Task Query(string kindId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var dSlt = new D_SLT(das);
                    MainGridData = MapperInstance.Map<IList<UIModel_A9920>>(dSlt.ListByKindID(kindId)).AsTrackable();
                }
            });

            await task;
        }
    }

    public class UIModel_A9920 : TPPADJ
    {
        public virtual bool IsChecked { get; set; }
        public virtual string PROD_ID { get; set; }
        public virtual string PROD_SETTLE_DATE { get; set; }
        public virtual string PDK_KIND_ID { get; set; }
        public virtual string PDK_SUBTYPE { get; set; }
        public virtual string PDK_STOCK_ID { get; set; }
        public virtual decimal? TPPBP_THERICAL_P_REF { get; set; }
        public virtual bool IS_NEAR_MONTH { get; set; }
    }
}