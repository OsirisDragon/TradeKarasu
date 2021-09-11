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
    }
}