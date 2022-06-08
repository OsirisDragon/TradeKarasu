namespace TradeFutNight.Reports
{
    partial class ReportCommonLandscapeHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportCommonLandscapeHeader));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblHeaderUserName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderReportID = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderOperateTime = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderChineseDatetime = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderOperateDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.pageInfoMain = new DevExpress.XtraReports.UI.XRPageInfo();
            this.pictureBoxTitle = new DevExpress.XtraReports.UI.XRPictureBox();
            this.OcfRocDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.UserName = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportID = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportTitle = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 96F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 96F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 96F;
            this.Detail.Name = "Detail";
            // 
            // lblHeaderUserName
            // 
            this.lblHeaderUserName.Dpi = 96F;
            this.lblHeaderUserName.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?UserName")});
            this.lblHeaderUserName.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderUserName.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblHeaderUserName.Multiline = true;
            this.lblHeaderUserName.Name = "lblHeaderUserName";
            this.lblHeaderUserName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderUserName.SizeF = new System.Drawing.SizeF(200F, 24F);
            this.lblHeaderUserName.StylePriority.UseFont = false;
            this.lblHeaderUserName.StylePriority.UseTextAlignment = false;
            this.lblHeaderUserName.Text = "測試人員";
            this.lblHeaderUserName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblHeaderUserName.TextFormatString = "作業人員：{0}";
            // 
            // lblHeaderReportID
            // 
            this.lblHeaderReportID.Dpi = 96F;
            this.lblHeaderReportID.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?ReportID")});
            this.lblHeaderReportID.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderReportID.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblHeaderReportID.Multiline = true;
            this.lblHeaderReportID.Name = "lblHeaderReportID";
            this.lblHeaderReportID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderReportID.SizeF = new System.Drawing.SizeF(200F, 24F);
            this.lblHeaderReportID.StylePriority.UseFont = false;
            this.lblHeaderReportID.StylePriority.UseTextAlignment = false;
            this.lblHeaderReportID.Text = "F00000";
            this.lblHeaderReportID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblHeaderReportID.TextFormatString = "報表代號：{0}";
            // 
            // lblHeaderOperateTime
            // 
            this.lblHeaderOperateTime.Dpi = 96F;
            this.lblHeaderOperateTime.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Now()")});
            this.lblHeaderOperateTime.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderOperateTime.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblHeaderOperateTime.Multiline = true;
            this.lblHeaderOperateTime.Name = "lblHeaderOperateTime";
            this.lblHeaderOperateTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderOperateTime.SizeF = new System.Drawing.SizeF(200F, 24F);
            this.lblHeaderOperateTime.StylePriority.UseFont = false;
            this.lblHeaderOperateTime.StylePriority.UseTextAlignment = false;
            this.lblHeaderOperateTime.Text = "00:00:00";
            this.lblHeaderOperateTime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblHeaderOperateTime.TextFormatString = "作業時間：{0:HH:mm:ss}";
            // 
            // lblHeaderChineseDatetime
            // 
            this.lblHeaderChineseDatetime.Dpi = 96F;
            this.lblHeaderChineseDatetime.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?OcfRocDate")});
            this.lblHeaderChineseDatetime.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderChineseDatetime.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblHeaderChineseDatetime.Multiline = true;
            this.lblHeaderChineseDatetime.Name = "lblHeaderChineseDatetime";
            this.lblHeaderChineseDatetime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderChineseDatetime.SizeF = new System.Drawing.SizeF(683F, 24F);
            this.lblHeaderChineseDatetime.StylePriority.UseFont = false;
            this.lblHeaderChineseDatetime.StylePriority.UseTextAlignment = false;
            this.lblHeaderChineseDatetime.Text = "中華民國000年00月00日";
            this.lblHeaderChineseDatetime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblHeaderOperateDate
            // 
            this.lblHeaderOperateDate.Dpi = 96F;
            this.lblHeaderOperateDate.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "GetDate(Today())")});
            this.lblHeaderOperateDate.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderOperateDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblHeaderOperateDate.Multiline = true;
            this.lblHeaderOperateDate.Name = "lblHeaderOperateDate";
            this.lblHeaderOperateDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderOperateDate.SizeF = new System.Drawing.SizeF(683F, 24F);
            this.lblHeaderOperateDate.StylePriority.UseFont = false;
            this.lblHeaderOperateDate.StylePriority.UseTextAlignment = false;
            this.lblHeaderOperateDate.Text = "作業日期：0000/00/00";
            this.lblHeaderOperateDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblHeaderOperateDate.TextFormatString = "作業日期：{0:yyyy/MM/dd}";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Dpi = 96F;
            this.lblHeaderTitle.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?ReportTitle")});
            this.lblHeaderTitle.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblHeaderTitle.Multiline = true;
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderTitle.SizeF = new System.Drawing.SizeF(1083F, 24F);
            this.lblHeaderTitle.StylePriority.UseFont = false;
            this.lblHeaderTitle.StylePriority.UseTextAlignment = false;
            this.lblHeaderTitle.Text = "99999–我是標題我是標題(夜盤)";
            this.lblHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // pageInfoMain
            // 
            this.pageInfoMain.Dpi = 96F;
            this.pageInfoMain.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.pageInfoMain.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.pageInfoMain.Name = "pageInfoMain";
            this.pageInfoMain.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.pageInfoMain.SizeF = new System.Drawing.SizeF(200F, 24F);
            this.pageInfoMain.StylePriority.UseFont = false;
            this.pageInfoMain.StylePriority.UseTextAlignment = false;
            this.pageInfoMain.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.pageInfoMain.TextFormatString = "頁次： {0} / {1}";
            // 
            // pictureBoxTitle
            // 
            this.pictureBoxTitle.Dpi = 96F;
            this.pictureBoxTitle.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("pictureBoxTitle.ImageSource"));
            this.pictureBoxTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.pictureBoxTitle.Name = "pictureBoxTitle";
            this.pictureBoxTitle.SizeF = new System.Drawing.SizeF(1083F, 24F);
            this.pictureBoxTitle.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
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
            // xrTable1
            // 
            this.xrTable1.Dpi = 96F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow3,
            this.xrTableRow4});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1083F, 96F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
            this.xrTableRow1.Dpi = 96F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pictureBoxTitle});
            this.xrTableCell2.Dpi = 96F;
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 3D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5});
            this.xrTableRow2.Dpi = 96F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHeaderTitle});
            this.xrTableCell5.Dpi = 96F;
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 3D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9});
            this.xrTableRow3.Dpi = 96F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHeaderReportID});
            this.xrTableCell7.Dpi = 96F;
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "xrTableCell7";
            this.xrTableCell7.Weight = 0.554016620498615D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHeaderChineseDatetime});
            this.xrTableCell8.Dpi = 96F;
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "xrTableCell8";
            this.xrTableCell8.Weight = 1.8919667590027702D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pageInfoMain});
            this.xrTableCell9.Dpi = 96F;
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "xrTableCell9";
            this.xrTableCell9.Weight = 0.554016620498615D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow4.Dpi = 96F;
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHeaderOperateTime});
            this.xrTableCell10.Dpi = 96F;
            this.xrTableCell10.Multiline = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "xrTableCell10";
            this.xrTableCell10.Weight = 0.554016620498615D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHeaderOperateDate});
            this.xrTableCell11.Dpi = 96F;
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "xrTableCell11";
            this.xrTableCell11.Weight = 1.8919667590027702D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHeaderUserName});
            this.xrTableCell12.Dpi = 96F;
            this.xrTableCell12.Multiline = true;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "xrTableCell12";
            this.xrTableCell12.Weight = 0.554016620498615D;
            // 
            // ReportCommonLandscapeHeader
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Dpi = 96F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 794;
            this.PageWidth = 1123;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.OcfRocDate,
            this.UserName,
            this.ReportID,
            this.ReportTitle});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Pixels;
            this.SnapGridSize = 12.5F;
            this.Version = "19.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderUserName;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderReportID;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderOperateTime;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderChineseDatetime;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderOperateDate;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderTitle;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfoMain;
        private DevExpress.XtraReports.UI.XRPictureBox pictureBoxTitle;
        private DevExpress.XtraReports.Parameters.Parameter OcfRocDate;
        private DevExpress.XtraReports.Parameters.Parameter UserName;
        private DevExpress.XtraReports.Parameters.Parameter ReportID;
        private DevExpress.XtraReports.Parameters.Parameter ReportTitle;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
    }
}
