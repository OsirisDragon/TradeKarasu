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

namespace TradeFutNight.Views.Prefix2
{
    /// <summary>
    ///  U_20016.xaml 的互動邏輯
    /// </summary>
    public partial class U_20016 : UserControlParent, IViewSword
    {
        private U_20016_ViewModel _vm;

        private OperationType _operationType = OperationType.Save;

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
                    Dispatcher.Invoke(() =>
                    {
                        item.MOCFEX_USER_ID = UserID;
                        item.MOCFEX_W_TIME = DateTime.Now;
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
                var domainData = CustomMapper<MOCFEX>(trackableData);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dMocfex = new D_MOCFEX(das);
                        dMocfex.Save(domainData);

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

                var report = CreateReport(domainData.ToList());
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(ExportFilePath);
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private IList<T> CustomMapper<T>(IEnumerable<UIModel_20016> items) where T : MOCFEX
        {
            var listResult = new List<T>();
            foreach (var item in items)
            {
                var newItem = _vm.MapperInstance.Map<T>(item);
                var trackItem = item.CastToIChangeTrackable();
                newItem.OriginalData = trackItem.GetOriginal();
                listResult.Add(newItem);
            }

            return listResult;
        }

        private XtraReport CreateReport<T>(IList<T> data)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            switch (_operationType)
            {
                case OperationType.Insert:
                    reportTitle += "–新增";
                    break;

                case OperationType.Save:
                    reportTitle += "–變更";
                    break;

                case OperationType.Print:
                    reportTitle += "–查詢";
                    break;

                default:
                    break;
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);
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

            _operationType = OperationType.Print;
            var report = CreateReport(_vm.MainGridData);
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
            _operationType = OperationType.Save;
        }

        private void BtnGenerateYear_Click(object sender, RoutedEventArgs e)
        {
            using (var das = Factory.CreateDalSession())
            {
                var dMocfex = new D_MOCFEX(das);
                var hasYearData = dMocfex.HasYearData(_vm.GenerateYear);

                if (hasYearData)
                {
                    MessageBoxExService.Instance().Error("已有" + _vm.GenerateYear + "年度資料，請改用查詢修改");
                    return;
                }
            }

            _vm.MainGridData.Clear();

            DateTime startDate = new DateTime(_vm.GenerateYear, 1, 1);
            DateTime endDate = new DateTime(_vm.GenerateYear, 12, 31);

            List<DateTime> dateList = new List<DateTime>();
            while (startDate <= endDate)
            {
                var uiModel = new UIModel_20016();
                uiModel.MOCFEX_DATE = startDate;

                char openCode = 'Y';
                if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    openCode = 'N';
                }

                uiModel.MOCFEX_CBOE_OPEN_CODE = openCode;
                uiModel.MOCFEX_DAY_OF_WEEK = Convert.ToChar(((int)startDate.DayOfWeek).ToString());
                uiModel.MOCFEX_USER_ID = UserID;
                uiModel.MOCFEX_W_TIME = DateTime.Now;
                _vm.Add(uiModel);

                startDate = startDate.AddDays(1);
            }

            _operationType = OperationType.Insert;

            MessageBoxExService.Instance().Info("新增" + _vm.GenerateYear + "年度完成請進行修改!!");
        }
    }
}