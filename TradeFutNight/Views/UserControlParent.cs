using CrossModel;
using CrossModel.Enum;
using DataEngine;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using TradeFutNight.Common;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views
{
    public partial class UserControlParent : UserControl
    {
        protected MainUI_ViewModel VmMainUi { get; set; }
        protected MainUI MainUi { get; set; }

        public string ProgramID { get; set; }
        public string ProgramName { get; set; }

        public string UserID { get { return MagicalHats.UserID; } set { } }

        public string UserName { get { return MagicalHats.UserName; } set { } }

        public OCF Ocf
        {
            get
            {
                bool isInDesignMode = false;

                Dispatcher.Invoke(() =>
                {
                    isInDesignMode = DesignerProperties.GetIsInDesignMode(this);
                });

                // Designer設計介面的時候不要取值，因為會出現xaml設計畫面上的顯示錯誤
                if (isInDesignMode)
                {
                    return null;
                }

                return MagicalHats.Ocf;
            }
            set { }
        }

        public string ExportFilePath
        {
            get
            {
                bool isInDesignMode = false;

                Dispatcher.Invoke(() =>
                {
                    isInDesignMode = DesignerProperties.GetIsInDesignMode(this);
                });

                // Designer設計介面的時候不要取值，因為會出現xaml設計畫面上的顯示錯誤
                if (isInDesignMode)
                {
                    return null;
                }

                return Path.Combine(AppSettings.LocalReportDirectory, MagicalHats.UniformFileName(AppSettings.SystemType, ProgramID, "", FileType.Pdf));
            }
            set { }
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
            {
                return false;
            }
            // 如果JSW只有一筆的話，就看那筆可不可以執行
            else if (countJSW == 1)
            {
                JSW oJSW = listJSW.Single();

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

        public virtual void ControlSetting()
        {
            // 先重置所有控制項
            VmMainUi.IsButtonSaveEnabled = false;
            VmMainUi.IsButtonInsertEnabled = false;
            VmMainUi.IsButtonDeleteEnabled = false;
            VmMainUi.IsButtonExportEnabled = false;
            VmMainUi.IsButtonPrintEnabled = false;
            VmMainUi.IsButtonPrintIndexEnabled = false;
            VmMainUi.IsButtonPrintStockEnabled = false;

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
            {
                return;
            }
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
            }
            // 如果有2筆以上的話，就看是不是全部Y或是全部N，全部Y就是可執行，全部N就是不可執行
            // 如果有Y也有N也是可執行(這種通常是有Q的這種，Q打開其他沒開的話就是可以查詢但不能刪除或儲存)
            else
            {
                foreach (var item in listJSW)
                {
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
                                    VmMainUi.IsButtonPrintEnabled = true;
                                }
                                else
                                {
                                    VmMainUi.IsButtonSaveEnabled = false;
                                    VmMainUi.IsButtonDeleteEnabled = false;
                                    VmMainUi.IsButtonPrintEnabled = true;
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
            }
        }

        public void UpdateAccessPermission(string programID, DalSession das)
        {
            var dJSW = new D_JSW(das);
            dJSW.UpdateJswByJrf(programID);
        }

        public void DbLog(string messageContent, DalSession das)
        {
            new D_LOGF(das).Insert(UserID, ProgramID, messageContent);
        }

        public void DbLog(string messageContent)
        {
            using (var das = new DalSession())
            {
                var dLogf = new D_LOGF(das);
                dLogf.Insert(UserID, ProgramID, messageContent);
            }
        }

        public bool CheckNotNullNotEmpty<T>(GridControl grid, ViewModelParent<T> vm)
        {
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                int rowHandle = grid.GetRowHandleByVisibleIndex(i);
                int listIndex = grid.GetListIndexByRowHandle(rowHandle);
                var item = vm.MainGridData[listIndex];

                foreach (PropertyInfo prop in item.GetType().GetProperties())
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    var colName = prop.Name;
                    var col = grid.Columns[colName];
                    if (col != null)
                    {
                        if (CustomProp.GetNotNullNotEmpty(col))
                        {
                            if (prop.GetValue(item) == null || prop.GetValue(item).ToString().Trim() == "" || prop.GetValue(item).ToString().Trim() == "\0")
                            {
                                vm.SetCurrentAndSelectedItem(item);
                                MessageBoxExService.Instance().Error($"{col.Header}不允許空值");
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        protected bool BaseCheck<T>(CheckSettings settings, GridControl gridControl, ViewModelParent<T> vm)
        {
            VmMainUi.LoadingText = MessageConst.LoadingStatusChecking;

            gridControl.View.CloseEditor();

            if (!gridControl.View.CommitEditing())
            {
                MessageBoxExService.Instance().Error(MessageConst.IsNotValidData);
                return false;
            }

            if (settings.IsCheckNotNullNotEmpty)
            {
                if (!CheckNotNullNotEmpty(gridControl, vm)) return false;
            }

            return true;
        }

        public void CloseWindow()
        {
            // 只把畫面上的程式畫面關掉
            MainUi.CloseWindow();

            // 如果要關閉整個程式的話
            //Application.Current.Dispatcher.Invoke(() => { Application.Current.Shutdown(886); });
        }

        /// <summary>
        /// DevExpress的GridView用的改變格子資料後的觸發事件
        /// </summary>
        protected void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = ((GridViewBase)sender);
            view.CommitEditing();
            var gridControl = ((GridControl)(((GridViewBase)sender).Parent));

            // 如果有加ModifyMark這個欄位的話，會給它一個自訂符號的註記
            var col = gridControl.Columns.GetColumnByFieldName("ModifyMark");
            if (col != null)
            {
                string modifyMarkStyle = CustomProp.GetModifyMarkStyle(col);

                view.CellValueChanged -= new CellValueChangedEventHandler(view_CellValueChanged);
                gridControl.SetCellValue(e.RowHandle, col, modifyMarkStyle);
                view.CellValueChanged += new CellValueChangedEventHandler(view_CellValueChanged);
            }
        }
    }
}