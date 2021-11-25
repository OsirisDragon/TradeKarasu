using CrossModel;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using Shield.File;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TradeFutNight.Common;
using TradeFutNight.Interfaces;
using TradeFutNightData;
using TradeFutNightData.Gates.Common;
using TradeUtility;

namespace TradeFutNight.Views
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : UserControl
    {
        private MainUI_ViewModel _vm;

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

            txtTxnId.Focus();

            barBottomDataBase.Content = "資料庫：" + SettingFile.Database.Futures_AH.ConnectionName;
            barBottomUser.Content = "使用者：" + MagicalHats.UserID + " " + MagicalHats.UserName;
            barBottomDate.Content = "日期：" + MagicalHats.Ocf.OCF_DATE.ToDateStr();

            MagicalHats.CheckMsgServerConnection();
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
                viewInstance = (IViewSword)panel.Control;
            }
            else
            {
                // 加入DocumentPanel
                // 這會觸發DocumentGroupMain_SelectedItemChanged事件
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

                viewInstance = (IViewSword)panel.Control;
                ((UserControlParent)viewInstance).Init(programID, programName, _vm, this);
            }

            bool isCanRun = await IsCanRun(panel);
            if (!isCanRun)
            {
                CloseWindow();
                return;
            }

            await viewInstance.Open();

            dockLayoutManagerMain.LayoutController.Activate(panel);

            _vm.IsLoadingVisible = false;
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

        private async void AccordionItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var programID = "30027";
                var programName = "造市者報價限制檔新增";

                await OpenProgram(programID, programName);
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
                var panel = (DocumentPanel)e.Item;
                bool isCanRun = await IsCanRun(panel);
                if (isCanRun)
                {
                    dockLayoutManagerMain.LayoutController.Activate(e.Item);
                    var viewInstance = (IViewSword)panel.Control;
                    viewInstance.ControlSetting();
                }
            }
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
    }
}