using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using CrossModel;
using System.IO;
using TradeUtility;
using CrossModel.Enum;
using TradeFutNight.Common;

namespace TradeFutNight.Reports
{
    public partial class ReportCommonPortraitBase : DevExpress.XtraReports.UI.XtraReport
    {
        public GroupHeaderBand GetGroupHeaderColumns()
        {
            return this.GroupHeaderColumns;
        }

        public DetailBand GetDetailBand()
        {
            return this.Detail;
        }

        public XRTableCell GetFooterMemo()
        {
            return this.tableCellMemo;
        }

        public XRTableRow GetFooterRowMemo()
        {
            return this.tableRowMemo;
        }

        public string FooterMemoText
        {
            set
            {
                this.tableCellMemo.Text = value;
            }
        }

        public bool FooterMemoVisible
        {
            set
            {
                this.tableRowMemo.Visible = value;
            }
        }

        public bool ReportFooterVisible
        {
            set
            {
                this.ReportFooter.Visible = value;
            }
        }

        public bool HasHandlePerson
        {
            set
            {
                this.tableCellHandlePerson.Visible = value;
            }
        }

        public bool HasConfirmPerson
        {
            set
            {
                this.tableCellConfirmPerson.Visible = value;
            }
        }

        public bool HasManagerPerson
        {
            set
            {
                this.tableCellManagerPerson.Visible = value;
            }
        }

        public ReportCommonPortraitBase()
        {
            InitializeComponent();
        }
    }

    public partial class ReportCommonPortrait : ReportCommonPortraitBase
    {
        public string DefaultPdfFilePath { get; set; }

        public ReportCommonPortrait(ReportSetting rptSetting) : base()
        {
            DefaultPdfFilePath = Path.Combine(rptSetting.ReportDirectory, MagicalHats.UniformFileName(rptSetting.SystemType, rptSetting.ReportID, "", FileType.Pdf));

            Parameter paramOcfDate = Parameters["OcfRocDate"];
            paramOcfDate.Visible = false;
            paramOcfDate.Value = "中華民國 " + (rptSetting.OcfDate.Year - 1911) + " 年 " + rptSetting.OcfDate.ToString("MM 月 dd 日 ");

            Parameter paramReportID = Parameters["ReportID"];
            paramReportID.Visible = false;
            paramReportID.Value = rptSetting.SysShortAliasID + rptSetting.ReportID;

            Parameter paramReportTitle = Parameters["ReportTitle"];
            paramReportTitle.Visible = false;
            paramReportTitle.Value = rptSetting.ReportTitle + rptSetting.SysDayOrNightText;

            Parameter paramUserName = Parameters["UserName"];
            paramUserName.Visible = false;
            paramUserName.Value = rptSetting.UserName;

            // 有沒有註解要顯示在Footer裡面
            if (!string.IsNullOrWhiteSpace(rptSetting.FooterMemoText))
            {
                FooterMemoVisible = true;
                FooterMemoText = rptSetting.FooterMemoText;
            }
            else
            {
                FooterMemoVisible = false;
                ReportFooterVisible = false;
            }

            // 註解的字體大小
            GetFooterMemo().Font = new Font(rptSetting.FooterMemoFontName, rptSetting.FooterMemoFontSize);

            // 有沒有要顯示經辦，複核，主管，的簽核欄位
            HasHandlePerson = rptSetting.HasHandlePerson;
            HasConfirmPerson = rptSetting.HasConfirmPerson;
            HasManagerPerson = rptSetting.HasManagerPerson;

            GetGroupHeaderColumns().Controls.Add(ReportNormal.CreateHeaderColumnsTable(rptSetting, rptSetting.ViewModel.DataGridColumns));
            GetDetailBand().Controls.Add(ReportNormal.CreateContentTable(this, rptSetting, rptSetting.ViewModel.DataGridColumns));

            this.DataSource = rptSetting.ViewModel.MainData;
        }

        public string ExportToPdf()
        {
            this.ExportToPdf(DefaultPdfFilePath);

            return DefaultPdfFilePath;
        }
    }
}