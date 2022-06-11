using ChangeTracking;
using CrossModel;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix3
{
    /// <summary>
    /// U_30041.xaml 的互動邏輯
    /// </summary>
    public partial class U_30041 : UserControlParent, IViewSword
    {
        private U_30041_ViewModel _vm;

        public U_30041()
        {
            InitializeComponent();
            _vm = (U_30041_ViewModel)DataContext;
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
            base.Delete(gridMain, _vm, false);
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

                if (trackableData.AddedItems.Count() == 0)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error(MessageConst.NoAddedData);
                    return false;
                }

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        foreach (var item in trackableData.AddedItems)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                item.TPPST_USER_ID = UserID;
                                item.TPPST_W_TIME = DateTime.Now;
                            });

                            if (item.TPPST_ACCU_QNTY == 0)
                            {
                                MessageBoxExService.Instance().Error("最小累計口數不得為0");
                                return false;
                            }
                            // 檢查任一「動態退單百分比」不為0者，其退單點數基準價類別(PDK2ND_TPP_PRICE_TYPE)不可為空白
                            if (item.TPPST_UNIT != 0)
                            {
                                var dPDK2ND = new D_PDK2ND(das);

                                var data = dPDK2ND.GetByKindId(item.TPPST_KIND_ID);

                                if (data == null || (data != null && string.IsNullOrEmpty(data.PDK2ND_TPP_PRICE_TYPE.ToString())))
                                {
                                    MessageBoxExService.Instance().Error($"請先執行30018作業設定{item.TPPST_KIND_ID}『退單點數基準價類別』");
                                    return false;
                                }
                            }
                        }
                        string kindId = "";
                        bool checkResult = true;
                        foreach (var item in trackableData.AddedItems)
                        {
                            if (kindId != item.TPPST_KIND_ID)
                            {
                                kindId = item.TPPST_KIND_ID;

                                var dTPPST = new D_TPPST(das);

                                var tppstData = dTPPST.ListByKindId(kindId);
                                var addData = trackableData.AddedItems.Where(c => c.TPPST_KIND_ID == kindId);
                                var data = tppstData.Union(addData)
                                                    .Select(c => new { c.TPPST_KIND_ID, c.TPPST_MONTH })
                                                    .Distinct();
                                for (int i = 0; i < 6; i++)
                                {
                                    var checkData = data.Where(c => c.TPPST_MONTH == i);
                                    if (checkData.Count() == 0)
                                    {
                                        checkResult = false;
                                        break;
                                    }
                                }
                                if (!checkResult)
                                {
                                    var msgResult = MessageBoxExService.Instance().Confirm($"{item.TPPST_KIND_ID}的月份不完整，要有012345總共6個月份，是否繼續");
                                    if (msgResult == System.Windows.MessageBoxResult.No)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        das.Rollback();
                        throw ex;
                    }
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
                var domainData = _vm.MapperInstance.Map<IList<TPPST>>(trackableData.AddedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dTPPST = new D_TPPST(das);
                        dTPPST.Insert(domainData);

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

                var report = CreateReport(domainData);
                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();

                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });

            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data)
        {
            string reportTitle = ProgramID + "–" + ProgramName;
            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);
            var reportCommon = ReportNormal.CreateCommonLandscape(data, gridMain, rptSetting);

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