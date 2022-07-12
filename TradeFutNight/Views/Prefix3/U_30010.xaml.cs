using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeFutNight.Common;
using CrossModel.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30010.xaml 的互動邏輯
    /// </summary>
    public partial class U_30010 : UserControlParent, IViewSword
    {
        private U_30010_ViewModel _vm;

        public U_30010()
        {
            InitializeComponent();
            _vm = (U_30010_ViewModel)DataContext;
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
            VmMainUi.IsButtonDeleteEnabled = false;
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
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = false }, gridMainA, _vm.Vm_A))
                return false;
            if (!BaseCheck(new CheckSettings() { IsCheckNotNullNotEmpty = false }, gridMainB, _vm.Vm_B))
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
                var trackableData = _vm.Vm_B.MainGridData.CastToIChangeTrackableCollection();

                foreach (var item in trackableData.ChangedItems)
                {
                    Dispatcher.Invoke(() =>
                    {
                        item.CADJ_USER_ID = UserID;
                        item.CADJ_W_TIME = DateTime.Now;
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
                var operateA = GetChanges<UIModel_30010_A, SDI>(_vm.Vm_A.MainGridData, _vm.Vm_A);
                var operateB = GetChanges<UIModel_30010_B, CADJ>(_vm.Vm_B.MainGridData, _vm.Vm_B);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dSdi = new D_SDI(das);
                        dSdi.Update(operateA.ChangedItems);

                        var dCadj = new D_CADJ(das);
                        dCadj.Update(operateB.ChangedItems);

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

                var reportA = CreateReport(_vm.Vm_A.MainGridData, OperationType.Save, gridMainA);

                var reportGate = await new ReportGate(reportA).CreateDocumentAsync();

                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                var reportB = CreateReport(_vm.Vm_B.MainGridData, OperationType.Save, gridMainB);
                var reportGate2 = await new ReportGate(reportB).CreateDocumentAsync();
                await reportGate2.ExportPdf(GetExportFilePath());
                await reportGate2.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info("處理完成，請通知結算作業組!");
                CloseWindow();
            });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType, GridControl grid)
        {
            string reportTitle = ProgramID + "–" + ProgramName;
            string memo = Memo;

            switch (operationType)
            {
                case OperationType.Save:

                    break;

                case OperationType.Print:

                    break;

                default:
                    break;
            }

            if (typeof(T) == typeof(UIModel_30010_A))
            {
                reportTitle += "-A表";
            }
            else if (typeof(T) == typeof(UIModel_30010_B))
            {
                reportTitle += "-B表";
                memo = "";
            }

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, memo, Ocf.OCF_DATE, true, false, true);
            var reportCommon = ReportNormal.CreateCommonPortrait(data, grid, rptSetting);

            return reportCommon;
        }

        public async Task Export()
        {
            gridViewA.CloseEditor();
            gridViewB.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task Print()
        {
            gridViewA.CloseEditor();
            gridViewB.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintIndex()
        {
            gridViewA.CloseEditor();
            gridViewB.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task PrintStock()
        {
            gridViewA.CloseEditor();
            gridViewB.CloseEditor();
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }
    }
}