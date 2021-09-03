using ChangeTracking;
using CrossModel;
using DevExpress.DataProcessing;
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

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30024.xaml 的互動邏輯
    /// </summary>
    public partial class U_30024 : UserControlParent, IViewSword
    {
        private U_30024_ViewModel _vm;

        public U_30024()
        {
            InitializeComponent();
            _vm = (U_30024_ViewModel)DataContext;
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
                Dispatcher.Invoke(() =>
                {
                    Insert();
                });
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
            bool isNeedConfirm = false;
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
            VmMainUi.LoadingText = MessageConst.LoadingStatusChecking;

            gridView.CloseEditor();

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

                if (trackableData.AddedItems.Count() == 0)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NoAddedData);
                    return false;
                }

                foreach (var item in trackableData.AddedItems)
                {
                    if (item.TPPINTD_SECOND_MONTH > 0 && string.IsNullOrEmpty(item.TPPINTD_SECOND_KIND_ID))
                    {
                        resultItem.AppendErrorMessage($"請輸入第{trackableData.AsEnumerable().IndexOf(item) + 1}筆的第二支腳契約代碼");
                    }

                    if (!string.IsNullOrEmpty(item.TPPINTD_SECOND_KIND_ID) && item.TPPINTD_SECOND_MONTH <= 0)
                    {
                        resultItem.AppendErrorMessage($"請輸入第{trackableData.AsEnumerable().IndexOf(item) + 1}筆的第二支腳月份序號");
                    }

                    Dispatcher.Invoke(() =>
                    {
                        item.TPPINTD_USER_ID = UserID;
                        item.TPPINTD_W_TIME = DateTime.Now;
                    });
                }

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
                var domainData = _vm.MapperInstance.Map<IList<TPPINTD>>(trackableData.AddedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dTppintd = new D_TPPINTD(das);
                        dTppintd.Insert(domainData);

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

                var report = CreateReport(domainData);
                var reportGate = await new ReportGate(report).CreateDocument();
                await reportGate.ExportPdf(ExportFilePath);
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();

                foreach (var item in trackableData.AddedItems)
                {
                    if (item.TPPINTD_SECOND_KIND_ID == "" && item.TPPINTD_SECOND_MONTH == 0)
                    {
                        MessageBoxExService.Instance().Info("請執行C1400變更除息影響點數!");
                        break;
                    }
                }

                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });

            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data)
        {
            string memo = "";
            Dispatcher.Invoke(() =>
            {
                memo = txtMemo.Text;
            });
            var rptSetting = ReportNormal.CreateSetting(ProgramID, ProgramID + "–" + ProgramName, UserName, memo, Ocf.OCF_DATE, true, false, true);
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
    }
}