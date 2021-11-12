using Newtonsoft.Json;
using System.Collections.Generic;

namespace TradeUtility
{
    public class JsonHelper
    {
        /// <summary>
        /// 將Dictioinart轉成Json字串後，把裡面的雙引號變成三個雙引號，這樣在傳給exe或cmd的時候才會保留雙引號
        /// </summary>
        public static string ToJsonStringWithQuoteForExeArguments(Dictionary<string, string> dictionaryData)
        {
            return JsonConvert.SerializeObject(dictionaryData, Newtonsoft.Json.Formatting.None).Replace("\"", "\"\"\"");
        }
    }
}