using CrossModel;
using DevExpress.Mvvm;
using TradeFutNight.Interfaces;

namespace TradeFutNight.Views
{
    public class MainUI_ViewModel : ViewModelBase
    {
        public bool IsLoadingVisible
        {
            get { return GetProperty(() => IsLoadingVisible); }
            set { SetProperty(() => IsLoadingVisible, value); }
        }

        public string LoadingText
        {
            get { return GetProperty(() => LoadingText); }
            set { SetProperty(() => LoadingText, value); }
        }

        public bool IsButtonInsertEnabled
        {
            get { return GetProperty(() => IsButtonInsertEnabled); }
            set { SetProperty(() => IsButtonInsertEnabled, value); }
        }

        public bool IsButtonSaveEnabled
        {
            get { return GetProperty(() => IsButtonSaveEnabled); }
            set { SetProperty(() => IsButtonSaveEnabled, value); }
        }

        public bool IsButtonDeleteEnabled
        {
            get { return GetProperty(() => IsButtonDeleteEnabled); }
            set { SetProperty(() => IsButtonDeleteEnabled, value); }
        }

        public bool IsButtonPrintEnabled
        {
            get { return GetProperty(() => IsButtonPrintEnabled); }
            set { SetProperty(() => IsButtonPrintEnabled, value); }
        }

        public bool IsButtonPrintIndexEnabled
        {
            get { return GetProperty(() => IsButtonPrintIndexEnabled); }
            set { SetProperty(() => IsButtonPrintIndexEnabled, value); }
        }

        public bool IsButtonPrintStockEnabled
        {
            get { return GetProperty(() => IsButtonPrintStockEnabled); }
            set { SetProperty(() => IsButtonPrintStockEnabled, value); }
        }

        public bool IsButtonExportEnabled
        {
            get { return GetProperty(() => IsButtonExportEnabled); }
            set { SetProperty(() => IsButtonExportEnabled, value); }
        }

        public MainUI_ViewModel()
        {
        }

        public void ShowLoadingWindow()
        {
            IsLoadingVisible = true;
        }

        public void HideLoadingWindow()
        {
            IsLoadingVisible = false;
        }
    }
}