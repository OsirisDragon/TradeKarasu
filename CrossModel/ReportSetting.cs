using CrossModel.Enum;
using System;

namespace CrossModel
{
    public class ReportSetting
    {
        public string HeaderColumnsFontName = "標楷體";
        public float HeaderColumnsFontSize = 11;
        public float HeaderColumnsRowHeight = 40f;
        public float HeaderColumnsWidthScaleFactor = 1f;

        public string ContentColumnsFontName = "標楷體";
        public float ContentColumnsFontSize = 11;
        public float ContentColumnsWidthScaleFactor = 1f;

        public string FooterMemoFontName = "標楷體";
        public float FooterMemoFontSize = 11;

        public DateTime OcfDate { get; set; }

        public string ReportID { get; set; }

        public string ReportTitle { get; set; }

        public string UserName { get; set; }

        public string SysShortAliasID { get; set; }

        public string SysDayOrNightText { get; set; }

        public SystemType SystemType { get; set; }

        public string HeaderMemoText { get; set; }

        public string FooterMemoText { get; set; }

        public Align HeaderMemoTextAlign { get; set; }

        public bool HasHandlePerson { get; set; }

        public bool HasConfirmPerson { get; set; }

        public bool HasManagerPerson { get; set; }

        public ReportSetting()
        {
        }

        public ReportSetting(string programID, string reportTitle, string userName, string memoText, DateTime ocfDate, bool hasHandlePerson, bool hasConfirmPerson, bool hasManagerPerson)
        {
            this.OcfDate = ocfDate;
            this.ReportID = programID;
            this.ReportTitle = reportTitle;
            this.UserName = userName;
            this.SysShortAliasID = AppSettings.SysShortAliasID;
            this.SysDayOrNightText = AppSettings.SysDayOrNightText;
            this.SystemType = AppSettings.SystemType;
            this.FooterMemoText = memoText;
            this.HasHandlePerson = hasHandlePerson;
            this.HasConfirmPerson = hasConfirmPerson;
            this.HasManagerPerson = hasManagerPerson;
        }
    }
}