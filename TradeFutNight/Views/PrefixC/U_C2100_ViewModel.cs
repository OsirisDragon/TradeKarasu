using AutoMapper;
using ChangeTracking;
using CrossModel;
using Shield.Mapping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Specific.PrefixC;
using TradeFutNightData.Gates.Tfxm;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.PrefixC
{
    public class U_C2100_ViewModel : ViewModelParent<UIModel_C2100>
    {
        public string PdkKindId
        {
            get { return GetProperty(() => PdkKindId); }
            set { SetProperty(() => PdkKindId, value); }
        }

        public IList<ItemInfo> IsAdjustNextDate
        {
            get { return GetProperty(() => IsAdjustNextDate); }
            set { SetProperty(() => IsAdjustNextDate, value); }
        }

        public U_C2100_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_C2100>();
        }

        public void Open()
        {
            MapperInstance = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPPBP, UIModel_C2100>().ReverseMap();
            }));

            IsAdjustNextDate = IsAdjustNextDateInfo();

            IList<UIModel_C2100> data = null;
            IList<UIModel_C2100_SDI> listCadjAndSdi = null;

            using (var das = Factory.CreateDalSession())
            {
                var dC2100 = new D_C2100<UIModel_C2100>(das);
                data = dC2100.ListAllData(MagicalHats.Ocf.OCF_PREV_DATE.Value).ToList();

                var dTPPST = new D_TPPST(das);
                var dPHALT = new D_PHALT(das);

                #region 只有設定在TPPST裡面的商品才要出現

                foreach (var item in data)
                {
                    var count = dTPPST.ListByKindId(item.PDK_KIND_ID.Substring(0, 2)).Count;
                    if (count <= 0)
                    {
                        data.Remove(item);
                    }
                }

                #endregion 只有設定在TPPST裡面的商品才要出現

                #region #region 如果是當天開盤前的訊息面暫停，TPPBP裡面會有這筆，但是基準價是空的，

                //但在PHALT裡面有紀錄這種的開盤基準價就要抓PROD_TPPBP_THERICAL_P，代表著暫停前的基準價

                foreach (var item in data)
                {
                    var count = dPHALT.ListByDateAndProd(MagicalHats.Ocf.OCF_DATE, MagicalHats.Ocf.OCF_PREV_DATE.Value, item.PDK_KIND_ID).Count;
                    if (count > 0)
                    {
                        item.TPPBP_THERICAL_P = item.PROD_TPPBP_THERICAL_P;
                        item.TPPBP_THERICAL_P_REF = item.PROD_TPPBP_THERICAL_P;
                        item.IS_NEW_PRODUCT_OR_MONTH = 1;
                    }
                }

                #endregion #region 如果是當天開盤前的訊息面暫停，TPPBP裡面會有這筆，但是基準價是空的，

                var dC2100Sdi = new D_C2100<UIModel_C2100_SDI>(das);
                // 先取有沒有契約調整的資料
                listCadjAndSdi = dC2100Sdi.ListCadjAndSdi(MagicalHats.Ocf.OCF_DATE).ToList();
            }

            #region 預估次日現貨開盤參考價，為日盤現貨收盤價

            using (var das = Factory.CreateDalSession(SettingDatabaseInfo.TfxmDay))
            {
                var dRSFD = new D_RSFD(das);

                foreach (var item in data)
                {
                    string pdkParamKey = item.PDK_PARAM_KEY;
                    string stockId = item.PDK_STOCK_ID;
                    decimal targetPrice = 0;

                    // 因為這裡是夜盤，營業日會比日盤多一天，所以要用前一營業日也就是等於日盤的今日營業日
                    var listRSFD = dRSFD.ListByStockIdAndDate(MagicalHats.Ocf.OCF_PREV_DATE.Value, stockId).SingleOrDefault();
                    targetPrice = listRSFD != null ? listRSFD.RSFD_CLOSE_PRICE.Value : 0;
                    var count = listCadjAndSdi.Where(c => c.SDI_STOCK_ID == stockId).Count();
                    var foundRow = listCadjAndSdi.Where(c => c.SDI_STOCK_ID == stockId).SingleOrDefault();

                    if (count > 0)
                    {
                        // 如果有遇次日除息，則要扣除息值
                        var calVal = GetCalculateSdiRelated(pdkParamKey, targetPrice, foundRow.SDI_DIVIDEND_CASH, foundRow.SDI_DISINVEST_RATE, foundRow.SDI_INCREASE_PRICE, foundRow.SDI_COMB_RATE, foundRow.SDI_DIVIDEND_SHARE);
                        if (calVal != 0)
                        {
                            targetPrice = calVal;
                        }
                    }

                    item.TARGET_PRICE = targetPrice;
                }
            }

            #endregion 預估次日現貨開盤參考價，為日盤現貨收盤價

            #region 夜盤期貨開盤基準價

            // 原則為本日日盤期貨最後一筆基準價
            // 再看看次日有沒有契約調整來作計算處理

            // 這段處理的是次日契約調整，非加掛標準型的
            // 針對減資、現增、股份轉換為既存公司之子公司、股份轉換為新設公司之子公司、除權或除息或(除權+除息)
            foreach (var item in data)
            {
                string pdkParamKey = item.PDK_PARAM_KEY;
                if (item.TPPBP_THERICAL_P == null) { continue; }
                var foundRow = listCadjAndSdi.Where(c => c.CADJ_AF_KIND_ID == item.PDK_KIND_ID).SingleOrDefault();
                if (foundRow != null)
                {
                    item.IS_ADJUST_NEXT_DATE = 1;

                    // 計算減資、現增、除權、除息、股份轉換等相關
                    var calVal = GetCalculateSdiRelated(pdkParamKey, item.TPPBP_THERICAL_P.Value, foundRow.SDI_DIVIDEND_CASH, foundRow.SDI_DISINVEST_RATE, foundRow.SDI_INCREASE_PRICE, foundRow.SDI_COMB_RATE, foundRow.SDI_DIVIDEND_SHARE);
                    if (calVal != 0)
                    {
                        item.TPPBP_THERICAL_P_REF = calVal;
                    }
                }
            }

            #endregion 夜盤期貨開盤基準價

            #region 契約提前下市規則，所有月份次日期貨開盤基準價=最近1個可交易月份本日期貨最後一筆基準價

            // 先照商品和月份排序，因為要抓近月的資料要先排序
            data = data.OrderBy(c => c.PDK_KIND_ID).ThenBy(c => c.PROD_SETTLE_DATE).ToList();
            using (var das = Factory.CreateDalSession())
            {
                var dCVAR = new D_CVAR(das);
                var listCvarForEnd = dCVAR.ListByDate(MagicalHats.Ocf.OCF_DATE);
                foreach (var itemCvar in listCvarForEnd)
                {
                    string cvarKindId = itemCvar.CVAR_KIND_ID;
                    int cvarKindIdLen = cvarKindId.Length;
                    var foundRow = data.Where(c => c.TPPBP_PROD_ID.Substring(0, cvarKindIdLen) == cvarKindId).SingleOrDefault();
                    if (foundRow != null)
                    {
                        decimal nearMonthTppbpThericalP = foundRow.TPPBP_THERICAL_P.Value;
                        string nearMonthPdkKindId = foundRow.PDK_KIND_ID;
                        foreach (var item in data)
                        {
                            if (nearMonthPdkKindId == item.PDK_KIND_ID)
                            {
                                item.TPPBP_THERICAL_P_REF = nearMonthTppbpThericalP;
                            }
                        }
                    }
                }
            }

            #endregion 契約提前下市規則，所有月份次日期貨開盤基準價=最近1個可交易月份本日期貨最後一筆基準價

            data = data.OrderBy(c => c.PDK_ON_CODE).ThenBy(c => c.PDK_STOCK_ID).ThenBy(c => c.PDK_KIND_ID).ThenBy(c => c.PROD_SETTLE_DATE).ToList();

            MainGridData = data.AsTrackable();
        }

        public void Insert()
        {
            MainGridData.Insert(0, new UIModel_C2100());
        }

        public void Delete(object item)
        {
            MainGridData.Remove((UIModel_C2100)item);
        }

        public IList<ItemInfo> IsAdjustNextDateInfo()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "●", Value = 1 });
            result.Add(new ItemInfo() { Text = "", Value = 0 });
            return result;
        }

        /// <summary>
        /// 計算SDI相關的資訊
        /// </summary>
        /// <param name="pdkParamKey"></param>
        /// <param name="inputValue"></param>
        /// <param name="sdiDividendCash"></param>
        /// <param name="sdiDisinvestRate"></param>
        /// <param name="sdiIncreasePrice"></param>
        /// <param name="sdiCombRate"></param>
        /// <param name="sdiDividendShare"></param>
        /// <returns></returns>
        public decimal GetCalculateSdiRelated(string pdkParamKey, decimal inputValue, decimal sdiDividendCash, decimal sdiDisinvestRate, decimal sdiIncreasePrice, decimal sdiCombRate, decimal sdiDividendShare)
        {
            decimal calVal = 0;
            // 減資
            if (sdiDisinvestRate != 0)
            {
                // (傳入值 - 每股退還股款) / 減資換股率
                calVal = (inputValue - sdiDividendCash) / sdiDisinvestRate;
            }
            // 股份轉換為既存公司之子公司
            // 股份轉換為新設公司之子公司
            else if (sdiCombRate != 0)
            {
                // (傳入值 - 每股配發現金) / 每股換股率
                calVal = (inputValue - sdiDividendCash) / sdiCombRate;
            }
            // 除權或除息或(除權+除息)
            else if (sdiDividendCash != 0 || sdiDividendShare != 0)
            {
                // (傳入值 - 每股配發現金) / (1+每股配發股數)
                calVal = (inputValue - sdiDividendCash) / (1 + sdiDividendShare);
            }
            // 其他狀況，像是現增之類，(現增認購價值於基準價程式處理)
            else
            {
                // 原本的值
                calVal = inputValue;
            }

            if (calVal != 0)
            {
                using (var das = Factory.CreateDalSession())
                {
                    var putUnit = new D_PUT(das).GetPutUnit(pdkParamKey, calVal);

                    // 取至最接近tick
                    if (!Utility.IsValidTick(calVal, putUnit))
                    {
                        if (putUnit != 0)
                        {
                            calVal = Math.Round(calVal / putUnit, 0) * putUnit;
                        }
                    }
                }
            }
            return calVal;
        }
    }

    public class UIModel_C2100 : TPPBP
    {
        public virtual string PDK_KIND_ID { get; set; }
        public virtual string PDK_NAME { get; set; }
        public virtual string PDK_STOCK_ID { get; set; }
        public virtual string PDK_PARAM_KEY { get; set; }
        public virtual string PDK_MARKET_CLOSE { get; set; }
        public virtual char PDK_ON_CODE { get; set; }
        public virtual decimal PDK_PROD_IDX { get; set; }
        public virtual string PROD_SETTLE_DATE { get; set; }
        public virtual DateTime PROD_END_DATE { get; set; }
        public virtual decimal PROD_TPPBP_THERICAL_P { get; set; }
        public virtual decimal PROD_RAISE_PRICE { get; set; }
        public virtual decimal PROD_FALL_PRICE { get; set; }
        public virtual decimal TARGET_PRICE { get; set; }
        public virtual decimal CHINASTOCK_FLUCTUATION { get; set; }

        public virtual int IS_ADJUST_NEXT_DATE { get; set; }
        public virtual int IS_NEW_PRODUCT_OR_MONTH { get; set; }
    }

    public class UIModel_C2100_SDI : SDI
    {
        public virtual string CADJ_AF_KIND_ID { get; set; }
    }
}