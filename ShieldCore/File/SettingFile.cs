using Shield.Crypto;
using Shield.IO;
using Shield.Mapping;
using System;
using System.IO;

namespace Shield.File
{
    public class SettingFile
    {
        private static Setting _Setting;

        public static Setting Setting
        {
            get
            {
                if (_Setting == null)
                {
                    ReadFile();
                }
                return _Setting;
            }
            set
            {
            }
        }

        public static SettingAuth Auth
        {
            get
            {
                return Setting.Auth;
            }
        }

        /// <summary>
        /// 可以更快速的叫用此功能
        /// </summary>
        public static SettingDatabase Database
        {
            get
            {
                return Setting.Database;
            }
        }

        public static string FilePath { get; set; }

        /// <summary>
        /// 讀設定檔
        /// </summary>
        private static void ReadFile()
        {
            try
            {
                string path = FilePath;

                if (String.IsNullOrEmpty(FilePath))
                {
                    path = Directory.GetCurrentDirectory() + @"\Setting.xml";
                }

                _Setting = SDerializer.XmlToObject<Setting>(path);

                if (Setting.Auth != null)
                {
                    if (Setting.Auth != null)
                        GetConnectionInfo(Setting.Auth);
                }

                if (Setting.Database != null)
                {
                    if (Setting.Database.Ci != null)
                        GetConnectionInfo(Setting.Database.Ci);

                    if (Setting.Database.CiFut != null)
                        GetConnectionInfo(Setting.Database.CiFut);

                    if (Setting.Database.CiFutAH != null)
                        GetConnectionInfo(Setting.Database.CiFutAH);

                    if (Setting.Database.CiMonit != null)
                        GetConnectionInfo(Setting.Database.CiMonit);

                    if (Setting.Database.CiOpt != null)
                        GetConnectionInfo(Setting.Database.CiOpt);

                    if (Setting.Database.CiOptAH != null)
                        GetConnectionInfo(Setting.Database.CiOptAH);

                    if (Setting.Database.CiUserAp != null)
                        GetConnectionInfo(Setting.Database.CiUserAp);

                    if (Setting.Database.Futures != null)
                        GetConnectionInfo(Setting.Database.Futures);

                    if (Setting.Database.Options != null)
                        GetConnectionInfo(Setting.Database.Options);

                    if (Setting.Database.Futures_AH != null)
                        GetConnectionInfo(Setting.Database.Futures_AH);

                    if (Setting.Database.Options_AH != null)
                        GetConnectionInfo(Setting.Database.Options_AH);

                    if (Setting.Database.Tfxm != null)
                        GetConnectionInfo(Setting.Database.Tfxm);

                    if (Setting.Database.Tfxm_AH != null)
                        GetConnectionInfo(Setting.Database.Tfxm_AH);
                }

                if (Setting.Download != null)
                {
                    GetConnectionInfo(Setting.Download);
                }

                if (Setting.Uploads != null)
                {
                    foreach (SettingDatabaseInfo info in Setting.Uploads)
                    {
                        GetConnectionInfo(info);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 取出連線資串相關資訊
        /// </summary>
        /// <param name="connectionName">連線名稱</param>
        private static SettingConnection GetConnectionString(string connectionName)
        {
            SettingConnection[] list = (SettingConnection[])Setting.GetType().GetProperty("ConnectionStrings").GetValue(Setting, null);
            SettingConnection conn = Array.Find<SettingConnection>(list, x => x.Name == connectionName);
            return conn;
        }

        private static void GetConnectionInfo(SettingAuth settingAuth)
        {
            SettingConnection connection = GetConnectionString(settingAuth.AdDomain);
            if (connection == null)
            {
                throw new Exception("在Setting.xml裡面找不到Connection為" + settingAuth.AdDomain + "的設定");
            }

            settingAuth.DomainUrl = connection.ConnectionString;
        }

        private static void GetConnectionInfo(SettingDatabaseInfo dbInfo)
        {
            try
            {
                string originConnectionString;

                SettingConnection connection = GetConnectionString(dbInfo.ConnectionName);
                if (connection == null)
                {
                    throw new Exception("在Setting.xml裡面找不到Connection為" + dbInfo.ConnectionName + "的設定");
                }

                originConnectionString = connection.ConnectionString;
                dbInfo.ConnectionString = originConnectionString;
                dbInfo.ProviderName = connection.ProviderName;
                dbInfo.EncryptPassword = connection.EncryptPassword;

                // 如果Xml檔案裡面有這個Tag的話且不是空值，就代表要用加密密碼
                if (!string.IsNullOrEmpty(connection.EncryptPassword))
                {
                    string password = StringCipher.Decrypt(connection.EncryptPassword, "InitialD");

                    string[] partOriginConnectStrs = originConnectionString.Split(';');

                    string originPasswordKeyValue = "";
                    string realPasswordKeyValue = "";

                    foreach (var item in partOriginConnectStrs)
                    {
                        if (item.Contains("Password="))
                        {
                            originPasswordKeyValue = item;
                            realPasswordKeyValue = "Password=" + password;
                        }
                        else if (item.Contains("PWD="))
                        {
                            originPasswordKeyValue = item;
                            realPasswordKeyValue = "PWD=" + password;
                        }
                    }

                    dbInfo.ConnectionString = dbInfo.ConnectionString.Replace(originPasswordKeyValue, realPasswordKeyValue);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("密碼錯誤解密失敗", ex);
            }
        }
    }
}