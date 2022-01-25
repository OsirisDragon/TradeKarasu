using ChangeTracking;
using CrossModel;
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

namespace TradeFutNight.Views.Prefix9
{
    /// <summary>
    /// U_90001.xaml 的互動邏輯
    /// </summary>
    public partial class U_90001 : UserControlParent, IViewSword
    {
        private U_90001_ViewModel _vm;

        public U_90001()
        {
            InitializeComponent();
            _vm = (U_90001_ViewModel)DataContext;
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

                foreach (var item in trackableData.AddedItems)
                {
                    if (item.TXN_ID.Length != 5)
                    {
                        resultItem.AppendErrorMessage($"欄位資料輸入錯誤");
                    }

                    Dispatcher.Invoke(() =>
                    {
                        item.TXN_W_USER_ID = UserID;
                        item.TXN_W_TIME = DateTime.Now;
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
                var domainData = _vm.MapperInstance.Map<IList<TXN>>(trackableData.AddedItems);
                var jswData = new List<JSW>();
                var utpData = new List<UTP>();

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dTXN = new D_TXN(das);
                        dTXN.Insert(domainData);

                        var dUPF = new D_UPF(das);
                        var upfData = dUPF.ListAll();

                        foreach (var item in domainData)
                        {
                            //新增JSW
                            if (item.TXN_TYPE.AsString() == "A")
                            {
                                var jswItem = new JSW();
                                jswItem.JSW_ID = item.TXN_ID;
                                jswItem.JSW_TYPE = 'Q';
                                jswItem.JSW_SW_CODE = 'Y';
                                jswData.Add(jswItem);
                                jswItem = new JSW();
                                jswItem.JSW_ID = item.TXN_ID;
                                jswItem.JSW_TYPE = 'I';
                                jswItem.JSW_SW_CODE = 'Y';
                                jswData.Add(jswItem);
                                jswItem = new JSW();
                                jswItem.JSW_ID = item.TXN_ID;
                                jswItem.JSW_TYPE = 'D';
                                jswItem.JSW_SW_CODE = 'Y';
                                jswData.Add(jswItem);
                                jswItem = new JSW();
                                jswItem.JSW_ID = item.TXN_ID;
                                jswItem.JSW_TYPE = 'U';
                                jswItem.JSW_SW_CODE = 'Y';
                                jswData.Add(jswItem);
                            }
                            else
                            {
                                var jswItem = new JSW();
                                jswItem.JSW_ID = item.TXN_ID;
                                jswItem.JSW_TYPE = item.TXN_TYPE.ToCharArray()[0];
                                jswItem.JSW_SW_CODE = 'Y';
                                jswData.Add(jswItem);
                                if (item.TXN_TYPE.AsString() == "D")
                                {
                                    jswItem = new JSW();
                                    jswItem.JSW_ID = item.TXN_ID;
                                    jswItem.JSW_TYPE = 'Q';
                                    jswItem.JSW_SW_CODE = 'Y';
                                    jswData.Add(jswItem);
                                }
                            }

                            //新增UTP資料By UPF
                            foreach (var upfItem in upfData)
                            {
                                var utpItem = new UTP();
                                utpItem.UTP_USER_ID = upfItem.UPF_USER_ID;
                                utpItem.UTP_TXN_ID = item.TXN_ID.AsString();
                                utpItem.UTP_W_DATE = DateTime.Now;
                                utpItem.UTP_W_USER_ID = UserID;
                                utpData.Add(utpItem);
                            }
                        }
                        var dJSW = new D_JSW(das);
                        dJSW.Insert(jswData);
                        DbLog("新增JSW", das);

                        var dUTP = new D_UTP(das);
                        dUTP.Insert(utpData);
                        DbLog("新增UTP", das);

                        UpdateAccessPermission(ProgramID, das);

                        DbLog($"{domainData[0].TXN_ID}等{domainData.Count}筆新增", das);

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
            var rptSetting = ReportNormal.CreateSetting(ProgramID, ProgramID + "–" + ProgramName, UserName, Memo, Ocf.OCF_DATE, true, false, true);
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