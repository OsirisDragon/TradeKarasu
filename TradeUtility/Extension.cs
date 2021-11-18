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

        public static string ToDateStr(this DateTime item)
        {
            return item.ToString("yyyy/MM/dd");
        }

        public static string ToDateTimeStr(this DateTime item)
        {
            return item.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public static DateTime AsDateTime(this object item, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;
            if (!DateTime.TryParse(item.ToString(), out result))
                return defaultDateTime;

            return result;
        }

        public static DateTime AsDateTime(this object item, string format, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;
            if (!DateTime.TryParseExact(item.ToString(), format, null, DateTimeStyles.AllowWhiteSpaces, out result))
                return defaultDateTime;
            return result;
        }

        public static int AsInt(this object item, int defaultInt = default(int))
        {
            if (item == null)
                return defaultInt;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static double AsDouble(this object item, double defaultDouble = default(double))
        {
            if (item == null)
                return defaultDouble;

            double result;
            if (!double.TryParse(item.ToString(), out result))
                return defaultDouble;

            return result;
        }

        public static decimal AsDecimal(this object item, decimal defaultDecimal = default(decimal))
        {
            if (item == null)
                return defaultDecimal;

            decimal result;
            if (!decimal.TryParse(item.ToString(), out result))
                return defaultDecimal;

            return result;
        }

        public static string AsString(this object item, string defaultString = default(string))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return defaultString;

            return item.ToString().Trim();
        }

        public static bool AsBool(this object item, bool defaultBool = default(bool))
        {
            if (item == null)
                return defaultBool;

            return new List<string>() { "yes", "y", "true" }.Contains(item.ToString().ToLower());
        }
    }
}