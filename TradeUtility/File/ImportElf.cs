using CrossModel.Enum;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using System.Data;

namespace TradeUtility.File
{
    public static class ImportElf
    {
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

        public static DataTable FileToDataTable(string fileName, FileType fileType, bool hasHeader)
        {
            DocumentFormat format = ConvertFileType(fileType);
            Workbook workbook = new Workbook();
            workbook.LoadDocument(fileName, format);
            Worksheet sheet = workbook.Worksheets[0];
            var data = sheet.GetExistingCells();
            var range = sheet.GetUsedRange();

            DataTable dtResult = sheet.CreateDataTable(range, hasHeader);
            DataTableExporter exporter = sheet.CreateDataTableExporter(range, dtResult, hasHeader);
            exporter.Options.ConvertEmptyCells = true;
            exporter.Options.DefaultCellValueToColumnTypeConverter.EmptyCellValue = 0;
            exporter.Export();

            return dtResult;
        }
    }
}