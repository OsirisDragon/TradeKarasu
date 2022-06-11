namespace TradeFutNight.Views.Prefix8
{
    partial class U_80003_Report
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
            this.components = new System.ComponentModel.Container();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.txtUpfUserId = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.OcfRocDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.UserName = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportID = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportTitle = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.tableFooter = new DevExpress.XtraReports.UI.XRTable();
            this.tableRowMemo = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCellMemo = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableRowSign = new DevExpress.XtraReports.UI.XRTableRow();
            this.tableCellHandlePerson = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCellConfirmPerson = new DevExpress.XtraReports.UI.XRTableCell();
            this.tableCellManagerPerson = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubreportHeader = new DevExpress.XtraReports.UI.XRSubreport();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tableFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 10F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 10F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtUpfUserId});
            this.Detail.HeightF = 256.6667F;
            this.Detail.Name = "Detail";
            // 
            // txtUpfUserId
            // 
            this.txtUpfUserId.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.txtUpfUserId.BorderWidth = 1F;
            this.txtUpfUserId.EditOptions.Enabled = true;
            this.txtUpfUserId.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UPF_USER_ID]")});
            this.txtUpfUserId.LocationFloat = new DevExpress.Utils.PointFloat(167.5F, 36.83332F);
            this.txtUpfUserId.Multiline = true;
            this.txtUpfUserId.Name = "txtUpfUserId";
            this.txtUpfUserId.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtUpfUserId.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.txtUpfUserId.StylePriority.UseBorders = false;
            this.txtUpfUserId.StylePriority.UseBorderWidth = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportHeader});
            this.PageHeader.HeightF = 109.375F;
            this.PageHeader.Name = "PageHeader";
            // 
            // OcfRocDate
            // 
            this.OcfRocDate.AllowNull = true;
            this.OcfRocDate.Name = "OcfRocDate";
            this.OcfRocDate.Visible = false;
            // 
            // UserName
            // 
            this.UserName.AllowNull = true;
            this.UserName.Name = "UserName";
            this.UserName.Visible = false;
            // 
            // ReportID
            // 
            this.ReportID.AllowNull = true;
            this.ReportID.Name = "ReportID";
            this.ReportID.Visible = false;
            // 
            // ReportTitle
            // 
            this.ReportTitle.AllowNull = true;
            this.ReportTitle.Name = "ReportTitle";
            this.ReportTitle.Visible = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.tableFooter});
            this.ReportFooter.HeightF = 50F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // tableFooter
            // 
            this.tableFooter.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.tableFooter.Name = "tableFooter";
            this.tableFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.tableFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRowMemo,
            this.tableRowSign});
            this.tableFooter.SizeF = new System.Drawing.SizeF(1128.125F, 50F);
            // 
            // tableRowMemo
            // 
            this.tableRowMemo.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCellMemo});
            this.tableRowMemo.Name = "tableRowMemo";
            this.tableRowMemo.Weight = 1D;
            // 
            // tableCellMemo
            // 
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
            this.tableRowSign.Name = "tableRowSign";
            this.tableRowSign.Weight = 1D;
            // 
            // tableCellHandlePerson
            // 
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
            this.tableCellManagerPerson.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tableCellManagerPerson.Multiline = true;
            this.tableCellManagerPerson.Name = "tableCellManagerPerson";
            this.tableCellManagerPerson.StylePriority.UseFont = false;
            this.tableCellManagerPerson.StylePriority.UseTextAlignment = false;
            this.tableCellManagerPerson.Text = "主管：";
            this.tableCellManagerPerson.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tableCellManagerPerson.Weight = 1.1931096178125713D;
            // 
            // xrSubreportHeader
            // 
            this.xrSubreportHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrSubreportHeader.Name = "xrSubreportHeader";
            this.xrSubreportHeader.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("OcfRocDate", this.OcfRocDate));
            this.xrSubreportHeader.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("UserName", this.UserName));
            this.xrSubreportHeader.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("ReportID", this.ReportID));
            this.xrSubreportHeader.ParameterBindings.Add(new DevExpress.XtraReports.UI.ParameterBinding("ReportTitle", this.ReportTitle));
            this.xrSubreportHeader.ReportSource = new TradeFutNight.Reports.ReportCommonLandscapeHeader();
            this.xrSubreportHeader.SizeF = new System.Drawing.SizeF(1128.125F, 109.375F);
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(TradeFutNight.Views.Prefix8.UIModel_80003);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // U_80003_Report
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.PageHeader,
            this.ReportFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 10, 10);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.OcfRocDate,
            this.ReportID,
            this.ReportTitle,
            this.UserName});
            this.Version = "19.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.U_80003_Report_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.tableFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportHeader;
        private DevExpress.XtraReports.Parameters.Parameter OcfRocDate;
        private DevExpress.XtraReports.Parameters.Parameter ReportID;
        private DevExpress.XtraReports.Parameters.Parameter ReportTitle;
        private DevExpress.XtraReports.Parameters.Parameter UserName;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable tableFooter;
        private DevExpress.XtraReports.UI.XRTableRow tableRowMemo;
        private DevExpress.XtraReports.UI.XRTableCell tableCellMemo;
        private DevExpress.XtraReports.UI.XRTableRow tableRowSign;
        private DevExpress.XtraReports.UI.XRTableCell tableCellHandlePerson;
        private DevExpress.XtraReports.UI.XRTableCell tableCellConfirmPerson;
        private DevExpress.XtraReports.UI.XRTableCell tableCellManagerPerson;
        private DevExpress.XtraReports.UI.XRLabel txtUpfUserId;
    }
}
