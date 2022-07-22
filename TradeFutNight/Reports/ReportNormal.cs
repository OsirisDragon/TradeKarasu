using CrossModel;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TradeFutNight.Reports
{
    public static class ReportNormal
    {
        public static ReportSetting CreateSetting(string programID, string reportTitle, string userName, string memoText, DateTime ocfDate, bool hasHandlePerson, bool hasConfirmPerson, bool hasManagerPerson)
        {
            return new ReportSetting(programID, reportTitle, userName, memoText, ocfDate, hasHandlePerson, hasConfirmPerson, hasManagerPerson);
        }

        public static ReportCommonLandscape<T> CreateCommonLandscape<T>(IList<T> exportData, GridControl gridControl, ReportSetting rptSetting)
        {
            return new ReportCommonLandscape<T>(exportData, gridControl, rptSetting);
        }

        public static ReportCommonPortrait CreateCommonPortrait<T>(IList<T> exportData, GridControl gridControl, ReportSetting rptSetting)
        {
            return new ReportCommonPortrait((IList)exportData, gridControl, rptSetting);
        }

        public static ReportCommonPortrait CreateCommonPortraitForScreenImage(Image imageDetail, ReportSetting rptSetting)
        {
            return new ReportCommonPortrait(imageDetail, rptSetting);
        }

        public static XRTable CreateHeaderColumnsTable(XtraReport report, ReportSetting rptSetting, GridControl gridControl)
        {
            var table = new XRTable
            {
                Borders = DevExpress.XtraPrinting.BorderSide.All
            };
            table.BeginInit();

            var row = new XRTableRow
            {
                HeightF = rptSetting.HeaderColumnsRowHeight
            };

            foreach (var col in gridControl.Columns)
            {
                var cell = new XRTableCell
                {
                    Multiline = true,
                    BackColor = Color.LightCyan
                };

                if (col.Header != null)
                    cell.Text = col.Header.ToString().Replace("<br/>", "\n");

                if (col.Width != 0)
                {
                    cell.WidthF = (float)(col.Width.Value * rptSetting.HeaderColumnsWidthScaleFactor);
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

                // 用CalculatedField的方式加入進報表，後面其他的Expression才能用中括號[加入的這個的CalcField的Name]存取到此欄位
                if (col.UnboundExpression != "")
                {
                    CalculatedField calcField = new CalculatedField();
                    report.CalculatedFields.Add(calcField);

                    calcField.DataSource = report.DataSource;
                    calcField.DataMember = report.DataMember;
                    calcField.Name = col.FieldName;
                    calcField.Expression = col.UnboundExpression;
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

        public static XRTable CreateContentTable(IList exportData, ReportSetting rptSetting, GridControl gridControl)
        {
            var table = new XRTable
            {
                Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom
            };
            table.BeginInit();

            var row = new XRTableRow
            {
                HeightF = 15f
            };

            #region FormatConditions先轉換

            // FormatConditions先轉換成一個Dictionary，將同樣FieldName和PropertyName的歸類成同一個Key，多個條件的Iif合成一個
            // 最後一個空白字串就是傳DefaultValue這個屬性
            // 範例：Iif([SLT_PRICE_FLUC] = 'P', 'Red', Iif([SLT_PRICE_FLUC] = 'F', 'Blue', ''))
            var expressProps = new Dictionary<Tuple<string, string>, ExpressProp>();

            var formatConditions = ((TableView)gridControl.View).FormatConditions;

            if (formatConditions.Count != 0)
            {
                foreach (var condition in formatConditions)
                {
                    if (condition is FormatCondition fc)
                    {
                        var expressProp = new ExpressProp();
                        expressProp.FieldName = fc.FieldName;

                        if (fc.Format.Foreground != null)
                        {
                            expressProp.PropertyName = "ForeColor";
                            expressProp.Expression = "Iif(" + fc.Expression + ",'" + fc.Format.Foreground.ToString() + "'";
                            expressProp.DefaultValue = "'Black'";
                        }

                        if (fc.Format.Background != null)
                        {
                            expressProp.PropertyName = "BackColor";
                            expressProp.Expression = "Iif(" + fc.Expression + ",'" + fc.Format.Background.ToString() + "'";
                            expressProp.DefaultValue = "'White'";
                        }

                        if (fc.Format.FontWeight != null)
                        {
                            if (fc.Format.FontWeight == System.Windows.FontWeights.Bold)
                            {
                                expressProp.PropertyName = "Font.Bold";
                                expressProp.Expression = "Iif(" + fc.Expression + ", true";
                                expressProp.DefaultValue = "false";
                            }
                        }

                        if (expressProp.PropertyName != "")
                        {
                            var tupleKey = new Tuple<string, string>(fc.FieldName, expressProp.PropertyName);

                            if (expressProps.ContainsKey(tupleKey))
                            {
                                ExpressProp existEp;
                                expressProps.TryGetValue(tupleKey, out existEp);
                                existEp.Expression = existEp.Expression + "," + expressProp.Expression;
                                existEp.NumberOfExpression++;
                            }
                            else
                            {
                                expressProp.NumberOfExpression++;
                                expressProps.Add(tupleKey, expressProp);
                            }
                        }
                    }
                }

                foreach (var expProp in expressProps)
                {
                    var exp = expProp.Value;
                    exp.Expression += "," + exp.DefaultValue;

                    // 有幾個Iif就要有幾個結束的括號
                    for (int i = 0; i < exp.NumberOfExpression; i++)
                    {
                        exp.Expression += ")";
                    }
                }
            }

            #endregion FormatConditions先轉換

            foreach (var col in gridControl.Columns)
            {
                XRTableCell cell = new XRTableCell();

                if (!string.IsNullOrWhiteSpace(col.FieldName))
                {
                    if (col.EditSettings is CheckEditSettings)
                    {
                        var checkBox = new XRCheckBox();
                        if (col.FieldType != typeof(bool))
                        {
                            checkBox.ExpressionBindings.Add(new ExpressionBinding("CheckState", $"Iif([{col.FieldName}]='Y',true,false)"));
                        }
                        else
                        {
                            checkBox.DataBindings.Add("CheckState", exportData, col.FieldName);
                        }

                        checkBox.LocationF = new PointF(10, 0);
                        checkBox.Borders = BorderSide.None;
                        cell.Controls.Add(checkBox);
                    }

                    if (col.EditSettings is ComboBoxEditSettings)
                    {
                        var expressText = TransformExpress(col);
                        if (!string.IsNullOrEmpty(expressText))
                            cell.ExpressionBindings.Add(new ExpressionBinding("Text", expressText));
                    }
                    else
                    {
                        cell.DataBindings.Add("Text", exportData, col.FieldName);
                    }

                    if (col.EditSettings != null && !String.IsNullOrEmpty(col.EditSettings.DisplayFormat))
                    {
                        cell.TextFormatString = col.EditSettings.DisplayFormat;
                    }

                    if (col.FieldName == "ModifyMark")
                    {
                        if (String.IsNullOrEmpty(col.Header.ToString()))
                            cell.Borders = BorderSide.None;
                    }
                }
                else
                {
                    if (col.Name.StartsWith("rowNumber"))
                    {
                        cell.Summary.Running = SummaryRunning.Report;
                        cell.Summary.Func = SummaryFunc.RecordNumber;
                    }
                }

                cell.TextAlignment = TransformAlignment(col.ActualHorizontalContentAlignment);

                if (col.Width != 0)
                {
                    cell.WidthF = (float)(col.Width.Value * rptSetting.ContentColumnsWidthScaleFactor);
                }

                cell.Font = new Font(rptSetting.ContentColumnsFontName, rptSetting.ContentColumnsFontSize);

                var paddingInfo = new PaddingInfo
                {
                    Left = 1,
                    Right = 1
                };

                cell.Padding = paddingInfo;

                if (col.AllowCellMerge.HasValue && col.AllowCellMerge.Value)
                {
                    cell.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
                }

                #region Cell Color

                // 如果該欄位ReadOnly
                if (col.ReadOnly)
                {
                    // 且有設定CellStyle的話，像設定ShareStyle.xaml裡面的CellStyleReadOnlyChangeColor這種如果ReadOnly就變顏色的設定的話
                    if (col.CellStyle != null)
                    {
                        cell.BackColor = Color.LightGray;
                    }
                }

                #endregion Cell Color

                #region FormatConditions設定進去

                if (expressProps.Count != 0)
                {
                    foreach (var expProp in expressProps)
                    {
                        // 找跟欄位一樣名的條件
                        if (expProp.Key.Item1 == col.FieldName)
                        {
                            cell.ExpressionBindings.Add(new ExpressionBinding(expProp.Value.PropertyName, TransformConditionFormatToXtrareportExpression(expProp.Value.Expression)));
                        }
                    }
                }

                #endregion FormatConditions設定進去

                row.Cells.Add(cell);
            }

            table.Rows.Add(row);

            table.AdjustSize();
            table.EndInit();

            return table;
        }

        public static void SetReportHeaderParameters(XtraReport report, ReportSetting rptSetting)
        {
            var paramOcfDate = report.Parameters["OcfRocDate"];
            paramOcfDate.Visible = false;
            paramOcfDate.Value = "中華民國 " + (rptSetting.OcfDate.Year - 1911) + " 年 " + rptSetting.OcfDate.ToString("MM 月 dd 日 ");

            var paramReportID = report.Parameters["ReportID"];
            paramReportID.Visible = false;
            paramReportID.Value = rptSetting.SysShortAliasID + rptSetting.ReportID;

            var paramReportTitle = report.Parameters["ReportTitle"];
            paramReportTitle.Visible = false;
            paramReportTitle.Value = rptSetting.ReportTitle + rptSetting.SysDayOrNightText;

            var paramUserName = report.Parameters["UserName"];
            paramUserName.Visible = false;
            paramUserName.Value = rptSetting.UserName;
        }

        private static string TransformExpress(GridColumn col)
        {
            // 將下拉選單List物件轉成Iif的報表判斷表達字串
            // 範例：Iif([SLT_PRICE_FLUC] = 'P', '百分比', Iif([SLT_PRICE_FLUC] = 'F', '固定點數', ''))
            string field = "[" + col.FieldName + "]";
            string result = "";

            var items = (IList<ItemInfo>)((ComboBoxEditSettings)col.EditSettings).ItemsSource;
            var numberOfIf = 0;

            for (int i = 0; i < items.Count; i++)
            {
                string whichValue = "";
                whichValue = items[i].Text;

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

        private static string TransformConditionFormatToXtrareportExpression(string uiFormat)
        {
            uiFormat = uiFormat.Replace("&lt;", "<");
            uiFormat = uiFormat.Replace("&gt;", ">");
            uiFormat = uiFormat.Replace("&amp;", "&");
            uiFormat = uiFormat.Replace("&quot;", "\"");
            uiFormat = uiFormat.Replace("&apos;", "'");

            return uiFormat;
        }

        private static TextAlignment TransformAlignment(System.Windows.HorizontalAlignment align)
        {
            TextAlignment resultAlign = TextAlignment.MiddleLeft;

            switch (align)
            {
                case System.Windows.HorizontalAlignment.Stretch:
                    break;

                case System.Windows.HorizontalAlignment.Center:
                    resultAlign = TextAlignment.MiddleCenter;
                    break;

                case System.Windows.HorizontalAlignment.Left:
                    resultAlign = TextAlignment.MiddleLeft;
                    break;

                case System.Windows.HorizontalAlignment.Right:
                    resultAlign = TextAlignment.MiddleRight;
                    break;

                default:
                    break;
            }

            return resultAlign;
        }
    }

    public class ExpressProp
    {
        public string FieldName { get; set; }
        public string PropertyName { get; set; }
        public string Expression { get; set; }
        public string DefaultValue { get; set; }
        public int NumberOfExpression { get; set; }
    }
}