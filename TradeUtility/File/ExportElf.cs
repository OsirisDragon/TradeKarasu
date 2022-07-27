using CrossModel.Attributes;
using CrossModel.Enum;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TradeUtility.File
{
    public static class ExportElf
    {
        public static void ToCsv<T>(IEnumerable<T> items, string filePath, bool hasHeader, bool writePreamble = true, char valueSeparator = ',')
        {
            ExportToFile(items, FileType.Csv, filePath, hasHeader, writePreamble, valueSeparator);
        }

        public static void ToTxt<T>(IEnumerable<T> items, string filePath, bool hasHeader, bool writePreamble = true, char valueSeparator = '\t')
        {
            ExportToFile(items, FileType.Txt, filePath, hasHeader, writePreamble, valueSeparator);
        }

        public static void ToXlsx<T>(IEnumerable<T> items, string filePath, bool hasHeader)
        {
            ExportToFile(items, FileType.Xlsx, filePath, hasHeader);
        }

        public static void ToXls<T>(IEnumerable<T> items, string filePath, bool hasHeader)
        {
            ExportToFile(items, FileType.Xls, filePath, hasHeader);
        }

        public static void ToXlsx(DataTable dt, string filePath, bool hasHeader)
        {
            Workbook wb = new Workbook();

            // 預設的Csv輸出中文會是亂碼，所以要加這行
            wb.Options.Export.Csv.WritePreamble = true;
            wb.Worksheets[0].Import(dt, hasHeader, 0, 0);
            wb.SaveDocument(filePath, ConvertFileType(FileType.Xlsx));
        }

        private static void ExportToFile<T>(this IEnumerable<T> items, FileType fileType, string filePath, bool hasHeader, bool writePreamble = true, char valueSeparator = ',')
        {
            Workbook wb = new Workbook();

            // 預設的Csv輸出中文會是亂碼，所以要加這行
            wb.Options.Export.Csv.WritePreamble = writePreamble;
            wb.Options.Export.Txt.WritePreamble = writePreamble;

            wb.Options.Export.Csv.ValueSeparator = valueSeparator;
            wb.Options.Export.Txt.ValueSeparator = valueSeparator;

            // Access the first worksheet in the workbook.
            Worksheet worksheet = wb.Worksheets[0];

            // Set the unit of measurement.
            wb.Unit = DevExpress.Office.DocumentUnit.Point;

            wb.BeginUpdate();

            var startRow = 0;
            var colIndex = 0;

            if (hasHeader)
            {
                startRow = 1;
                foreach (var prop in typeof(T).GetProperties())
                {
                    bool isColumn = true;
                    var propName = prop.Name;

                    object[] attrs = prop.GetCustomAttributes(true);
                    if (attrs == null || attrs.Length == 0)
                    {
                        isColumn = true;
                    }
                    else
                    {
                        foreach (Attribute attr in attrs)
                        {
                            if (attr is ColumnSettingAttribute)
                            {
                                isColumn = (attr as ColumnSettingAttribute).IsColumn;
                                if (isColumn)
                                {
                                    var displayName = (attr as ColumnSettingAttribute).DisplayName;
                                    if (!string.IsNullOrEmpty(displayName))
                                    {
                                        propName = displayName;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (isColumn)
                    {
                        worksheet.Columns[colIndex][0].Value = propName;
                        colIndex++;
                    }
                }
            }

            for (int i = 0; i < items.Count(); i++)
            {
                var item = items.ElementAtOrDefault(i);

                colIndex = 0;
                foreach (var prop in typeof(T).GetProperties())
                {
                    bool isColumn = true;

                    object[] attrs = prop.GetCustomAttributes(true);
                    if (attrs == null || attrs.Length == 0)
                    {
                    }
                    else
                    {
                        foreach (Attribute attr in attrs)
                        {
                            if (attr is ColumnSettingAttribute)
                            {
                                isColumn = (attr as ColumnSettingAttribute).IsColumn;
                            }
                        }
                    }

                    if (!isColumn) continue;

                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var propValue = prop.GetValue(item, null);

                    if (propValue != null)
                    {
                        var objectData = prop.GetValue(item, null);
                        var cellValue = CellValue.FromObject(objectData, new CustomExcelCellConverter());

                        var cell = worksheet.Columns[colIndex][startRow + i];

                        if (cellValue.Type == CellValueType.DateTime)
                        {
                            cell.NumberFormat = "yyyy/MM/dd HH:mm:ss";
                        }

                        cell.Value = cellValue;
                    }

                    colIndex++;
                }
            }

            wb.EndUpdate();

            wb.Calculate();

            wb.SaveDocument(filePath, ConvertFileType(fileType));
        }

        private static DocumentFormat ConvertFileType(FileType fileType)
        {
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

            return format;
        }
    }

    internal class CustomExcelCellConverter : ICellValueConverter
    {
        public object ConvertToObject(CellValue value)
        {
            throw new NotImplementedException();
        }

        CellValue ICellValueConverter.TryConvertFromObject(object value)
        {
            bool isChar = value.GetType() == typeof(Char);
            if (isChar)
                return value.ToString();
            else
                return CellValue.FromObject(value);
        }
    }
}