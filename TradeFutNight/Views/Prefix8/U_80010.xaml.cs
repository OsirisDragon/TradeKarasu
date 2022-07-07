using CrossModel;
using CrossModel.Enum;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeUtility;

namespace TradeFutNight.Views.Prefix8
{
    /// <summary>
    /// U_80010.xaml 的互動邏輯
    /// </summary>
    public partial class U_80010 : UserControlParent, IViewSword
    {
        private U_80010_ViewModel _vm;

        public U_80010()
        {
            InitializeComponent();
            _vm = (U_80010_ViewModel)DataContext;
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
            VmMainUi.IsButtonPrintEnabled = true;
            VmMainUi.IsButtonSaveEnabled = false;
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
        }

        public void Delete()
        {
        }

        public async Task<bool> CheckField()
        {
            var task = Task.Run(() => true);
            await task;

            return task.Result;
        }

        public async Task Save()
        {
            var task = Task.Run(() =>
            {
            });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, false, false, false);

            var gridControl = (GridControl)((DXTabItem)tabControl.SelectedItem).FindName("gridMain" + _vm.OperationTypeVal + _vm.SearchTypeVal);

            if (_vm.OperationTypeVal + _vm.SearchTypeVal == "OpfId")
            {
                rptSetting.HeaderColumnsFontSize = 8;
                rptSetting.ContentColumnsFontSize = 8;
                rptSetting.ContentColumnsWidthScaleFactor = 0.7f;
                rptSetting.HeaderColumnsWidthScaleFactor = 0.7f;
            }
            var operationTypeTxt = ((ItemInfo)rbOperationType.SelectedItem).Text;
            var searchTypeTxt = ((ItemInfo)rbSearchType.SelectedItem).Text;
            rptSetting.HeaderMemoText = $"查詢條件：{operationTypeTxt}, {searchTypeTxt}";

            if (_vm.SearchSubTypeVal != null)
            {
                rptSetting.HeaderMemoText += $", { _vm.SearchSubTypeName}{ _vm.SearchSubTypeVal}";
            }

            var reportCommon = ReportNormal.CreateCommonPortrait(data, gridControl, rptSetting);

            return reportCommon;
        }

        public async Task Export()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task Print()
        {
            var gridControl = (GridControl)((DXTabItem)tabControl.SelectedItem).FindName("gridMain" + _vm.OperationTypeVal + _vm.SearchTypeVal);
            var result = ((IEnumerable)gridControl.ItemsSource).Cast<object>().ToList();

            var report = CreateReport(result, OperationType.Print);
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());
            await reportGate.Print();
        }

        public async Task PrintIndex()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintStock()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        private async void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            VmMainUi.ShowLoadingWindow();

            var button = ((Button)sender);
            button.IsEnabled = false;

            try
            {
                if (_vm.SearchSubType != null && string.IsNullOrEmpty(_vm.SearchSubTypeVal.AsString()))
                {
                    var subTypeName = _vm.SearchSubTypeName.Replace("：", "");
                    MessageBoxExService.Instance().Error($"請選擇{subTypeName}");
                    return;
                }

                await _vm.Query();

                ((DXTabItem)tabControl.FindName("tab" + _vm.OperationTypeVal + _vm.SearchTypeVal)).IsSelected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                button.IsEnabled = true;
                VmMainUi.HideLoadingWindow();
            }
        }

        private void RbOperationType_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            _vm.GetSearchType();
            _vm.SearchTypeVal = _vm.OperationTypeVal == "Txn" ? "Txn" : "Id";
            _vm.GetSearchSubType();
            if (cbSearchSubTypeId.ItemsSource == null)
            {
                cbSearchSubTypeId.Visibility = lblSearchSubType.Visibility = Visibility.Collapsed;
            }
            else
            {
                cbSearchSubTypeId.Visibility = lblSearchSubType.Visibility = Visibility.Visible;
            }
        }

        private void RbSearchType_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            _vm.GetSearchSubType();
            if (cbSearchSubTypeId.ItemsSource == null)
            {
                cbSearchSubTypeId.Visibility = lblSearchSubType.Visibility = Visibility.Collapsed;
            }
            else
            {
                cbSearchSubTypeId.Visibility = lblSearchSubType.Visibility = Visibility.Visible;
            }
        }
    }
}