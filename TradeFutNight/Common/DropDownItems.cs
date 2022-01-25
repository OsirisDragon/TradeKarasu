using CrossModel;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;

namespace TradeFutNight.Common
{
    public class DropDownItems
    {
        public static IList<ItemInfo> ProdExpireCode()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "到期", Value = 'Y' });
            result.Add(new ItemInfo() { Text = "未到期", Value = 'N' });
            return result;
        }

        public static IList<ItemInfo> TppIndexGrp()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "其他", Value = (byte)0 });
            result.Add(new ItemInfo() { Text = "上市/櫃", Value = (byte)1 });
            result.Add(new ItemInfo() { Text = "日本", Value = (byte)3 });
            result.Add(new ItemInfo() { Text = "美國", Value = (byte)5 });
            result.Add(new ItemInfo() { Text = "英國", Value = (byte)6 });
            result.Add(new ItemInfo() { Text = "原油", Value = (byte)101 });
            result.Add(new ItemInfo() { Text = "黃金", Value = (byte)102 });
            return result;
        }

        public static IList<ItemInfo> TppType()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "歷史波動", Value = "1" });
            result.Add(new ItemInfo() { Text = "瞬時波動", Value = "2" });
            result.Add(new ItemInfo() { Text = "CBOE VIX", Value = "4" });
            result.Add(new ItemInfo() { Text = "日經VIX", Value = "JNIV" });
            result.Add(new ItemInfo() { Text = "JPX東證期貨漲跌幅", Value = "TOPX" });
            result.Add(new ItemInfo() { Text = "CME E-mini S&P500期貨漲跌幅", Value = "SPX" });
            result.Add(new ItemInfo() { Text = "ICE FTSE 100指數期貨漲跌幅", Value = "FTSE" });
            result.Add(new ItemInfo() { Text = "CME 黃金期貨漲跌幅", Value = "FIXAM" });
            result.Add(new ItemInfo() { Text = "ICE 布蘭特原油期貨漲跌幅", Value = "IPCI" });
            return result;
        }

        public static IList<ItemInfo> PriceFlucItem()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "百分比", Value = 'P' });
            result.Add(new ItemInfo() { Text = "固定點數", Value = 'F' });
            return result;
        }

        public static IList<ItemInfo> PdkParamKeysCanQuote()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dPdk = new D_PDK(das);
                return dPdk.ListDistinctParamKeyCanQuote().Select(
                    c => new ItemInfo() { Text = c.PDK_PARAM_KEY, Value = c.PDK_PARAM_KEY }).ToList();
            }
        }

        public static IList<ItemInfo> MordKindIdType()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "非股票類", Value = 'K' });
            result.Add(new ItemInfo() { Text = "股票關聯KEY值", Value = 'P' });
            return result;
        }

        public static IList<ItemInfo> TppintdFirstKindId()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dTppintd = new D_TPPINTD(das);
                var result = dTppintd.ListFirstKindID().Select(
                    c => new ItemInfo() { Text = c.TPPINTD_FIRST_KIND_ID, Value = c.TPPINTD_FIRST_KIND_ID }).ToList();
                result.Insert(0, new ItemInfo() { Text = "全部", Value = "%" });
                return result;
            }
        }

        public static IList<ItemInfo> TppintdFirstKindIdTwoChar()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dTppintd = new D_TPPINTD(das);
                var result = dTppintd.ListFirstKindIDTwoChar().Select(
                    c => new ItemInfo() { Text = c.ToString(), Value = c.ToString() }).ToList();
                return result;
            }
        }

        public static IList<ItemInfo> TppintdSecondKindId()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dTppintd = new D_TPPINTD(das);
                var result = dTppintd.ListSecondKindID().Select(
                    c => new ItemInfo() { Text = c.TPPINTD_SECOND_KIND_ID, Value = c.TPPINTD_SECOND_KIND_ID }).ToList();
                result.Insert(0, new ItemInfo() { Text = "全部", Value = "%" });
                return result;
            }
        }

        public static IList<ItemInfo> PhaltType()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "P：契約", Value = 'P' });
            result.Add(new ItemInfo() { Text = "I：商品", Value = 'I' });
            return result;
        }

        public static IList<ItemInfo> PhaltMsgType()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "1：期貨資訊異常", Value = '1' });
            result.Add(new ItemInfo() { Text = "2：標的市場暫停", Value = '2' });
            result.Add(new ItemInfo() { Text = "3：個股訊息面暫停", Value = '3' });
            result.Add(new ItemInfo() { Text = "4：盤後契約尚未完成", Value = '4' });
            return result;
        }

        public static IList<ItemInfo> TpphaltMsgType()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "暫停動態退單", Value = '1' });
            result.Add(new ItemInfo() { Text = "放寬退單倍數", Value = '2' });
            return result;
        }

        public static IList<ItemInfo> TpphaltType()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "契約", Value = 'P' });
            result.Add(new ItemInfo() { Text = "單式商品", Value = 'I' });
            result.Add(new ItemInfo() { Text = "複式商品", Value = 'S' });
            return result;
        }

        public static IList<ItemInfo> TpphaltRange()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "上下限", Value = '0' });
            result.Add(new ItemInfo() { Text = "上限", Value = '1' });
            result.Add(new ItemInfo() { Text = "下限", Value = '2' });
            return result;
        }

        public static IList<ItemInfo> DayOfWeek()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "星期日", Value = '0' });
            result.Add(new ItemInfo() { Text = "星期一", Value = '1' });
            result.Add(new ItemInfo() { Text = "星期二", Value = '2' });
            result.Add(new ItemInfo() { Text = "星期三", Value = '3' });
            result.Add(new ItemInfo() { Text = "星期四", Value = '4' });
            result.Add(new ItemInfo() { Text = "星期五", Value = '5' });
            result.Add(new ItemInfo() { Text = "星期六", Value = '6' });
            return result;
        }

        public static IList<ItemInfo> SubtypeCode()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "指數", Value = 'I' });
            result.Add(new ItemInfo() { Text = "股票", Value = 'S' });
            result.Add(new ItemInfo() { Text = "商品", Value = 'C' });
            result.Add(new ItemInfo() { Text = "公債", Value = 'B' });
            result.Add(new ItemInfo() { Text = "利率", Value = 'R' });
            result.Add(new ItemInfo() { Text = "滙率", Value = 'E' });
            return result;
        }

        public static IList<ItemInfo> ZtypepProdType()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "FUT期貨", Value = "FUT" });
            result.Add(new ItemInfo() { Text = "PHY現貨", Value = "PHY" });
            result.Add(new ItemInfo() { Text = "OOF期權", Value = "OOF" });
            result.Add(new ItemInfo() { Text = "OOP現權", Value = "OOP" });
            return result;
        }

        public static IList<ItemInfo> ZtypepPriceQuote()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "IDX指數", Value = "IDX" });
            result.Add(new ItemInfo() { Text = "STD非指數", Value = "STD" });
            result.Add(new ItemInfo() { Text = "INT利率指數", Value = "INT" });
            return result;
        }

        public static IList<ItemInfo> ZtypepSettlement()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "CASH現金", Value = "CASH" });
            result.Add(new ItemInfo() { Text = "DELIV實物", Value = "DELIV" });
            return result;
        }

        public static IList<ItemInfo> FrpProdId()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "CME小標普500期貨最近月每日結算價", Value = "SPX_S" });
            result.Add(new ItemInfo() { Text = "CBOE VIX指數收盤價", Value = "VIX" });
            result.Add(new ItemInfo() { Text = "ICE富時100期貨最近月每日結算價", Value = "FTSE_S" });
            result.Add(new ItemInfo() { Text = "ICE布蘭特原油期貨最近到期結算價", Value = "BRF_S" });
            result.Add(new ItemInfo() { Text = "CME黃金期貨最近到期結算價", Value = "GDF_S" });
            result.Add(new ItemInfo() { Text = "SGX富臺指數期貨日盤最後一筆成交價", Value = "STWNamc1" });
            return result;
        }

        public static IList<ItemInfo> OswGrpAH()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "第一盤", Value = 10 });
            result.Add(new ItemInfo() { Text = "第二盤", Value = 11 });
            return result;
        }

        public static IList<ItemInfo> MarketClose()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "0_全部時段收盤", Value = 0 });
            return result;
        }

        public static IList<ItemInfo> TxnType()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "新增", Value = "I" });
            result.Add(new ItemInfo() { Text = "查詢,刪除", Value = "D" });
            result.Add(new ItemInfo() { Text = "變更", Value = "U" });
            result.Add(new ItemInfo() { Text = "維護", Value = "A" });
            result.Add(new ItemInfo() { Text = "其他", Value = "O" });
            result.Add(new ItemInfo() { Text = "PROC", Value = "S" });
            result.Add(new ItemInfo() { Text = "報表", Value = "R" });
            return result;
        }

        public static IList<ItemInfo> TxnDefault()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "Y:預設", Value = "Y" });
            result.Add(new ItemInfo() { Text = "", Value = " " });
            return result;
        }
    }
}