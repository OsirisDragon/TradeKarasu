using CrossModel.Enum;

namespace CrossModel
{
    public static class AppSettings
    {
        public static string Secret { get; set; }

        public static SystemType SystemType { get; set; }

        public static string SysShortAliasID { get; set; }

        public static string SysDayOrNightText { get; set; }

        public static string LocalReportDirectoryWithoutDate { get; set; }

        public static string LocalReportDirectory { get; set; }

        public static string LocalRoutineDataDirectory { get; set; }

        public static string DashForTitle { get; set; }
    }
}