using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.PrefixA
{
    /// <summary>
    /// U_A9920.xaml 的互動邏輯
    /// </summary>
    public partial class U_A9920 : UserControlParent, IViewSword
    {
        private U_A9920_ViewModel _vm;

        public U_A9920()
        {
            InitializeComponent();
            _vm = (U_A9920_ViewModel)DataContext;
        }

        public void InitialSetting(string programID, string programName, MainUI_ViewModel vmMainUi, MainUI mainUi)
        {
            base.Init(programID, programName, vmMainUi, mainUi);
        }

        public async Task<bool> IsCanRun()
        {
            var task = Task.Run(() =>
            {
                var isCanRun = IsCanRunProgram();
                MagicalHats.LogToDb(UserID, ProgramID, MessageConst.IsCanRun + ":" + isCanRun.ToString().ToUpper());
                return isCanRun;
            });
            await task;

            return task.Result;
        }

        public async Task Open()
        {
            var task = Task.Run(() =>
            {
                _vm.Open();
                MagicalHats.LogToDb(UserID, ProgramID, MessageConst.Open);
            });
            await task;
        }

        public void Insert()
        {
            gridView.CloseEditor();
            _vm.Insert();
        }

        public void Delete()
        {
            bool isNeedConfirm = true;
            var selectedItem = gridMain.SelectedItem;
            if (selectedItem != null)
            {
                if (isNeedConfirm)
                {
                    if (MessageBoxExService.Instance().Confirm(MessageConst.ConfirmDelete) == MessageBoxResult.Yes)
                        _vm.Delete(selectedItem);
                }
                else
                {
                    _vm.Delete(selectedItem);
                }
            }
        }

        public async Task<bool> CheckField()
        {
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = true }, gridMain, _vm))
                return false;

            var task = Task.Run(() =>
            {
                if (!IsCanRunProgram())
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
                    return false;
                }

                var resultItem = new ResultItem();
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();
                var checkItems = trackableData.ChangedItems;

                if (checkItems.Count() == 0)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NoChangedData);
                    return false;
                }

                //foreach (var item in checkItems)
                //{
                //    if (item.TPPADJ_MAX < item.TPPADJ_MIN)
                //    {
                //        resultItem.AppendErrorMessage($"{item.TPPADJ_KIND_ID}的權利金上限不能小於權利金下限");
                //    }

                //    using (var das = Factory.CreateDalSession())
                //    {
                //        var dPDK = new D_PDK(das);
                //        var pdk = dPDK.getByParamKey(item.TPPADJ_KIND_ID);
                //        if (pdk != null && pdk.PDK_SUBTYPE == 'E')
                //        {
                //            if (pdk.PDK_PRICE_FLUC != 'F')
                //            {
                //                resultItem.AppendErrorMessage("匯率類的商品僅可選擇「固定點數」");
                //            }
                //        }
                //    }
                //}

                if (resultItem.HasError)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(resultItem.ErrorMessage);
                    return false;
                }

                return true;
            });
            await task;

            return task.Result;
        }

        public async Task Save()
        {
            VmMainUi.LoadingText = MessageConst.LoadingStatusSaving;

            var task = Task.Run(async () =>
            {
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();
                var domainData = CustomMapper(trackableData.ChangedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        //var dTPPADJ = new D_TPPADJ(das);
                        //dTPPADJ.Update(domainData);

                        UpdateAccessPermission(ProgramID, das);

                        DbLog(ProgramID, UserID, MessageConst.Completed, das);

                        das.Commit();
                    }
                    catch (Exception ex)
                    {
                        das.Rollback();
                        throw ex;
                    }
                }

                var report = CreateReport(domainData, OperationType.Save);
                var reportGate = await new ReportGate(report).CreateDocument();
                await reportGate.ExportPdf(ExportFilePath);
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private IList<TPPADJ> CustomMapper(IEnumerable<UIModel_A9920> items)
        {
            var listResult = new List<TPPADJ>();
            foreach (var item in items)
            {
                var newItem = _vm.MapperInstance.Map<TPPADJ>(item);

                var trackItem = item.CastToIChangeTrackable();
                newItem.OriginalData = trackItem.GetOriginal();
                listResult.Add(newItem);
            }

            return listResult;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string memo = "";
            Dispatcher.Invoke(() =>
            {
                memo = txtMemo.Text;
            });

            string reportTitle = ProgramID + "–" + ProgramName;

            switch (operationType)
            {
                case OperationType.Save:

                    break;

                case OperationType.Print:

                    break;

                default:
                    break;
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, memo, Ocf.OCF_DATE, true, false, true);
            var reportCommon = ReportNormal.CreateCommonLandscape(data, gridMain.Columns, rptSetting);

            return reportCommon;
        }

        public async Task Export()
        {
            gridView.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task Print()
        {
            gridView.CloseEditor();

            var report = CreateReport(_vm.MainGridData, OperationType.Print);
            var reportGate = await new ReportGate(report).CreateDocument();
            await reportGate.ExportPdf(ExportFilePath);
            await reportGate.Print();
        }

        public async Task PrintIndex()
        {
            gridView.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintStock()
        {
            gridView.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        private async void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            //var button = ((Button)sender);
            //button.IsEnabled = false;

            //if (cbKindId.SelectedItem != null)
            //{
            //    var selectedItem = (ItemInfo)cbKindId.SelectedItem;

            //    var task = Task.Run(async () =>
            //    {
            //        await _vm.Query(selectedItem.Value.ToString());
            //    });

            //    await task;
            //}

            //button.IsEnabled = true;
        }

        private void ChkBox_Checked(object sender, RoutedEventArgs e)
        {
            ChangeGridColumnCheckBoxReadOnly((CheckBox)sender, false);
        }

        private void ChkBox_UnChecked(object sender, RoutedEventArgs e)
        {
            ChangeGridColumnCheckBoxReadOnly((CheckBox)sender, true);
        }

        private void ChangeGridColumnCheckBoxReadOnly(CheckBox chkBox, bool isCheck)
        {
            switch (chkBox.Name)
            {
                case "chkBoxTPPADJ_M_PRICE_LIMIT":
                    _vm.IsReadOnlyTPPADJ_M_PRICE_LIMIT = isCheck;
                    break;

                case "chkBoxTPPADJ_M_PRICE_LIMIT_F":
                    _vm.IsReadOnlyTPPADJ_M_PRICE_LIMIT_F = isCheck;
                    break;

                case "chkBoxTPPADJ_M_INTERVAL":
                    _vm.IsReadOnlyTPPADJ_M_INTERVAL = isCheck;
                    break;

                case "chkBoxTPPADJ_ACCU_QNTY":
                    _vm.IsReadOnlyTPPADJ_ACCU_QNTY = isCheck;
                    break;

                case "chkBoxTPPADJ_M_PRICE_FILTER":
                    _vm.IsReadOnlyTPPADJ_M_PRICE_FILTER = isCheck;
                    break;

                case "chkBoxTPPADJ_BS_PRICE_FILTER":
                    _vm.IsReadOnlyTPPADJ_BS_PRICE_FILTER = isCheck;
                    break;

                case "chkBoxTPPADJ_THERICAL_P_REF":
                    _vm.IsReadOnlyTPPADJ_THERICAL_P_REF = isCheck;
                    break;

                case "chkBoxTPPADJ_SPREAD":
                    _vm.IsReadOnlyTPPADJ_SPREAD = isCheck;
                    break;

                default:
                    break;
            }
        }

        private void ComboCategorys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selectedItem = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            comboDetails.Items.Clear();

            IEnumerable<PDK> pdks;
            using (var das = Factory.CreateDalSession())
            {
                pdks = new D_PDK(das).ListNotExpire();
            }

            if (selectedItem == "全部")
            {
            }
            else if (selectedItem == "上市指數")
            {
                pdks = pdks.Where(c => c.PDK_SUBTYPE == 'I' && c.PDK_UNDERLYING_MARKET == '1');
            }
            else if (selectedItem == "上櫃指數")
            {
                pdks = pdks.Where(c => c.PDK_SUBTYPE == 'I' && c.PDK_UNDERLYING_MARKET == '2');
            }
            else if (selectedItem == "國外指數")
            {
                pdks = pdks.Where(c => c.PDK_SUBTYPE == 'I' && (c.PDK_UNDERLYING_MARKET == '3' || c.PDK_UNDERLYING_MARKET == '5'));
            }
            else if (selectedItem == "匯率")
            {
                pdks = pdks.Where(c => c.PDK_SUBTYPE == 'E');
            }
            else if (selectedItem == "ETF")
            {
                pdks = pdks.Where(c => c.PDK_PARAM_KEY == "ETF");
            }

            comboDetails.Items.Add("全部");

            if (selectedItem != "全部")
            {
                foreach (var item in pdks)
                {
                    comboDetails.Items.Add(item.PDK_KIND_ID);
                }
            }

            comboDetails.Text = "全部";
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckHelper(true);
        }

        private void CheckHelper(bool isCheck)
        {
            // 勾或不勾
            string selectedItemCategory, selectedItemDetail;

            selectedItemCategory = ((ComboBoxItem)comboCategorys.SelectedItem).Content.ToString();
            selectedItemDetail = comboDetails.SelectedItem.ToString();

            // 如果第二層選全部，就把第二層的每一個商品組成字串
            if (selectedItemDetail == "全部")
            {
                foreach (ComboBoxItem item in comboDetails.Items)
                {
                    selectedItemDetail += item.Content.ToString();
                }
            }

            bool hasAlreadyScroll = false;

            foreach (var item in _vm.MainGridData)
            {
                // 如果第一層選全部就是全部都要勾或不勾
                // 如果選其他的就判斷PROD_ID有沒有符合
                if (selectedItemCategory == "全部" || selectedItemDetail.IndexOf(item.TPPADJ_PROD_ID.Substring(0, 3)) >= 0)
                {
                    item.IsChecked = isCheck;

                    // 把卷軸捲到那筆去，如果有多筆只要捲到第一筆就好，不然每一筆都捲速度太慢
                    if (!hasAlreadyScroll)
                    {
                        _vm.SetCurrentAndSelectedItem(item);
                        hasAlreadyScroll = true;
                    }
                }
            }

            MessageBoxExService.Instance().Info("OK");
        }
    }
}