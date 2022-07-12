using ChangeTracking;
using CrossModel;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using CrossModel.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Gates.Specific.Prefix8;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.Prefix8
{
    /// <summary>
    /// U_80014.xaml 的互動邏輯
    /// </summary>
    public partial class U_80014 : UserControlParent, IViewSword
    {
        private U_80014_ViewModel _vm;

        public U_80014()
        {
            InitializeComponent();
            _vm = (U_80014_ViewModel)DataContext;
        }

        public async Task<bool> IsCanRun()
        {
            var task = Task.Run(() =>
            {
                var isCanRun = IsCanRunProgram();
                DbLog(MessageConst.IsCanRun + ":" + isCanRun.ToString().ToUpper());
                return isCanRun;
            });
            await task;

            return task.Result;
        }

        public override void ToolButtonSetting()
        {
            base.ToolButtonSetting();
        }

        public async Task Open()
        {
            ToolButtonSetting();

            var task = Task.Run(() =>
            {
                _vm.Open();
                DbLog(MessageConst.Open);
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
            base.Delete(gridMain, _vm);
        }

        public async Task<bool> CheckField()
        {
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = false }, gridMain, _vm))
                return false;

            var task = Task.Run(() =>
            {
                if (!IsCanRunProgram())
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NotAllowedExcute);
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

            List<UIModel_80014_Add> addData = new List<UIModel_80014_Add>();

            var task = Task.Run(async () =>
            {
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dUTP = new D_UTP(das);
                        foreach (var item in trackableData)
                        {
                            var isCheck = item.UTP_YN_CODE;
                            if (isCheck)
                            {
                                foreach (var user in _vm.CurUpfUserName)
                                {
                                    var userId = user.Value.ToString();
                                    var utpCount = dUTP.ListByTxnAndUser(item.TXN_ID, userId).Count();
                                    var newUtp = new UTP()
                                    {
                                        UTP_USER_ID = userId,
                                        UTP_TXN_ID = item.TXN_ID,
                                        UTP_YN_CODE = 'Y',
                                        UTP_W_DATE = DateTime.Now,
                                        UTP_W_USER_ID = UserID
                                    };
                                    if (utpCount > 0)
                                    {
                                        dUTP.UpdateItem(newUtp);
                                    }
                                    else
                                    {
                                        dUTP.InsertItem(newUtp);
                                    }

                                    var newUtpInfo = new UIModel_80014_Add()
                                    {
                                        C_TYPE = "新增",
                                        UTP_TXN_ID = item.TXN_ID,
                                        TXN_NAME = item.TXN_NAME,
                                        UTP_USER_ID = user.Text,
                                        W_TIME = DateTime.Now
                                    };
                                    addData.Add(newUtpInfo);

                                    string logMsg = $"{UserID},{UserName},{newUtpInfo.W_TIME.ToString("yyyy/MM/dd HH:mm:ss")},{newUtpInfo.UTP_USER_ID.Substring(0, 1)}," +
                                                    $"{newUtpInfo.UTP_USER_ID},{newUtpInfo.UTP_TXN_ID},{newUtpInfo.TXN_NAME},A";
                                    DbLog(logMsg, das);
                                }
                            }
                        }

                        _vm.PrintGridData = _vm.MapperInstance.Map<IList<UIModel_80014_Add>>(addData).AsTrackable();

                        UpdateAccessPermission(ProgramID, das);

                        DbLog(MessageConst.Completed, das);

                        das.Commit();
                    }
                    catch (Exception ex)
                    {
                        das.Rollback();
                        throw ex;
                    }
                }

                var report = CreateReport(trackableData.ToList(), gridMain);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath());

                var reportAdd = CreateReport(_vm.PrintGridData, gridMainPrint);
                var reportGateAdd = await new ReportGate(reportAdd).CreateDocumentAsync();
                await reportGateAdd.ExportPdf(GetExportFilePath());

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;

            tabControl.SelectedIndex = 1;
        }

        private XtraReport CreateReport<T>(IList<T> data, GridControl gridControl)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);

            rptSetting.HeaderMemoText = "權限設定給：" + string.Join(",", _vm.CurUpfUserName.Select(c => c.Text));

            var reportCommon = ReportNormal.CreateCommonPortrait(data, gridControl, rptSetting);

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
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
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

        private void BtnChoose_Click(object sender, RoutedEventArgs e)
        {
            var selectItem = (ItemInfo)lbOriUpf.SelectedItem;
            if (selectItem == null)
            {
                MessageBoxExService.Instance().Error($"請選擇使用者");
                return;
            }

            _vm.AddUser(selectItem);

            cbUserId.IsEnabled = ((IList<ItemInfo>)lbCurUpf.ItemsSource).Count > 0;
        }

        private void BtnDechoose_Click(object sender, RoutedEventArgs e)
        {
            var selectItem = (ItemInfo)lbCurUpf.SelectedItem;
            if (selectItem == null)
            {
                MessageBoxExService.Instance().Error($"請選擇使用者");
                return;
            }
            _vm.RemoveUser(selectItem);

            cbUserId.IsEnabled = ((IList<ItemInfo>)lbCurUpf.ItemsSource).Count > 0;
        }

        private async void CbUserId_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            var cbEditor = ((ComboBoxEdit)sender);
            var userId = ((ItemInfo)cbEditor.SelectedItem).Value.AsString();

            await _vm.Query();

            using (var das = Factory.CreateDalSession())
            {
                try
                {
                    var d80014 = new D_80014<UIModel_80014_UTP>(das);
                    var data = d80014.ListByUserId(userId);

                    foreach (var item in _vm.MainGridData)
                    {
                        var foundItem = data.Where(c => c.UTP_TXN_ID == item.TXN_ID).SingleOrDefault();
                        if (foundItem != null)
                        {
                            item.UTP_YN_CODE = true;
                        }
                    }
                    MessageBoxExService.Instance().Info($"已複製指定使用者之權限，請於編輯權限完成後，點選存檔圖示將資料儲存。");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}