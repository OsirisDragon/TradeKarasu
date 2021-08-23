using CrossModel;
using CrossModel.Enum;
using DataEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradeFutNight.Common;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views
{
    public partial class UserControlParent : UserControl
    {
        private string _exportFilePath;

        protected MainUI_ViewModel VmMainUi { get; set; }
        protected MainUI MainUi { get; set; }

        public string ProgramID { get; set; }
        public string ProgramName { get; set; }

        public string UserID { get { return MagicalHats.UserID; } set { } }

        public string UserName { get { return MagicalHats.UserName; } set { } }

        public OCF Ocf { get { return MagicalHats.Ocf; } set { } }

        public string ExportFilePath
        {
            get { return _exportFilePath; }
            set { _exportFilePath = value; }
        }

        public UserControlParent()
        {
        }

        public void Init(string programID, string programName, MainUI_ViewModel vmMainUi, MainUI mainUi)
        {
            ProgramID = programID;
            ProgramName = programName;
            VmMainUi = vmMainUi;
            MainUi = mainUi;

            ExportFilePath = Path.Combine(AppSettings.LocalReportDirectory, MagicalHats.UniformFileName(AppSettings.SystemType, ProgramID, "", FileType.Pdf)); ;
        }

        public bool IsCanRunProgram()
        {
            IEnumerable<JSW> listJSW;
            int countJSW = 0;

            using (var das = new DalSession())
            {
                var dJSW = new D_JSW(das);
                listJSW = dJSW.ListByID(ProgramID);
                countJSW = listJSW.Count();
            }

            // 如果都沒有JSW的話，就讓程式不可執行
            if (countJSW == 0)
                return false;

            // 如果JSW只有一筆的話，就看那筆可不可以執行
            else if (countJSW == 1)
            {
                JSW oJSW = listJSW.Single();

                #region 設定個別的Button是否可按

                switch (oJSW.JSW_TYPE)
                {
                    case 'I':
                        VmMainUi.IsButtonSaveEnabled = true;
                        VmMainUi.IsButtonInsertEnabled = true;
                        VmMainUi.IsButtonDeleteEnabled = true;
                        VmMainUi.IsButtonPrintEnabled = false;
                        break;

                    case 'U':
                        VmMainUi.IsButtonSaveEnabled = true;
                        break;
                }

                #endregion 設定個別的Button是否可按

                if (oJSW.JSW_SW_CODE == 'Y')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // 如果有2筆以上的話，就看是不是全部Y或是全部N，全部Y就是可執行，全部N就是不可執行
            // 如果有Y也有N也是可執行(這種通常是有Q的這種，Q打開其他沒開的話就是可以查詢但不能刪除或儲存)
            else
            {
                bool isAllRecordSame = true;

                char? preValue = ' ';

                foreach (var item in listJSW)
                {
                    if (item.JSW_SW_CODE != preValue && preValue != ' ')
                    {
                        isAllRecordSame = false;
                    }

                    preValue = item.JSW_SW_CODE;

                    #region 設定個別的Button是否可按

                    // 如果只有兩筆，就是D或Q這種
                    if (countJSW == 2)
                    {
                        switch (item.JSW_TYPE)
                        {
                            case 'D':
                                if (item.JSW_SW_CODE == 'Y')
                                {
                                    VmMainUi.IsButtonSaveEnabled = true;
                                    VmMainUi.IsButtonDeleteEnabled = true;
                                }
                                else
                                {
                                    VmMainUi.IsButtonSaveEnabled = false;
                                    VmMainUi.IsButtonDeleteEnabled = false;
                                }

                                break;
                        }
                    }
                    // 如果大於兩筆以上，就是DIQU這種
                    else
                    {
                        switch (item.JSW_TYPE)
                        {
                            case 'D':
                            case 'I':
                            case 'U':
                                if (item.JSW_SW_CODE == 'Y')
                                {
                                    VmMainUi.IsButtonSaveEnabled = true;
                                    VmMainUi.IsButtonDeleteEnabled = true;
                                    VmMainUi.IsButtonInsertEnabled = true;
                                }
                                else
                                {
                                    VmMainUi.IsButtonSaveEnabled = false;
                                    VmMainUi.IsButtonDeleteEnabled = false;
                                    VmMainUi.IsButtonInsertEnabled = false;
                                }

                                break;
                        }
                    }

                    #endregion 設定個別的Button是否可按
                }

                if (isAllRecordSame)
                {
                    if (listJSW.First().JSW_SW_CODE == 'Y')
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public void UpdateAccessPermission(string programID, DalSession das)
        {
            var dJSW = new D_JSW(das);
            dJSW.UpdateJswByJrf(programID);
        }

        public void DbLog(string programID, string userID, string messageContent, DalSession das)
        {
            new D_LOGF(das).Insert(UserID, programID, messageContent);
        }

        public void CloseWindow()
        {
            // 只把畫面上的程式畫面關掉
            MainUi.CloseWindow();

            // 如果要關閉整個程式的話
            //Application.Current.Dispatcher.Invoke(() => { Application.Current.Shutdown(886); });
        }
    }
}