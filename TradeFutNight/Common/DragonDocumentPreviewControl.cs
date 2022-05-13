using DevExpress.Xpf.Printing;
using System.Windows.Input;

namespace TradeFutNight.Common
{
    public class DragonDocumentPreviewControl : DocumentPreviewControl
    {
        public DragonDocumentPreviewControl()
        {
            this.PreviewKeyDown += DragonDocumentPreviewControl_PreviewKeyDown;
            this.PreviewMouseWheel += DragonDocumentPreviewControl_PreviewMouseWheel;
        }

        /// <summary>
        /// 關掉預設的按住Ctrl+滑鼠滾輪可以放大縮小的功能，因為不要讓該死的使用者發現有這個功能
        /// </summary>
        private void DragonDocumentPreviewControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
                e.Handled = true;
        }

        /// <summary>
        /// 關掉預設的快速鍵功能，像是Ctrl+F會跳出搜尋框之類的，因為不要讓該死的使用者發現有這個功能
        /// </summary>
        private void DragonDocumentPreviewControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
                e.Handled = true;
        }
    }
}