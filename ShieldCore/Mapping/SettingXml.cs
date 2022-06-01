using Shield.File;
using System.Xml.Serialization;

namespace Shield.Mapping
{
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Setting
    {
        public SettingAuth Auth { get; set; }

        public SettingDatabase Database { get; set; }

        public SettingDatabaseInfo Download { get; set; }

        public string System { get; set; }

        [XmlArrayItem("Connection", IsNullable = false)]
        public SettingConnection[] ConnectionStrings { get; set; }

        [XmlArrayItem("Item", IsNullable = false)]
        public SettingDatabaseInfo[] Uploads { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SettingAuth
    {
        public string AdDomain { get; set; }

        /// <summary>
        /// 這個屬性沒有對應到XML，給值是在程式裡給值
        /// </summary>
        public string DomainUrl { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SettingDatabase
    {
        public SettingDatabaseInfo CiUserAp { get; set; }

        public SettingDatabaseInfo Ci { get; set; }

        public SettingDatabaseInfo CiFut { get; set; }

        public SettingDatabaseInfo CiOpt { get; set; }

        public SettingDatabaseInfo CiFutAH { get; set; }

        public SettingDatabaseInfo CiOptAH { get; set; }

        public SettingDatabaseInfo CiMonit { get; set; }

        public SettingDatabaseInfo Futures { get; set; }

        public SettingDatabaseInfo Options { get; set; }

        public SettingDatabaseInfo Futures_AH { get; set; }

        public SettingDatabaseInfo Options_AH { get; set; }

        public SettingDatabaseInfo Tfxm { get; set; }

        public SettingDatabaseInfo Tfxm_AH { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SettingDatabaseInfo
    {
        public string ConnectionName { get; set; }

        public string InitialCatalog { get; set; }

        /// <summary>
        /// 這個屬性沒有對應到XML，給值是在程式裡給值
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 這個屬性沒有對應到XML，給值是在程式裡給值
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 這個屬性沒有對應到XML，給值是在程式裡給值
        /// </summary>
        public string EncryptPassword { get; set; }

        public string ProgramBeOpenedAfterDownload { get; set; }

        public string DisplayName { get; set; }

        public string SystemName { get; set; }

        public string Type { get; set; }

        public string Filters { get; set; }

        public static SettingDatabaseInfo FutDay
        {
            get
            {
                return SettingFile.Database.Futures;
            }
        }

        public static SettingDatabaseInfo FutNight
        {
            get
            {
                return SettingFile.Database.Futures_AH;
            }
        }

        public static SettingDatabaseInfo OptDay
        {
            get
            {
                return SettingFile.Database.Options;
            }
        }

        public static SettingDatabaseInfo OptNight
        {
            get
            {
                return SettingFile.Database.Options_AH;
            }
        }

        public static SettingDatabaseInfo TfxmDay
        {
            get
            {
                return SettingFile.Database.Tfxm;
            }
        }

        public static SettingDatabaseInfo TfxmNight
        {
            get
            {
                return SettingFile.Database.Tfxm_AH;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SettingConnection
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public string ProviderName { get; set; }

        public string EncryptPassword { get; set; }
    }
}