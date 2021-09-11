using CrossModel;
using System.Collections.Generic;

namespace TradeFutNight.Common
{
    public class DropDownItems
    {
        public static IList<ItemInfo> tppIndexGrp()
        {
            var result = new List<ItemInfo>();
            result.Add(new ItemInfo() { Text = "其他", Value = (byte)0 });
            result.Add(new ItemInfo() { Text = "上市/櫃", Value = (byte)1 });
            result.Add(new ItemInfo() { Text = "日本", Value = (byte)3 });
            result.Add(new ItemInfo() { Text = "美國", Value = (byte)5 });
            result.Add(new ItemInfo() { Text = "英國", Value = (byte)6 });
            return result;
        }
    }
}