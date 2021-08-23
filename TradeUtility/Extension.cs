using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace TradeUtility
{
    public static class Extension
    {
        public static void ToCsv<T>(this IEnumerable<T> items, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.GetEncoding(950)))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture);

                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(items);
                }
            }
        }
    }
}