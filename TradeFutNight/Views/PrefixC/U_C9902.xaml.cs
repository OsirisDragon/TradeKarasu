using CrossModel;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNight.Reports;

namespace TradeFutNight.Views.PrefixC
{
    /// <summary>
    /// U_C9902.xaml 的互動邏輯
    /// </summary>
    public partial class U_C9902 : UserControlParent, IViewSword
    {
        private U_C9902_ViewModel _vm;

        public U_C9902()
        {
            InitializeComponent();
            _vm = (U_C9902_ViewModel)DataContext;
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

            var task = Task.Run(() =>
            {
            });
            await task;
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

            List<TemplatedLink> links = new List<TemplatedLink>();

            if (_vm.MainGridData.Count != 0)
                links.Add(new PrintableControlLink((TableView)gridMain.View) { });

            if (_vm.FileGridData.Count != 0)
                links.Add(new PrintableControlLink((TableView)gridFile.View) { ReportHeaderTemplate = Resources["reportHeaderGridFile"] as DataTemplate });

            CompositeLink compositeLink = new CompositeLink(links);
            compositeLink.Margins = new System.Drawing.Printing.Margins(20, 20, 10, 10);
            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            compositeLink.DocumentName = ProgramID + "–" + ProgramName;

            var reportGate = new ReportGate(compositeLink);
            await reportGate.ExportPdf(GetExportFilePath());
            await reportGate.Print();

            MessageBoxExService.Instance().Info(MessageConst.PrintSuccess);

            await Task.Yield();
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