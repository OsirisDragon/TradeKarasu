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
            this.lblHeaderReportIDDisplay = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderOperateTimeDisplay = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderOperateTime = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderChineseDatetime = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderOperateDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPageDisplay = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderUserNameDisplay = new DevExpress.XtraReports.UI.XRLabel();
            this.lblHeaderTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.pageInfoMain = new DevExpress.XtraReports.UI.XRPageInfo();
            this.pictureBoxTitle = new DevExpress.XtraReports.UI.XRPictureBox();
            this.OcfRocDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.UserName = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportID = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportTitle = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 96F;
            this.TopMargin.HeightF = 9.6F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 96F;
            this.BottomMargin.HeightF = 9.6F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblHeaderUserName,
            this.lblHeaderReportID,
            this.lblHeaderReportIDDisplay,
            this.lblHeaderOperateTimeDisplay,
            this.lblHeaderOperateTime,
            this.lblHeaderChineseDatetime,
            this.lblHeaderOperateDate,
            this.lblPageDisplay,
            this.lblHeaderUserNameDisplay,
            this.lblHeaderTitle,
            this.pageInfoMain,
            this.pictureBoxTitle});
            this.Detail.Dpi = 96F;
            this.Detail.HeightF = 96F;
            this.Detail.Name = "Detail";
            // 
            // lblHeaderUserName
            // 
            this.lblHeaderUserName.Dpi = 96F;
            this.lblHeaderUserName.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?UserName")});
            this.lblHeaderUserName.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderUserName.LocationFloat = new DevExpress.Utils.PointFloat(987F, 69.23999F);
            this.lblHeaderUserName.Multiline = true;
            this.lblHeaderUserName.Name = "lblHeaderUserName";
            this.lblHeaderUserName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderUserName.SizeF = new System.Drawing.SizeF(96F, 22.08F);
            this.lblHeaderUserName.StylePriority.UseFont = false;
            this.lblHeaderUserName.StylePriority.UseTextAlignment = false;
            this.lblHeaderUserName.Text = "測試人員";
            this.lblHeaderUserName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblHeaderReportID
            // 
            this.lblHeaderReportID.Dpi = 96F;
            this.lblHeaderReportID.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?ReportID")});
            this.lblHeaderReportID.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderReportID.LocationFloat = new DevExpress.Utils.PointFloat(83.00001F, 42.99001F);
            this.lblHeaderReportID.Multiline = true;
            this.lblHeaderReportID.Name = "lblHeaderReportID";
            this.lblHeaderReportID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderReportID.SizeF = new System.Drawing.SizeF(75.00001F, 22.08001F);
            this.lblHeaderReportID.StylePriority.UseFont = false;
            this.lblHeaderReportID.StylePriority.UseTextAlignment = false;
            this.lblHeaderReportID.Text = "F00000";
            this.lblHeaderReportID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblHeaderReportIDDisplay
            // 
            this.lblHeaderReportIDDisplay.Dpi = 96F;
            this.lblHeaderReportIDDisplay.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderReportIDDisplay.LocationFloat = new DevExpress.Utils.PointFloat(0F, 42.99001F);
            this.lblHeaderReportIDDisplay.Multiline = true;
            this.lblHeaderReportIDDisplay.Name = "lblHeaderReportIDDisplay";
            this.lblHeaderReportIDDisplay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderReportIDDisplay.SizeF = new System.Drawing.SizeF(83.00001F, 22.08001F);
            this.lblHeaderReportIDDisplay.StylePriority.UseFont = false;
            this.lblHeaderReportIDDisplay.StylePriority.UseTextAlignment = false;
            this.lblHeaderReportIDDisplay.Text = "報表代號：";
            this.lblHeaderReportIDDisplay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblHeaderOperateTimeDisplay
            // 
            this.lblHeaderOperateTimeDisplay.Dpi = 96F;
            this.lblHeaderOperateTimeDisplay.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderOperateTimeDisplay.LocationFloat = new DevExpress.Utils.PointFloat(0F, 69.23999F);
            this.lblHeaderOperateTimeDisplay.Multiline = true;
            this.lblHeaderOperateTimeDisplay.Name = "lblHeaderOperateTimeDisplay";
            this.lblHeaderOperateTimeDisplay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderOperateTimeDisplay.SizeF = new System.Drawing.SizeF(83.00001F, 22.08F);
            this.lblHeaderOperateTimeDisplay.StylePriority.UseFont = false;
            this.lblHeaderOperateTimeDisplay.StylePriority.UseTextAlignment = false;
            this.lblHeaderOperateTimeDisplay.Text = "作業時間：";
            this.lblHeaderOperateTimeDisplay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblHeaderOperateTime
            // 
            this.lblHeaderOperateTime.Dpi = 96F;
            this.lblHeaderOperateTime.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Now()")});
            this.lblHeaderOperateTime.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderOperateTime.LocationFloat = new DevExpress.Utils.PointFloat(83.29089F, 69.23999F);
            this.lblHeaderOperateTime.Multiline = true;
            this.lblHeaderOperateTime.Name = "lblHeaderOperateTime";
            this.lblHeaderOperateTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderOperateTime.SizeF = new System.Drawing.SizeF(74.7091F, 22.08F);
            this.lblHeaderOperateTime.StylePriority.UseFont = false;
            this.lblHeaderOperateTime.StylePriority.UseTextAlignment = false;
            this.lblHeaderOperateTime.Text = "00:00:00";
            this.lblHeaderOperateTime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblHeaderOperateTime.TextFormatString = "{0:HH:mm:ss}";
            // 
            // lblHeaderChineseDatetime
            // 
            this.lblHeaderChineseDatetime.Dpi = 96F;
            this.lblHeaderChineseDatetime.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?OcfRocDate")});
            this.lblHeaderChineseDatetime.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderChineseDatetime.LocationFloat = new DevExpress.Utils.PointFloat(311.291F, 47.16F);
            this.lblHeaderChineseDatetime.Multiline = true;
            this.lblHeaderChineseDatetime.Name = "lblHeaderChineseDatetime";
            this.lblHeaderChineseDatetime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderChineseDatetime.SizeF = new System.Drawing.SizeF(449.9948F, 22.08F);
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
            this.lblHeaderOperateDate.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderOperateDate.LocationFloat = new DevExpress.Utils.PointFloat(311.291F, 69.24F);
            this.lblHeaderOperateDate.Multiline = true;
            this.lblHeaderOperateDate.Name = "lblHeaderOperateDate";
            this.lblHeaderOperateDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderOperateDate.SizeF = new System.Drawing.SizeF(449.9948F, 22.08F);
            this.lblHeaderOperateDate.StylePriority.UseFont = false;
            this.lblHeaderOperateDate.StylePriority.UseTextAlignment = false;
            this.lblHeaderOperateDate.Text = "作業日期：0000/00/00";
            this.lblHeaderOperateDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblHeaderOperateDate.TextFormatString = "作業日期：{0:yyyy/MM/dd}";
            // 
            // lblPageDisplay
            // 
            this.lblPageDisplay.Dpi = 96F;
            this.lblPageDisplay.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPageDisplay.LocationFloat = new DevExpress.Utils.PointFloat(903.9999F, 42.99001F);
            this.lblPageDisplay.Multiline = true;
            this.lblPageDisplay.Name = "lblPageDisplay";
            this.lblPageDisplay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblPageDisplay.SizeF = new System.Drawing.SizeF(82.99994F, 22.08001F);
            this.lblPageDisplay.StylePriority.UseFont = false;
            this.lblPageDisplay.StylePriority.UseTextAlignment = false;
            this.lblPageDisplay.Text = "頁　　次：";
            this.lblPageDisplay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblHeaderUserNameDisplay
            // 
            this.lblHeaderUserNameDisplay.Dpi = 96F;
            this.lblHeaderUserNameDisplay.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderUserNameDisplay.LocationFloat = new DevExpress.Utils.PointFloat(903.9999F, 69.23999F);
            this.lblHeaderUserNameDisplay.Multiline = true;
            this.lblHeaderUserNameDisplay.Name = "lblHeaderUserNameDisplay";
            this.lblHeaderUserNameDisplay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderUserNameDisplay.SizeF = new System.Drawing.SizeF(82.99994F, 22.08F);
            this.lblHeaderUserNameDisplay.StylePriority.UseFont = false;
            this.lblHeaderUserNameDisplay.StylePriority.UseTextAlignment = false;
            this.lblHeaderUserNameDisplay.Text = "作業人員：";
            this.lblHeaderUserNameDisplay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Dpi = 96F;
            this.lblHeaderTitle.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?ReportTitle")});
            this.lblHeaderTitle.Font = new System.Drawing.Font("標楷體", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(311.291F, 25.08F);
            this.lblHeaderTitle.Multiline = true;
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblHeaderTitle.SizeF = new System.Drawing.SizeF(449.9948F, 22.08F);
            this.lblHeaderTitle.StylePriority.UseFont = false;
            this.lblHeaderTitle.StylePriority.UseTextAlignment = false;
            this.lblHeaderTitle.Text = "99999–我是標題我是標題(夜盤)";
            this.lblHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // pageInfoMain
            // 
            this.pageInfoMain.Dpi = 96F;
            this.pageInfoMain.LocationFloat = new DevExpress.Utils.PointFloat(987F, 42.99001F);
            this.pageInfoMain.Name = "pageInfoMain";
            this.pageInfoMain.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.pageInfoMain.SizeF = new System.Drawing.SizeF(96F, 22.08001F);
            this.pageInfoMain.StylePriority.UseTextAlignment = false;
            this.pageInfoMain.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // pictureBoxTitle
            // 
            this.pictureBoxTitle.Dpi = 96F;
            this.pictureBoxTitle.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("pictureBoxTitle.ImageSource"));
            this.pictureBoxTitle.LocationFloat = new DevExpress.Utils.PointFloat(311.291F, 3F);
            this.pictureBoxTitle.Name = "pictureBoxTitle";
            this.pictureBoxTitle.SizeF = new System.Drawing.SizeF(449.9948F, 22.08F);
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
            // ReportCommonLandscapeHeader
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Dpi = 96F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 10);
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
            this.Version = "19.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderUserName;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderReportID;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderReportIDDisplay;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderOperateTimeDisplay;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderOperateTime;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderChineseDatetime;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderOperateDate;
        private DevExpress.XtraReports.UI.XRLabel lblPageDisplay;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderUserNameDisplay;
        private DevExpress.XtraReports.UI.XRLabel lblHeaderTitle;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfoMain;
        private DevExpress.XtraReports.UI.XRPictureBox pictureBoxTitle;
        private DevExpress.XtraReports.Parameters.Parameter OcfRocDate;
        private DevExpress.XtraReports.Parameters.Parameter UserName;
        private DevExpress.XtraReports.Parameters.Parameter ReportID;
        private DevExpress.XtraReports.Parameters.Parameter ReportTitle;
    }
}
