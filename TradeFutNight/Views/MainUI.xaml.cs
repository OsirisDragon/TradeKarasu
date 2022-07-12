using CrossModel;
using CrossModel.Interfaces;
using DataEngine;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using Shield.File;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeFutNightData.Models.Common;
using TradeUtility;

namespace TradeFutNight.Views
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : UserControl
    {
        private MainUI_ViewModel _vm;

        private bool _isNewOpenProgram = false;

        private bool _isExeCallMode = false;

        public bool IsExeCallMode
        {
            get { return _isExeCallMode; }
            set { _isExeCallMode = value; }
        }

        public MainUI()
        {
            InitializeComponent();
            _vm = new MainUI_ViewModel();
            DataContext = _vm;
            ((MessageBoxExService)MessageBoxExService.Instance()).VmMainUi = _vm;

            txtTxnId.Focus();

            barBottomDataBase.Content = "資料庫：" + SettingFile.Database.Futures_AH.ConnectionName;
            barBottomUser.Content = "使用者：" + MagicalHats.UserID + " " + MagicalHats.UserName;
            barBottomDate.Content = "日期：" + MagicalHats.Ocf.OCF_DATE.ToDateStr();

            MagicalHats.CheckMsgServerConnection();
        }

        public void CloseWindow()
        {
            if (!IsExeCallMode)
            {
                Dispatcher.Invoke(() =>
                {
                    var activeItem = dockLayoutManagerMain.LayoutController.ActiveItem;

                    if (activeItem != null)
                    {
                        documentGroupMain.Remove(activeItem);
                    }
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => { Application.Current.Shutdown(886); });
            }
        }

        public void CloseLeftPanel()
        {
            layoutPanelLeft.Visibility = Visibility.Collapsed;
        }

        private void BtnInsert_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var activeItem = dockLayoutManagerMain.LayoutController.ActiveItem;

                if (activeItem != null)
                {
                    ((IViewSword)(((DocumentPanel)activeItem).Control)).Insert();
                }
            }
            catch (Exception ex)
            {
                MessageBoxExService.Instance().Error(ex.Message);
            }
        }

        private void BtnDelete_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var activeItem = dockLayoutManagerMain.LayoutController.ActiveItem;

                if (activeItem != null)
                {
                    ((IViewSword)(((DocumentPanel)activeItem).Control)).Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBoxExService.Instance().Error(ex.Message);
            }
        }

        private async void BtnSave_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                _vm.IsLoadingVisible = true;
                var activeItem = dockLayoutManagerMain.LayoutController.ActiveItem;

                CheckSystem();

                if (activeItem != null)
                {
                    var viewSword = ((IViewSword)(((DocumentPanel)activeItem).Control));

                    bool checkResult = await viewSword.CheckField();
                    if (checkResult)
                    {
                        await viewSword.Save();
                    }
                }
                _vm.IsLoadingVisible = false;
            }
            catch (Exception ex)
            {
                _vm.IsLoadingVisible = false;
                MessageBoxExService.Instance().Error(ex.Message);
            }
        }

        private async void BtnPrint_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                _vm.IsLoadingVisible = true;

                var activeItem = dockLayoutManagerMain.LayoutController.ActiveItem;

                if (activeItem != null)
                {
                    await ((IViewSword)(((DocumentPanel)activeItem).Control)).Print();
                }

                _vm.IsLoadingVisible = false;
            }
            catch (Exception ex)
            {
                _vm.IsLoadingVisible = false;
                MessageBoxExService.Instance().Error(ex.Message);
            }
        }

        private async void BtnPrintIndex_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                _vm.IsLoadingVisible = true;

                var activeItem = dockLayoutManagerMain.LayoutController.ActiveItem;

                if (activeItem != null)
                {
                    await ((IViewSword)(((DocumentPanel)activeItem).Control)).PrintIndex();
                }

                _vm.IsLoadingVisible = false;
            }
            catch (Exception ex)
            {
                _vm.IsLoadingVisible = false;
                MessageBoxExService.Instance().Error(ex.Message);
            }
        }

        private async void BtnPrintStock_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                _vm.IsLoadingVisible = true;

                var activeItem = dockLayoutManagerMain.LayoutController.ActiveItem;

                if (activeItem != null)
                {
                    await ((IViewSword)(((DocumentPanel)activeItem).Control)).PrintStock();
                }

                _vm.IsLoadingVisible = false;
            }
            catch (Exception ex)
            {
                _vm.IsLoadingVisible = false;
                MessageBoxExService.Instance().Error(ex.Message);
            }
        }

        private async void BtnExport_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                _vm.IsLoadingVisible = true;

                var activeItem = dockLayoutManagerMain.LayoutController.ActiveItem;

                if (activeItem != null)
                {
                    await ((IViewSword)(((DocumentPanel)activeItem).Control)).Export();
                }

                _vm.IsLoadingVisible = false;
            }
            catch (Exception ex)
            {
                MessageBoxExService.Instance().Error(ex.Message);
            }
        }

        private async void MenuControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                AccordionControl control = (AccordionControl)sender;
                if (control.SelectedItem.GetType() == typeof(TXN))
                {
                    var selectItem = (TXN)control.SelectedItem;
                    var programID = selectItem.TXN_ID;
                    var programName = selectItem.TXN_NAME;

                    await OpenProgram(programID, programName);
                }
            }
            catch (Exception ex)
            {
                _vm.IsLoadingVisible = false;
                MessageBoxExService.Instance().Error(ex.Message);
            }
        }

        private async Task<bool> IsCanRun(DocumentPanel panel)
        {
            if (panel.Control is IViewSword)
            {
                var viewInstance = (IViewSword)panel.Control;
                bool isCanRun = await viewInstance.IsCanRun();
                if (!isCanRun)
                {
                    _vm.IsLoadingVisible = false;
                    ThemedMessageBox.Show(MessageConst.Attention, MessageConst.NotAllowedExcute, MessageBoxButton.OK, MessageBoxImage.Error);
                    documentGroupMain.Remove(panel);
                    return false;
                }
            }

            return true;
        }

        public async Task OpenProgram(string programID, string programName)
        {
            var panelName = "U_" + programID;

            _vm.LoadingText = "Loading";
            _vm.IsLoadingVisible = true;

            await Task.Delay(500);

            IViewSword viewInstance = null;

            // 看panel有沒有存在已經開啟的視窗裡面
            var panel = (DocumentPanel)dockLayoutManagerMain.GetItem(panelName);

            if (panel != null)
            {
                dockLayoutManagerMain.DockController.RemovePanel(panel);
            }

            // 加入DocumentPanel
            // 這會觸發DocumentGroupMain_SelectedItemChanged事件(如果是新增第一個DocumentPanel才會觸發)
            panel = dockLayoutManagerMain.DockController.AddDocumentPanel(documentGroupMain,
            new Uri(@"Views\Prefix" + programID[0] + @"\U_" + programID + ".xaml", UriKind.Relative));

            panel.Name = panelName;
            panel.Caption = programID + "-" + programName;
            panel.AllowDock = false;
            panel.AllowDrag = false;
            panel.AllowDrop = false;
            panel.AllowFloat = false;
            panel.AllowHide = false;
            panel.AllowMove = false;
            panel.AllowContextMenu = false;

            viewInstance = (IViewSword)panel.Control;
            ((UserControlParent)viewInstance).Init(programID, programName, _vm, this);

            _isNewOpenProgram = true;
            // 這會觸發DocumentGroupMain_SelectedItemChanged事件
            dockLayoutManagerMain.LayoutController.Activate(panel);

            bool isCanRun = await IsCanRun(panel);
            if (!isCanRun)
            {
                documentGroupMain.Remove(panel);
                return;
            }

            await viewInstance.Open();

            _vm.IsLoadingVisible = false;
        }

        /// <summary>
        /// DocumentGroup如果其內的物件變動就會觸發
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DocumentGroupMain_SelectedItemChanged(object sender, DevExpress.Xpf.Docking.Base.SelectedItemChangedEventArgs e)
        {
            // 如果是AddDocumentPanel也會觸發這個事件，但Name會是空白的，所以排除
            if (e.Item != null && e.Item.Name != "")
            {
                // 如果是新開的視窗，也就是Activate觸發的事件，不要再執行相關的檢查
                if (!_isNewOpenProgram)
                {
                    var panel = (DocumentPanel)e.Item;
                    bool isCanRun = await IsCanRun(panel);
                    if (isCanRun)
                    {
                        dockLayoutManagerMain.LayoutController.Activate(e.Item);
                        var viewInstance = (IViewSword)panel.Control;
                        viewInstance.ToolButtonSetting();
                    }
                }
            }
            else
            {
                // 重置所有控制項
                _vm.IsButtonSaveEnabled = false;
                _vm.IsButtonInsertEnabled = false;
                _vm.IsButtonDeleteEnabled = false;
                _vm.IsButtonExportEnabled = false;
                _vm.IsButtonPrintEnabled = false;
                _vm.IsButtonPrintIndexEnabled = false;
                _vm.IsButtonPrintStockEnabled = false;
            }

            // 不管是新增的視窗或是切換Tab頁籤進來的視窗，都會執行這段
            _isNewOpenProgram = false;
        }

        private async void TxtTxnId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var txtTxnID = ((TextBox)sender).Text.Trim();

                using (var das = Factory.CreateDalSession())
                {
                    var txn = new D_TXN(das).Get(txtTxnID);

                    if (txn != null)
                    {
                        try
                        {
                            await OpenProgram(txtTxnID, txn.TXN_NAME);
                        }
                        catch (Exception ex)
                        {
                            _vm.IsLoadingVisible = false;
                            MessageBoxExService.Instance().Error(ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 怕介面放太久，有些資料會變掉，像是營業日之類
        /// </summary>
        private void CheckSystem()
        {
            using (var das = new DalSession())
            {
                var dOcf = new D_OCF(das);
                var currentOcf = dOcf.Get();

                if (MagicalHats.Ocf.OCF_DATE != currentOcf.OCF_DATE)
                {
                    throw new Exception($"資料庫營業日OCF_DATE已經變更成{currentOcf.OCF_DATE.ToDateStr()}，請關閉整個程式，再重新開啟");
                }
            }
        }
    }
}