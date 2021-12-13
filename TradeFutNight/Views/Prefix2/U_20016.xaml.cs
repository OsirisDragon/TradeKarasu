﻿using ChangeTracking;
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

namespace TradeFutNight.Views.Prefix2
{
    /// <summary>
    /// U_20016.xaml 的互動邏輯
    /// </summary>
    public partial class U_20016 : UserControlParent, IViewSword
    {
        private U_20016_ViewModel _vm;

        public U_20016()
        {
            InitializeComponent();
            _vm = (U_20016_ViewModel)DataContext;
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

        public override void ControlSetting()
        {
            base.ControlSetting();
            VmMainUi.IsButtonPrintEnabled = true;
        }

        public async Task Open()
        {
            ControlSetting();

            _vm.StartDate = _vm.DefaultMinDateTime;
            _vm.EndDate = _vm.DefaultMinDateTime;
            _vm.GenerateYear = Ocf.OCF_DATE.Year + 1;

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

                foreach (var item in trackableData.ChangedItems)
                {
                    //if (item.TPPINTD_SECOND_MONTH > 0 && string.IsNullOrEmpty(item.TPPINTD_SECOND_KIND_ID))
                    //{
                    //    resultItem.AppendErrorMessage($"請輸入第{trackableData.AsEnumerable().IndexOf(item) + 1}筆的第二支腳契約代碼");
                    //}

                    //if (!string.IsNullOrEmpty(item.TPPINTD_SECOND_KIND_ID) && item.TPPINTD_SECOND_MONTH <= 0)
                    //{
                    //    resultItem.AppendErrorMessage($"請輸入第{trackableData.AsEnumerable().IndexOf(item) + 1}筆的第二支腳月份序號");
                    //}

                    //Dispatcher.Invoke(() =>
                    //{
                    //    item.TPPINTD_USER_ID = UserID;
                    //    item.TPPINTD_W_TIME = DateTime.Now;
                    //});
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
                var domainData = CustomMapper(trackableData.ChangedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dTppintd = new D_TPPINTD(das);
                        dTppintd.Update(domainData);

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

                var report = CreateReport(domainData, OperationType.Save);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(ExportFilePath);
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private IList<TPPINTD> CustomMapper(IEnumerable<UIModel_20016> items)
        {
            var listResult = new List<TPPINTD>();
            //foreach (var item in items)
            //{
            //    var newItem = _vm.MapperInstance.Map<TPPINTD>(item);

            //    var trackItem = item.CastToIChangeTrackable();
            //    newItem.OriginalData = trackItem.GetOriginal();
            //    listResult.Add(newItem);
            //}

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
                    reportTitle += "–查詢";
                    break;

                default:
                    break;
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, memo, Ocf.OCF_DATE, true, false, true);
            var reportCommon = ReportNormal.CreateCommonPortrait(data, gridMain, rptSetting);

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
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
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
            var button = ((Button)sender);
            button.IsEnabled = false;

            var task = Task.Run(async () =>
            {
                await _vm.Query();
            });

            await task;

            button.IsEnabled = true;
        }

        private void BtnGenerateYear_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}