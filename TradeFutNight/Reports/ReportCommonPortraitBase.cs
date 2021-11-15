﻿using CrossModel;
using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

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

    public partial class ReportCommonPortrait<T> : ReportCommonPortraitBase
    {
        public string DefaultPdfFilePath { get; set; }

        public ReportCommonPortrait(IList<T> exportData, GridColumnCollection columns, ReportSetting rptSetting) : base()
        {
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
            }

            // 註解的字體大小
            GetFooterMemo().Font = new Font(rptSetting.FooterMemoFontName, rptSetting.FooterMemoFontSize);

            // 有沒有要顯示經辦，複核，主管，的簽核欄位
            HasHandlePerson = rptSetting.HasHandlePerson;
            HasConfirmPerson = rptSetting.HasConfirmPerson;
            HasManagerPerson = rptSetting.HasManagerPerson;

            Application.Current.Dispatcher.Invoke(() =>
            {
                GetGroupHeaderColumns().Controls.Add(ReportNormal.CreateHeaderColumnsTable(rptSetting, columns));

                // 如果有資料才要產生內容
                if (exportData.Count > 0)
                    GetDetailBand().Controls.Add(ReportNormal.CreateContentTable(exportData, rptSetting, columns));
            });

            this.DataSource = exportData;
        }
    }
}