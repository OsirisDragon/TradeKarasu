using CrossModel.Enum;
using CsvHelper;
using CsvHelper.Configuration;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace TradeUtility
{
    public static class Extension
    {
        public static void ToCsv<T>(this IEnumerable<T> items, string filePath, bool hasHeader)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.GetEncoding(950)))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture);
                config.HasHeaderRecord = hasHeader;

                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(items);
                }
            }
        }

        public static void ToExcel(this DataTable dt, string filePath, FileType fileType, bool hasHeader)
        {
            try
            {
                Workbook wb = new Workbook();
                wb.Options.Export.Csv.WritePreamble = true;//預設的Csv輸出中文會是亂碼
                wb.Worksheets[0].Import(dt, hasHeader, 0, 0);//從title以下開始輸出
                DocumentFormat format;
                switch (fileType)
                {
                    case FileType.Txt:
                        format = DocumentFormat.Text;
                        break;

                    case FileType.Xlsx:
                        format = DocumentFormat.Xlsx;
                        break;

                    case FileType.Xls:
                        format = DocumentFormat.Xls;
                        break;

                    default:
                    case FileType.Csv:
                        format = DocumentFormat.Csv;
                        break;
                }

                wb.SaveDocument(filePath, format);
            }
            catch (Exception ex)
            {
                File.Delete(filePath);
                throw ex;
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

        public static string Left(this string str, int length)
        {
            str = (str ?? string.Empty);
            return str.Substring(0, Math.Min(length, str.Length));
        }

        public static string Right(this string str, int length)
        {
            str = (str ?? string.Empty);
            return (str.Length >= length)
                ? str.Substring(str.Length - length, length)
                : str;
        }
    }
}