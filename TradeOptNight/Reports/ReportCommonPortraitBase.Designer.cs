namespace TradeOptNight.Reports
{
    partial class ReportCommonPortraitBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.OcfRocDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.UserName = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportID = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportTitle = new DevExpress.XtraReports.Parameters.Parameter();
            this.SubBandHeaderMemo = new DevExpress.XtraReports.UI.SubBand();
            this.lblHeaderMemo = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.tableFooter = new DevExpress.XtraReports.UI.XRTable();
            this.tableRowMemo = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCellMemo = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableRowSign = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCellHandlePerson = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCellConfirmPerson = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCellManagerPerson = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeaderColumns = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrSubreportHeader = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.tableFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 96F;
            this.TopMargin.HeightF = 10F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 96F;
            this.BottomMargin.HeightF = 10F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 13.44F;
            this.Detail.Name = "Detail";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportHeader});
            this.PageHeader.Dpi = 96F;
            this.PageHeader.HeightF = 95F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.SubBands.AddRange(new DevExpress.XtraReports.UI.SubBand[] {
            this.SubBandHeaderMemo});
            // 
            // OcfRocDate
            // 
            this.OcfRocDate.AllowNull = true;
            this.OcfRocDate.Description = "OCF的中華民國表示日期";
            this.OcfRocDate.Name = "OcfRocDate";
            this.OcfRocDate.Visible = false;
            // 
            // UserName
            // 
            this.UserName.AllowNull = true;
            this.UserName.Description = "UserName";
            this.UserName.Name = "UserName";
            this.UserName.Visible = false;
            // 
            // ReportID
            // 
            this.ReportID.AllowNull = true;
            this.ReportID.Description = "ReportID";
            this.ReportID.Name = "ReportID";
            this.ReportID.Visible = false;
            // 
            // ReportTitle
            // 
            this.ReportTitle.AllowNull = true;
            this.ReportTitle.Description = "ReportTitle";
            this.ReportTitle.Name = "ReportTitle";
            this.ReportTitle.Visible = false;
            // 
            // SubBandHeaderMemo
            // 
            this.SubBandHeaderMemo.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHeaderMemo});
            this.SubBandHeaderMemo.Dpi = 96F;
            this.SubBandHeaderMemo.HeightF = 24F;
            this.SubBandHeaderMemo.Name = "SubBandHeaderMemo";
            // 
            // lblHeaderMemo
            // 
            this.lblHeaderMemo.AutoWidth = true;
            this.lblHeaderMemo.CanShrink = true;
            this.lblHeaderMemo.Dpi = 96F;
            this.lblHeaderMemo.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderMemo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblHeaderMemo.Multiline = true;
            this.lblHeaderMemo.Name = "lblHeaderMemo";
            this.lblHeaderMemo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 96F);
            this.lblHeaderMemo.SizeF = new System.Drawing.SizeF(754F, 24F);
            this.lblHeaderMemo.StylePriority.UseFont = false;
            this.lblHeaderMemo.StylePriority.UsePadding = false;
            this.lblHeaderMemo.StylePriority.UseTextAlignment = false;
            this.lblHeaderMemo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tableFooter});
            this.ReportFooter.Dpi = 96F;
            this.ReportFooter.HeightF = 55F;
            this.ReportFooter.KeepTogether = true;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // tableFooter
            // 
            this.tableFooter.Dpi = 96F;
            this.tableFooter.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3F);
            this.tableFooter.Name = "tableFooter";
            this.tableFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.tableFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRowMemo,
            this.tableRowSign});
            this.tableFooter.SizeF = new System.Drawing.SizeF(754F, 48F);
            // 
            // tableRowMemo
            // 
            this.tableRowMemo.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCellMemo});
            this.tableRowMemo.Dpi = 96F;
            this.tableRowMemo.Name = "tableRowMemo";
            this.tableRowMemo.Weight = 1D;
            // 
            // tableCellMemo
            // 
            this.tableCellMemo.Dpi = 96F;
            this.tableCellMemo.Font = new System.Drawing.Font("標楷體", 10F);
            this.tableCellMemo.Multiline = true;
            this.tableCellMemo.Name = "tableCellMemo";
            this.tableCellMemo.StylePriority.UseFont = false;
            this.tableCellMemo.Weight = 4D;
            // 
            // tableRowSign
            // 
            this.tableRowSign.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCellHandlePerson,
            this.tableCellConfirmPerson,
            this.tableCellManagerPerson});
            this.tableRowSign.Dpi = 96F;
            this.tableRowSign.Name = "tableRowSign";
            this.tableRowSign.Weight = 1D;
            // 
            // tableCellHandlePerson
            // 
            this.tableCellHandlePerson.Dpi = 96F;
            this.tableCellHandlePerson.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tableCellHandlePerson.Multiline = true;
            this.tableCellHandlePerson.Name = "tableCellHandlePerson";
            this.tableCellHandlePerson.StylePriority.UseFont = false;
            this.tableCellHandlePerson.StylePriority.UseTextAlignment = false;
            this.tableCellHandlePerson.Text = "經辦：";
            this.tableCellHandlePerson.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tableCellHandlePerson.Weight = 1.2175882500296598D;
            // 
            // tableCellConfirmPerson
            // 
            this.tableCellConfirmPerson.Dpi = 96F;
            this.tableCellConfirmPerson.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tableCellConfirmPerson.Multiline = true;
            this.tableCellConfirmPerson.Name = "tableCellConfirmPerson";
            this.tableCellConfirmPerson.StylePriority.UseFont = false;
            this.tableCellConfirmPerson.StylePriority.UseTextAlignment = false;
            this.tableCellConfirmPerson.Text = "複核：";
            this.tableCellConfirmPerson.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.tableCellConfirmPerson.Weight = 0.58930213215776939D;
            // 
            // tableCellManagerPerson
            // 
            this.tableCellManagerPerson.Dpi = 96F;
            this.tableCellManagerPerson.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tableCellManagerPerson.Multiline = true;
            this.tableCellManagerPerson.Name = "tableCellManagerPerson";
            this.tableCellManagerPerson.StylePriority.UseFont = false;
            this.tableCellManagerPerson.StylePriority.UseTextAlignment = false;
            this.tableCellManagerPerson.Text = "主管：";
            this.tableCellManagerPerson.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tableCellManagerPerson.Weight = 1.1931096178125713D;
            // 
            // GroupHeaderColumns
            // 
            this.GroupHeaderColumns.Dpi = 96F;
            this.GroupHeaderColumns.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
            this.GroupHeaderColumns.HeightF = 9.6F;
            this.GroupHeaderColumns.Name = "GroupHeaderColumns";
            this.GroupHeaderColumns.RepeatEveryPage = true;
            // 
            // xrSubreportHeader
            // 
            this.xrSubreportHeader.Dpi = 96F;
            this.xrSubreportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrSubreportHeader.Name = "xrSubreportHeader";
            this.xrSubreportHeader.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("OcfRocDate", this.OcfRocDate));
            this.xrSubreportHeader.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("UserName", this.UserName));
            this.xrSubreportHeader.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("ReportID", this.ReportID));
            this.xrSubreportHeader.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("ReportTitle", this.ReportTitle));
            this.xrSubreportHeader.ReportSource = new TradeOptNight.Reports.ReportCommonPortraitHeader();
            this.xrSubreportHeader.SizeF = new System.Drawing.SizeF(754F, 95F);
            // 
            // ReportCommonPortraitBase
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeaderColumns});
            this.Dpi = 96F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 10, 10);
            this.PageHeight = 1123;
            this.PageWidth = 794;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.OcfRocDate,
            this.UserName,
            this.ReportTitle,
            this.ReportID});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Pixels;
            this.SnapGridSize = 12.5F;
            this.Version = "19.2";
            ((System.ComponentModel.ISupportInitialize)(this.tableFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable tableFooter;
        private DevExpress.XtraReports.UI.XRTableRow tableRowMemo;
        private DevExpress.XtraReports.UI.XRTableCell tableCellMemo;
        private DevExpress.XtraReports.UI.XRTableRow tableRowSign;
        private DevExpress.XtraReports.UI.XRTableCell tableCellHandlePerson;
        private DevExpress.XtraReports.UI.XRTableCell tableCellConfirmPerson;
        private DevExpress.XtraReports.UI.XRTableCell tableCellManagerPerson;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderColumns;
        private DevExpress.XtraReports.Parameters.Parameter OcfRocDate;
        private DevExpress.XtraReports.Parameters.Parameter UserName;
        private DevExpress.XtraReports.Parameters.Parameter ReportTitle;
        private DevExpress.XtraReports.Parameters.Parameter ReportID;
        private DevExpress.XtraReports.UI.SubBand SubBandHeaderMemo;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderMemo;
    }
}
