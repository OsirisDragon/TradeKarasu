using CrossModel;
using System.Collections.Generic;
using System.Linq;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;

namespace TradeFutNight.Common
{
    public class DropDownItems
    {
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
    }
}