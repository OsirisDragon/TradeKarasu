using CrossModel;
using CrossModel.Enum;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Printing;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;
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
            VmMainUi.IsButtonPrintIndexEnabled = true;
            VmMainUi.IsButtonPrintStockEnabled = true;
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

            var report = (U_80003_Report)CreateReport(_vm.MainGridData, OperationType.Query);

            EditingFieldExtensions.Instance.RegisterEditorInfo("ComboBoxEditor", "Custom", "ComboBox Editor");
            report.xrLabel1.EditOptions.Enabled = true;
            report.xrLabel1.EditOptions.EditorName = "ComboBoxEditor";

            _vm.Report = report;
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
                return true;
            });
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
            string reportTitle = ProgramID + AppSettings.DashForTitle + ProgramName;
            ReportSetting rptSetting = ReportNormal.CreateSetting(ProgramID, reportTitle, UserName, Memo, Ocf.OCF_DATE, true, false, true);

            U_80003_Report report = new U_80003_Report
            {
                DataSource = data,
                HasHandlePerson = rptSetting.HasHandlePerson,
                HasConfirmPerson = rptSetting.HasConfirmPerson,
                HasManagerPerson = rptSetting.HasManagerPerson,
                PageHeaderVisible = true,
                TableFooterVisible = true
            };

            switch (operationType)
            {
                case OperationType.Query:
                    report.HasHandlePerson = false;
                    report.HasConfirmPerson = false;
                    report.HasManagerPerson = false;
                    report.PageHeaderVisible = false;
                    report.TableFooterVisible = false;
                    break;

                case OperationType.PrintIndex:
                    rptSetting.ReportTitle += AppSettings.DashForTitle + "指數類";
                    break;

                case OperationType.PrintStock:
                    rptSetting.ReportTitle += AppSettings.DashForTitle + "股票類";
                    break;

                default:
                    break;
            }

            ReportNormal.SetReportHeaderParameters(report, rptSetting);

            return report;
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
            //CloseEditor(docPreviewControl);

            SnapShotPNG((UIElement)scrollViewerMain.Content, 1);

            //RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            //rtb.Render(this);

            //PngBitmapEncoder png = new PngBitmapEncoder();
            //png.Frames.Add(BitmapFrame.Create(rtb));
            //MemoryStream stream = new MemoryStream();
            //png.Save(stream);
            //Image image = Image.FromStream(stream);
            //image.Save("test.jpg");

            //await Task.FromResult<object>(null);
            //throw new NotImplementedException();
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

        public void SnapShotPNG(UIElement source, int zoom)
        {
            try
            {
                double actualHeight = source.RenderSize.Height;
                double actualWidth = source.RenderSize.Width;

                double renderHeight = actualHeight * zoom;
                double renderWidth = actualWidth * zoom;

                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
                VisualBrush sourceBrush = new VisualBrush(source);

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                using (drawingContext)
                {
                    drawingContext.PushTransform(new ScaleTransform(zoom, zoom));
                    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new System.Windows.Point(0, 0), new System.Windows.Point(actualWidth, actualHeight)));
                }
                renderTarget.Render(drawingVisual);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                //using (FileStream stream = new FileStream("test.jpg", FileMode.Create, FileAccess.Write))
                //{
                //    encoder.Save(stream);
                //}

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    var rptSetting = ReportNormal.CreateSetting(ProgramID, "QQ", UserName, Memo, Ocf.OCF_DATE, true, false, true);
                    var reportCommon = ReportNormal.CreateCommonPortraitForScreenImage(image, rptSetting);
                    reportCommon.CreateDocument();
                    reportCommon.ExportToPdf("test.pdf");
                }
            }
            catch (Exception e)
            {
            }
        }

        private void TextEdit_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var editor = e.Source as TextEdit;
            if (editor == null) return;

            editor.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
        }
    }
}