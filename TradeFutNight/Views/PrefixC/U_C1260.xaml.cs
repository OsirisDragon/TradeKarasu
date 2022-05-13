using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using Shield.File;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Tfxm;
using TradeFutNightData.Models.Tfxm;

namespace TradeFutNight.Views.PrefixC
{
    /// <summary>
    /// U_C1260.xaml 的互動邏輯
    /// </summary>
    public partial class U_C1260 : UserControlParent, IViewSword
    {
        private U_C1260_ViewModel _vm;

        public U_C1260()
        {
            InitializeComponent();
            _vm = (U_C1260_ViewModel)DataContext;
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

                var resultItem = new ResultItem();
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();

                foreach (var item in trackableData)
                {
                    Dispatcher.Invoke(() =>
                    {
                        item.FRP_CONFIRM = 'Y';
                        item.FRP_USER_ID = UserID;
                        item.FRP_W_TIME = DateTime.Now;
                    });
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
                var domainData = CustomMapper<FRP>(trackableData.ChangedItems);

                using (var dasTfxm = Factory.CreateDalSession(SettingFile.Database.Tfxm_AH))
                {
                    dasTfxm.Begin();

                    try
                    {
                        var dFRP = new D_FRP(dasTfxm);

                        //更新現貨資料
                        dFRP.Update(domainData);

                        using (var das = Factory.CreateDalSession())
                        {
                            das.Begin();

                            try
                            {
                                //更新交易系統狀態
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

                        dasTfxm.Commit();
                    }
                    catch (Exception ex)
                    {
                        dasTfxm.Rollback();
                        throw ex;
                    }
                }

                var report = CreateReport(domainData, OperationType.Save);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private IList<T> CustomMapper<T>(IEnumerable<UIModel_C1260> items) where T : FRP
        {
            var listResult = new List<T>();

            Dispatcher.Invoke(() =>
            {
                foreach (var item in items)
                {
                    var newItem = _vm.MapperInstance.Map<T>(item);

                    var trackItem = item.CastToIChangeTrackable();
                    newItem.OriginalData = trackItem.GetOriginal();
                    listResult.Add(newItem);
                }
            });

            return listResult;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

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

            var report = CreateReport(_vm.MainGridData, OperationType.Print);
            var reportGate = await new ReportGate(report).CreateDocumentAsync();
            await reportGate.ExportPdf(GetExportFilePath());
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
    }
}