using CrossModel;
using CrossModel.Enum;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TradeFutNight.Reports
{
    public static class ReportNormal
    {
        public static ReportSetting CreateSetting(string programID, string reportTitle, string userName, string memoText, DateTime ocfDate, bool hasHandlePerson, bool hasConfirmPerson, bool hasManagerPerson)
        {
            return new ReportSetting(programID, reportTitle, userName, memoText, ocfDate, hasHandlePerson, hasConfirmPerson, hasManagerPerson);
        }

        public static ReportCommonLandscape<T> CreateCommonLandscape<T>(IList<T> exportData, GridColumnCollection columns, ReportSetting rptSetting)
        {
            return new ReportCommonLandscape<T>(exportData, columns, rptSetting);
        }

        public static XRTable CreateHeaderColumnsTable(ReportSetting rptSetting, GridColumnCollection columns)
        {
            XRTable table = new XRTable();
            table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table.BeginInit();

            XRTableRow row = new XRTableRow();
            row.HeightF = rptSetting.HeaderColumnsRowHeight;

            foreach (var col in columns)
            {
                XRTableCell cell = new XRTableCell();
                cell.Multiline = true;
                cell.Text = col.Header.ToString().Replace("<br/>", "\n");
                cell.BackColor = Color.LightCyan;

                if (col.Width != 0)
                {
                    cell.WidthF = (float)(col.Width.Value * 1f);
                }

                if (col.EditSettings != null && !String.IsNullOrEmpty(col.EditSettings.DisplayFormat))
                {
                    cell.TextFormatString = col.EditSettings.DisplayFormat;
                }

                if (col.FieldName == "ModifyMark")
                {
                    if (String.IsNullOrEmpty(col.Header.ToString()))
                    {
                        cell.Borders = BorderSide.None;
                        cell.BackColor = Color.White;
                    }
                }

                cell.Font = new Font(rptSetting.HeaderColumnsFontName, rptSetting.HeaderColumnsFontSize);
                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                row.Cells.Add(cell);
            }

            table.Rows.Add(row);

            table.AdjustSize();
            table.EndInit();

            return table;
        }

        public static XRTable CreateContentTable<T>(IList<T> exportData, ReportSetting rptSetting, GridColumnCollection columns)
        {
            XRTable table = new XRTable();
            table.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
            table.BeginInit();

            XRTableRow row = new XRTableRow();
            row.HeightF = 15f;

            foreach (var col in columns)
            {
                XRTableCell cell = new XRTableCell();

                if (!string.IsNullOrWhiteSpace(col.FieldName))
                {
                    if (col.EditSettings is ComboBoxEditSettings)
                    {
                        var expressText = TransformExpress(col, ExpressionType.Text);
                        if (!string.IsNullOrEmpty(expressText))
                            cell.ExpressionBindings.Add(new ExpressionBinding("Text", expressText));

                        //var expressForeColor = TransformExpress(col, ExpressionType.ForeColor);
                        //if (!string.IsNullOrEmpty(expressForeColor))
                        //    cell.ExpressionBindings.Add(new ExpressionBinding("ForeColor", expressForeColor));
                    }
                    else if (col.EditSettings != null && !String.IsNullOrEmpty(col.EditSettings.DisplayFormat))
                    {
                        //string format = "{0:" + col.EditSettings.DisplayFormat + "}";
                        cell.DataBindings.Add("Text", exportData, col.FieldName, col.EditSettings.DisplayFormat);
                    }
                    else
                    {
                        cell.DataBindings.Add("Text", exportData, col.FieldName);
                    }

                    if (col.FieldName == "ModifyMark")
                    {
                        if (String.IsNullOrEmpty(col.Header.ToString()))
                            cell.Borders = BorderSide.None;
                    }
                }
                else
                {
                    if (col.Name == "rowNumber")
                    {
                        cell.Summary.Running = SummaryRunning.Report;
                        cell.Summary.Func = SummaryFunc.RecordNumber;
                    }
                }

                if (col.HorizontalHeaderContentAlignment != System.Windows.HorizontalAlignment.Left)
                {
                    cell.TextAlignment = TransformAlignment(col.ActualHorizontalContentAlignment);
                }

                if (col.Width != 0)
                {
                    cell.WidthF = (float)(col.Width.Value * 1f);
                }

                cell.Font = new Font(rptSetting.ContentColumnsFontName, rptSetting.ContentColumnsFontSize);

                PaddingInfo paddingInfo = new PaddingInfo();
                paddingInfo.Left = 1;
                paddingInfo.Right = 1;

                cell.Padding = paddingInfo;

                if (col.AllowCellMerge.HasValue && col.AllowCellMerge.Value)
                {
                    cell.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
                }

                row.Cells.Add(cell);
            }

            table.Rows.Add(row);

            table.AdjustSize();
            table.EndInit();

            return table;
        }

        private static string TransformExpress(GridColumn col, ExpressionType expressionType)
        {
            // 將下拉選單List物件轉成Iif的報表判斷表達字串
            //範例：Iif([SLT_PRICE_FLUC] = 'P', '百分比', Iif([SLT_PRICE_FLUC] = 'F', '固定點數', ''))
            string field = "[" + col.FieldName + "]";
            string result = "";

            var items = (IList<ItemInfo>)((ComboBoxEditSettings)col.EditSettings).ItemsSource;
            var numberOfIf = 0;

            for (int i = 0; i < items.Count; i++)
            {
                string whichValue = "";
                if (expressionType == ExpressionType.Text)
                {
                    whichValue = items[i].Text;
                }
                else if (expressionType == ExpressionType.ForeColor)
                {
                    // 預設給黑色
                    //whichValue = "Black";
                    //if (!string.IsNullOrEmpty(items[i].ForeColor))
                    //{
                    //    whichValue = items[i].ForeColor;
                    //}
                    //else
                    //{
                    //    continue;
                    //}
                }

                result += "Iif(" + field + "='" + items[i].Value + "','" + whichValue + "',";
                numberOfIf++;
            }

            if (result != "")
                result += "''";

            for (int i = 0; i < numberOfIf; i++)
            {
                result += ")";
            }

            return result;
        }

        private static TextAlignment TransformAlignment(System.Windows.HorizontalAlignment align)
        {
            TextAlignment resultAlign = TextAlignment.TopLeft;

            switch (align)
            {
                case System.Windows.HorizontalAlignment.Stretch:
                    break;

                case System.Windows.HorizontalAlignment.Center:
                    resultAlign = TextAlignment.TopCenter;
                    break;

                case System.Windows.HorizontalAlignment.Left:
                    resultAlign = TextAlignment.TopLeft;
                    break;

                case System.Windows.HorizontalAlignment.Right:
                    resultAlign = TextAlignment.TopRight;
                    break;

                default:
                    break;
            }

            return resultAlign;
        }
    }
}