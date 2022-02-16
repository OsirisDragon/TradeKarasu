using CrossModel.Enum;
using DataEngine;
using Shield.File;
using System;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeUtility.AD;

namespace TradeFutNight.Auth
{
    /// <summary>
    /// AuthDoubleWithoutCard.xaml 的互動邏輯
    /// </summary>
    public partial class AuthDoubleWithoutCard : Window
    {
        private string _programID;

        public AuthDoubleWithoutCard(string programID)
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.ShowInTaskbar = false;
            this.txtAdAccount.Focus();
            this._programID = programID;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string adAccount = txtAdAccount.Text;
            string adPassword = txtAdPassword.Password;

            if (adAccount != "...")
            {
                if (adAccount == "")
                {
                    MessageBoxExService.Instance().Error("請輸入AD使用者代號");
                    txtAdAccount.Focus();
                    return;
                }

                MparAuthType mparAuthType;
                using (var das = Factory.CreateDalSession())
                {
                    var mpar = new D_MPAR(das).Get();
                    mparAuthType = (MparAuthType)Convert.ToInt32(mpar.MPAR_AUTH_TYPE.ToString());
                }

                if ((mparAuthType == MparAuthType.AdPassCard || mparAuthType == MparAuthType.AdPass) && adPassword == "")
                {
                    MessageBoxExService.Instance().Error("請輸入AD使用者密碼");
                    txtAdPassword.Focus();
                    return;
                }

                if (adAccount == MagicalHats.UserAD)
                {
                    MessageBoxExService.Instance().Error("請輸入另一組非登入ID");
                    txtAdAccount.Focus();
                    return;
                }

                #region AD認證

                if ((mparAuthType == MparAuthType.AdPassCard || mparAuthType == MparAuthType.AdPass))
                {
                    try
                    {
                        using (var ldapClient = new LdapClient(SettingFile.Auth.DomainUrl, adAccount, adPassword))
                        {
                            ldapClient.AuthUser();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxExService.Instance().Error(ex.Message);
                        return;
                    }
                }

                #endregion AD認證
            }

            // 紀錄LOG
            using (var das = new DalSession())
            {
                var dUpf = new D_UPF(das);
                var upf = dUpf.GetByUserAdAccount(adAccount);

                var dLogf = new D_LOGF(das);
                dLogf.Insert(upf.UPF_USER_ID, _programID, "雙重覆核成功");
            }

            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}