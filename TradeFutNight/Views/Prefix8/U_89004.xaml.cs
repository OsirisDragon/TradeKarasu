using ChangeTracking;
using CrossModel;
using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.Prefix8
{
    /// <summary>
    /// U_89004.xaml 的互動邏輯
    /// </summary>
    public partial class U_89004 : UserControlParent, IViewSword
    {
        private U_89004_ViewModel _vm;

        public U_89004()
        {
            InitializeComponent();
            _vm = (U_89004_ViewModel)DataContext;
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

            List<UIModel_89004_Add> addData = new List<UIModel_89004_Add>();

            var task = Task.Run(async () =>
            {
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();
                var operate = GetChanges<UIModel_89004, YUTP>(_vm.MainGridData, _vm);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dYUTP = new D_YUTP(das);
                        dYUTP.Update(operate.ChangedItems);

                        foreach (var item in trackableData.ChangedItems)
                        {
                            var newUtpInfo = new UIModel_89004_Add()
                            {
                                C_TYPE = item.YUTP_YN_CODE == 'Y' ? "新增" : "刪除",
                                YUTP_YTXN_ID = item.YUTP_YTXN_ID,
                                YTXN_NAME = item.YTXN_NAME,
                                YUTP_USER_ID = cbUserId.Text,
                                W_TIME = DateTime.Now
                            };
                            addData.Add(newUtpInfo);

                            string cTypeVal = item.YUTP_YN_CODE == 'Y' ? "A" : "D";
                            string logMsg = $"{UserID},{UserName},{newUtpInfo.W_TIME.ToString("yyyy/MM/dd HH:mm:ss")},{newUtpInfo.YUTP_USER_ID.Substring(0, 1)}," +
                                            $"{newUtpInfo.YUTP_USER_ID},{newUtpInfo.YUTP_YTXN_ID},{newUtpInfo.YTXN_NAME},{cTypeVal}";
                            DbLog(logMsg, das);
                        }

                        _vm.PrintGridData = _vm.MapperInstance.Map<IList<UIModel_89004_Add>>(addData).AsTrackable();

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

            //rptSetting.HeaderMemoText = "權限設定給：" + string.Join(",", _vm.CurUpfUserName.Select(c => c.Text));

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

        private async void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            VmMainUi.LoadingText = MessageConst.LoadingStatusSaving;

            var userId = ((ItemInfo)cbUserId.SelectedItem).Value.AsString();
            await _vm.Query(userId);

            VmMainUi.HideLoadingWindow();
        }

        private void CheckEdit_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
            }
        }
    }
}