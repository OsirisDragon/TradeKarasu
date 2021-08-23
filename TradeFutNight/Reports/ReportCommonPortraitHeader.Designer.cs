namespace TradeFutNight.Reports
{
    partial class ReportCommonPortraitHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportCommonPortraitHeader));
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
            this.ReportTitle = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportID = new DevExpress.XtraReports.Parameters.Parameter();
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
            this.Detail.Name = "Detail";
            // 
            // lblHeaderUserName
            // 
            this.lblHeaderUserName.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?UserName")});
            this.lblHeaderUserName.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderUserName.LocationFloat = new DevExpress.Utils.PointFloat(719.7863F, 72.12499F);
            this.lblHeaderUserName.Multiline = true;
            this.lblHeaderUserName.Name = "lblHeaderUserName";
            this.lblHeaderUserName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderUserName.SizeF = new System.Drawing.SizeF(86.46368F, 23F);
            this.lblHeaderUserName.StylePriority.UseFont = false;
            this.lblHeaderUserName.StylePriority.UseTextAlignment = false;
            this.lblHeaderUserName.Text = "測試人員";
            this.lblHeaderUserName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblHeaderReportID
            // 
            this.lblHeaderReportID.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?ReportID")});
            this.lblHeaderReportID.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderReportID.LocationFloat = new DevExpress.Utils.PointFloat(76.34468F, 44.78126F);
            this.lblHeaderReportID.Multiline = true;
            this.lblHeaderReportID.Name = "lblHeaderReportID";
            this.lblHeaderReportID.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderReportID.SizeF = new System.Drawing.SizeF(78.125F, 23F);
            this.lblHeaderReportID.StylePriority.UseFont = false;
            this.lblHeaderReportID.StylePriority.UseTextAlignment = false;
            this.lblHeaderReportID.Text = "F00000";
            this.lblHeaderReportID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblHeaderReportIDDisplay
            // 
            this.lblHeaderReportIDDisplay.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderReportIDDisplay.LocationFloat = new DevExpress.Utils.PointFloat(4.166667F, 44.78126F);
            this.lblHeaderReportIDDisplay.Multiline = true;
            this.lblHeaderReportIDDisplay.Name = "lblHeaderReportIDDisplay";
            this.lblHeaderReportIDDisplay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderReportIDDisplay.SizeF = new System.Drawing.SizeF(71.875F, 23F);
            this.lblHeaderReportIDDisplay.StylePriority.UseFont = false;
            this.lblHeaderReportIDDisplay.StylePriority.UseTextAlignment = false;
            this.lblHeaderReportIDDisplay.Text = "報表代號：";
            this.lblHeaderReportIDDisplay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblHeaderOperateTimeDisplay
            // 
            this.lblHeaderOperateTimeDisplay.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderOperateTimeDisplay.LocationFloat = new DevExpress.Utils.PointFloat(4.166667F, 72.12499F);
            this.lblHeaderOperateTimeDisplay.Multiline = true;
            this.lblHeaderOperateTimeDisplay.Name = "lblHeaderOperateTimeDisplay";
            this.lblHeaderOperateTimeDisplay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderOperateTimeDisplay.SizeF = new System.Drawing.SizeF(71.875F, 23F);
            this.lblHeaderOperateTimeDisplay.StylePriority.UseFont = false;
            this.lblHeaderOperateTimeDisplay.StylePriority.UseTextAlignment = false;
            this.lblHeaderOperateTimeDisplay.Text = "作業時間：";
            this.lblHeaderOperateTimeDisplay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblHeaderOperateTime
            // 
            this.lblHeaderOperateTime.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Now()")});
            this.lblHeaderOperateTime.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderOperateTime.LocationFloat = new DevExpress.Utils.PointFloat(76.34468F, 72.12499F);
            this.lblHeaderOperateTime.Multiline = true;
            this.lblHeaderOperateTime.Name = "lblHeaderOperateTime";
            this.lblHeaderOperateTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderOperateTime.SizeF = new System.Drawing.SizeF(77.82199F, 23F);
            this.lblHeaderOperateTime.StylePriority.UseFont = false;
            this.lblHeaderOperateTime.StylePriority.UseTextAlignment = false;
            this.lblHeaderOperateTime.Text = "00:00:00";
            this.lblHeaderOperateTime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblHeaderOperateTime.TextFormatString = "{0:HH:mm:ss}";
            // 
            // lblHeaderChineseDatetime
            // 
            this.lblHeaderChineseDatetime.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?OcfRocDate")});
            this.lblHeaderChineseDatetime.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderChineseDatetime.LocationFloat = new DevExpress.Utils.PointFloat(167.7083F, 49.125F);
            this.lblHeaderChineseDatetime.Multiline = true;
            this.lblHeaderChineseDatetime.Name = "lblHeaderChineseDatetime";
            this.lblHeaderChineseDatetime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderChineseDatetime.SizeF = new System.Drawing.SizeF(468.7446F, 23F);
            this.lblHeaderChineseDatetime.StylePriority.UseFont = false;
            this.lblHeaderChineseDatetime.StylePriority.UseTextAlignment = false;
            this.lblHeaderChineseDatetime.Text = "中華民國000年00月00日";
            this.lblHeaderChineseDatetime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblHeaderOperateDate
            // 
            this.lblHeaderOperateDate.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "GetDate(Today())")});
            this.lblHeaderOperateDate.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderOperateDate.LocationFloat = new DevExpress.Utils.PointFloat(167.7083F, 72.125F);
            this.lblHeaderOperateDate.Multiline = true;
            this.lblHeaderOperateDate.Name = "lblHeaderOperateDate";
            this.lblHeaderOperateDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderOperateDate.SizeF = new System.Drawing.SizeF(468.7446F, 23F);
            this.lblHeaderOperateDate.StylePriority.UseFont = false;
            this.lblHeaderOperateDate.StylePriority.UseTextAlignment = false;
            this.lblHeaderOperateDate.Text = "作業日期：0000/00/00";
            this.lblHeaderOperateDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblHeaderOperateDate.TextFormatString = "作業日期：{0:yyyy/MM/dd}";
            // 
            // lblPageDisplay
            // 
            this.lblPageDisplay.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPageDisplay.LocationFloat = new DevExpress.Utils.PointFloat(645.8279F, 44.78126F);
            this.lblPageDisplay.Multiline = true;
            this.lblPageDisplay.Name = "lblPageDisplay";
            this.lblPageDisplay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPageDisplay.SizeF = new System.Drawing.SizeF(71.875F, 23F);
            this.lblPageDisplay.StylePriority.UseFont = false;
            this.lblPageDisplay.StylePriority.UseTextAlignment = false;
            this.lblPageDisplay.Text = "頁　　次：";
            this.lblPageDisplay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblHeaderUserNameDisplay
            // 
            this.lblHeaderUserNameDisplay.Font = new System.Drawing.Font("標楷體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderUserNameDisplay.LocationFloat = new DevExpress.Utils.PointFloat(645.8279F, 72.12499F);
            this.lblHeaderUserNameDisplay.Multiline = true;
            this.lblHeaderUserNameDisplay.Name = "lblHeaderUserNameDisplay";
            this.lblHeaderUserNameDisplay.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderUserNameDisplay.SizeF = new System.Drawing.SizeF(71.875F, 23F);
            this.lblHeaderUserNameDisplay.StylePriority.UseFont = false;
            this.lblHeaderUserNameDisplay.StylePriority.UseTextAlignment = false;
            this.lblHeaderUserNameDisplay.Text = "作業人員：";
            this.lblHeaderUserNameDisplay.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?ReportTitle")});
            this.lblHeaderTitle.Font = new System.Drawing.Font("標楷體", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHeaderTitle.LocationFloat = new DevExpress.Utils.PointFloat(167.7083F, 26.125F);
            this.lblHeaderTitle.Multiline = true;
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblHeaderTitle.SizeF = new System.Drawing.SizeF(468.7446F, 23F);
            this.lblHeaderTitle.StylePriority.UseFont = false;
            this.lblHeaderTitle.StylePriority.UseTextAlignment = false;
            this.lblHeaderTitle.Text = "99999–我是標題我是標題(夜盤)";
            this.lblHeaderTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // pageInfoMain
            // 
            this.pageInfoMain.LocationFloat = new DevExpress.Utils.PointFloat(719.7863F, 44.78126F);
            this.pageInfoMain.Name = "pageInfoMain";
            this.pageInfoMain.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.pageInfoMain.SizeF = new System.Drawing.SizeF(86.46368F, 23F);
            this.pageInfoMain.StylePriority.UseTextAlignment = false;
            this.pageInfoMain.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // pictureBoxTitle
            // 
            this.pictureBoxTitle.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("pictureBoxTitle.ImageSource"));
            this.pictureBoxTitle.LocationFloat = new DevExpress.Utils.PointFloat(167.7083F, 3.125F);
            this.pictureBoxTitle.Name = "pictureBoxTitle";
            this.pictureBoxTitle.SizeF = new System.Drawing.SizeF(468.7446F, 23F);
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
            // ReportTitle
            // 
            this.ReportTitle.AllowNull = true;
            this.ReportTitle.Description = "ReportTitle";
            this.ReportTitle.Name = "ReportTitle";
            this.ReportTitle.Visible = false;
            // 
            // ReportID
            // 
            this.ReportID.AllowNull = true;
            this.ReportID.Description = "ReportID";
            this.ReportID.Name = "ReportID";
            this.ReportID.Visible = false;
            // 
            // ReportCommonPortraitHeader
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 10);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.OcfRocDate,
            this.UserName,
            this.ReportTitle,
            this.ReportID});
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
        private DevExpress.XtraReports.Parameters.Parameter ReportTitle;
        private DevExpress.XtraReports.Parameters.Parameter ReportID;
    }
}
