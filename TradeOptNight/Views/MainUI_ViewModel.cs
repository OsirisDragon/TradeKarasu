using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradeOptNight.Common;
using TradeOptNightData;
using TradeOptNightData.Gates.Common;
using TradeOptNightData.Models.Common;

namespace TradeOptNight.Views
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

        public IList<TxnGroup> TxnGridData
        {
            get { return GetProperty(() => TxnGridData); }
            set { SetProperty(() => TxnGridData, value); }
        }

        public MainUI_ViewModel()
        {
            using (var das = Factory.CreateDalSession())
            {
                var dTXN = new D_TXN(das);
                var txnData = dTXN.ListByUser(MagicalHats.UserID)
                    .GroupBy(c => c.TXN_ID.Substring(0, 1))
                    .Select(c => new TxnGroup(c.Key, c.ToArray()));
                TxnGridData = new ObservableCollection<TxnGroup>(txnData.ToArray());
            }
        }

        public void ShowLoadingWindow()
        {
            IsLoadingVisible = true;
        }

        public void HideLoadingWindow()
        {
            IsLoadingVisible = false;
        }

        public class TxnGroup
        {
            public string Name { get; set; }
            public ObservableCollection<TXN> Txns { get; set; }

            public TxnGroup(string name, IEnumerable<TXN> txns)
            {
                switch (name)
                {
                    case "2":
                        Name = "[2] 檔案維護";
                        break;

                    case "3":
                        Name = "[3] 檔案維護";
                        break;

                    case "4":
                        Name = "[4] 會員管理";
                        break;

                    case "5":
                        Name = "[5] 報表製作";
                        break;

                    case "8":
                        Name = "[8] 使用者管理";
                        break;

                    case "9":
                        Name = "[9] 交易管理";
                        break;

                    case "A":
                        Name = "[A] 其它作業";
                        break;

                    case "B":
                        Name = "[B] 結算部專用";
                        break;

                    case "C":
                        Name = "[C] 交易部專用";
                        break;

                    case "S":
                        Name = "[S] Span專用";
                        break;

                    case "E":
                        Name = "[E] 結算部異常作業";
                        break;

                    default:
                        Name = name;
                        break;
                }
                Txns = new ObservableCollection<TXN>(txns);
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}