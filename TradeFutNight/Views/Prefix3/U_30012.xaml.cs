using ChangeTracking;
using CrossModel;
using CrossModel.Enum;
using DevExpress.XtraReports.UI;
using Leo.Palm.Client.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// U_30012.xaml 的互動邏輯
    /// </summary>
    public partial class U_30012 : UserControlParent, IViewSword
    {
        private U_30012_ViewModel _vm;

        public U_30012()
        {
            InitializeComponent();
            _vm = (U_30012_ViewModel)DataContext;
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

            var task = Task.Run(async () =>
            {
                var trackableData = _vm.MainGridData.CastToIChangeTrackableCollection();
                var domainData = _vm.MapperInstance.Map<IList<PUT>>(trackableData.DeletedItems);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dPUT = new D_PUT(das);
                        dPUT.Delete(domainData);

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
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private XtraReport CreateReport<T>(IList<T> data, OperationType operationType)
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            switch (operationType)
            {
                case OperationType.Save:
                    reportTitle = reportTitle.Replace("查詢、", "");
                    break;

                case OperationType.Print:
                    reportTitle = reportTitle.Replace("、刪除", "");
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

        private PalmLib palm = new PalmLib();

        private void BtnEnroll_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBoxExService.Instance().Info("Please enter the ID.");
            }
            else
            {
                BIORESULT r = palm.BioAPI_Enroll();
                if (r.WParam == 0)
                {
                    using (var das = Factory.CreateDalSession())
                    {
                        das.Begin();

                        try
                        {
                            D_TMPCRD dTMPCRD = new D_TMPCRD(das);
                            List<TMPCRD> data = new List<TMPCRD>();
                            TMPCRD item = new TMPCRD();
                            item.TMPCRD_USER_ID = txtId.Text;
                            item.TMPCRD_TXT_CODE = r.LParam;
                            item.TMPCRD_BIN_CODE = Encoding.UTF8.GetBytes(r.LParam);
                            data.Add(item);
                            dTMPCRD.Insert(data);

                            r = palm.BioAPI_AddUser(txtId.Text, r.LParam);

                            das.Commit();
                        }
                        catch (Exception ex)
                        {
                            das.Rollback();
                            throw ex;
                        }
                    }
                }
                MessageBoxExService.Instance().Info(string.Format("Enroll {0} !", r.WParam == 0 ? "Success" : "Fail"));
            }
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            using (var das = Factory.CreateDalSession())
            {
                das.Begin();

                try
                {
                    D_TMPCRD dTMPCRD = new D_TMPCRD(das);
                    var data = dTMPCRD.ListById(txtId.Text);
                    if (data.Count != 0)
                    {
                        //string key = data.AsTrackable().ToList().FirstOrDefault().TMPCRD_TXT_CODE;
                        string key = Encoding.UTF8.GetString(data.AsTrackable().ToList().FirstOrDefault().TMPCRD_BIN_CODE);
                        BIORESULT r = palm.BioAPI_AddUser(txtId.Text, key);
                    }
                    else
                    {
                        MessageBoxExService.Instance().Info("無此使用者");
                    }
                }
                catch (Exception ex)
                {
                    das.Rollback();
                    throw ex;
                }

                if (!String.IsNullOrEmpty(txtId.Text))
                {
                    BIORESULT r = palm.BioAPI_Verify(txtId.Text);
                    MessageBoxExService.Instance().Info(string.Format("Verify {0} !", r.WParam == 0 ? "Success" : "Fail"));
                }
                else
                {
                    MessageBoxExService.Instance().Info("Please select a ID.");
                }
            }
        }

        private void BtnCheckN_Click(object sender, RoutedEventArgs e)
        {
            BIORESULT r = palm.BioAPI_Identify();
            if (r.WParam == 0)
                MessageBox.Show(string.Format("Identify Success: {0} !", r.LParam));
            else
                MessageBox.Show(string.Format("Identify Fail !"));
        }

        private void BtnConvertByte_Click(object sender, RoutedEventArgs e)
        {
            string key;
            byte[] keyByteData;
            string byteToStr;
            using (var das = Factory.CreateDalSession())
            {
                das.Begin();

                try
                {
                    D_TMPCRD dTMPCRD = new D_TMPCRD(das);
                    var data = dTMPCRD.ListById(txtId.Text);
                    if (data.Count != 0)
                    {
                        key = data.AsTrackable().ToList().FirstOrDefault().TMPCRD_TXT_CODE;
                        keyByteData = Encoding.UTF8.GetBytes(key);
                        byteToStr = Encoding.UTF8.GetString(keyByteData);
                        MessageBoxExService.Instance().Info("byteToStr = key : " + (byteToStr == key));
                    }
                    else
                    {
                        MessageBoxExService.Instance().Info("無此使用者");
                    }
                }
                catch (Exception ex)
                {
                    das.Rollback();
                    throw ex;
                }
            }
        }

        private void BtnCapture_Click(object sender, RoutedEventArgs e)
        {
            BIORESULT r = palm.BioAPI_Capture();
            if (r.WParam == 0)
                MessageBox.Show(string.Format("Capture Success: {0} !", r.LParam));
            else
                MessageBox.Show(string.Format("Capture Fail !"));
        }
    }
}