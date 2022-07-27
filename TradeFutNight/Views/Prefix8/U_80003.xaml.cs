using CrossModel;
using CrossModel.Interfaces;
using DevExpress.Xpf.Editors;
using DevExpress.XtraReports.UI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TradeFutNight.Common;
using TradeFutNight.Reports;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views.Prefix8
{
    /// <summary>
    /// U_80003.xaml 的互動邏輯
    /// </summary>
    public partial class U_80003 : UserControlParent, IViewSword
    {
        private U_80003_ViewModel _vm;

        public U_80003()
        {
            InitializeComponent();
            _vm = (U_80003_ViewModel)DataContext;
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
        }

        public void Delete()
        {
        }

        public async Task<bool> CheckField()
        {
            var task = Task.Run(() =>
            {
                if (_vm.MainFormData == null)
                {
                    VmMainUi.HideLoadingWindow();
                    MessageBoxExService.Instance().Error("請先查詢使用者");
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
                _vm.MainFormData.UPF_W_USER_ID = UserID;
                _vm.MainFormData.UPF_W_DATE = DateTime.Now;

                var dtoData = _vm.MapperInstance.Map<UPF>(_vm.MainFormData);

                using (var das = Factory.CreateDalSession())
                {
                    das.Begin();

                    try
                    {
                        var dUpf = new D_UPF(das);
                        dUpf.Update(dtoData);

                        var dUpfcrd = new D_UPFCRD(das);
                        dUpfcrd.DeleteWithNormalType(dtoData.UPF_USER_ID);

                        var upfcrd = new UPFCRD()
                        {
                            UPFCRD_CARD_NO = _vm.MainFormData.UPFCRD_CARD_NO,
                            UPFCRD_CARD_TYPE = 'N',
                            UPFCRD_USER_ID = _vm.MainFormData.UPF_USER_ID,
                            UPFCRD_DEPT_ID = _vm.MainFormData.UPF_DEPT_ID,
                            UPFCRD_VALID_DATE = null,
                            UPFCRD_W_DATE = _vm.MainFormData.UPF_W_DATE,
                            UPFCRD_W_USER_ID = _vm.MainFormData.UPF_W_USER_ID
                        };
                        dUpfcrd.InsertSingle(upfcrd);

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

                XtraReport report = null;
                Dispatcher.Invoke(() =>
                {
                    report = CreateReport();
                });

                var reportGate = await new ReportGate(report).CreateDocumentAsync();
                await reportGate.ExportPdf(GetExportFilePath());
                await reportGate.Print();

                VmMainUi.HideLoadingWindow();
                MessageBoxExService.Instance().Info(MessageConst.ProcessSuccess);
                CloseWindow();
            });
            await task;
        }

        private XtraReport CreateReport()
        {
            string reportTitle = ProgramID + "–" + ProgramName;

            var rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);
            var image = ImageProcess.CaptureControlImage((UIElement)scrollViewerMain.Content);
            var reportCommon = ReportNormal.CreateCommonPortraitForScreenImage(image, rptSetting);

            return reportCommon;
        }

        public async Task Export()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
        }

        public async Task Print()
        {
            await Task.FromResult<object>(null);
            throw new NotImplementedException();
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

            var userId = cbUserId.EditValue.AsString();

            await _vm.Query(userId);

            button.IsEnabled = true;
            VmMainUi.HideLoadingWindow();
        }

        /// <summary>
        /// 這個事件除了更改控制項的文字會觸發之外，當ViewModel Binding時也會更改控制項的值也會觸發
        /// 所以要判斷跟原本的值一不一樣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextEdit_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (_vm.MainFormDataOriginal == null || _vm.MainFormData == null) return;

            var editor = e.Source as TextEdit;
            if (editor == null) return;

            // 抓取控制項上面的Binding的Property屬性名
            BindingExpression bindingExpression = editor.GetBindingExpression(BaseEdit.EditValueProperty);
            var propertyName = bindingExpression.ResolvedSourcePropertyName;

            // 由屬性名來取值
            var originalVal = _vm.MainFormDataOriginal.GetType().GetProperty(propertyName).GetValue(_vm.MainFormDataOriginal, null).ToString();
            var nowVal = _vm.MainFormData.GetType().GetProperty(propertyName).GetValue(_vm.MainFormData, null).ToString();

            if (originalVal == nowVal)
            {
                editor.Background = null;
            }
            else
            {
                editor.Background = Brushes.LightGray;
            }
        }
    }
}