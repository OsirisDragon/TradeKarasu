using CrossModel;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TradeFutNight.Common;
using TradeFutNightData;
using TradeFutNightData.Gates.Specific.Prefix8;
using TradeFutNightData.Models.Common;

namespace TradeFutNight.Views.Prefix8
{
    public class U_80003_ViewModel : ViewModelParent<UIModel_80003>
    {
        public XtraReport Report
        {
            get { return GetProperty(() => Report); }
            set { SetProperty(() => Report, value); }
        }

        public UIModel_80003 MainFormData
        {
            get { return GetProperty(() => MainFormData); }
            set { SetProperty(() => MainFormData, value); }
        }

        public IList<ItemInfo> UpfUserIdName
        {
            get { return GetProperty(() => UpfUserIdName); }
            set { SetProperty(() => UpfUserIdName, value); }
        }

        public IList<ItemInfo> Dpt
        {
            get { return GetProperty(() => Dpt); }
            set { SetProperty(() => Dpt, value); }
        }

        public IEnumerable<string> ComboBoxItemsSource { get; set; }

        public U_80003_ViewModel()
        {
            MainGridData = new ObservableCollection<UIModel_80003>();

            ComboBoxItemsSource = Enumerable.Range(1, 20)
                .Select(x => string.Format("Item {0}", x));
        }

        public void Open()
        {
            UpfUserIdName = DropDownItems.UpfUserIdName();
            var tempDpt = DropDownItems.Dpt();
            // 不用"全部"這個選項
            tempDpt.Remove(tempDpt.SingleOrDefault(c => c.Value.ToString() == "%"));
            Dpt = tempDpt;
        }

        public async Task Query(string userId)
        {
            var task = Task.Run(() =>
            {
                using (var das = Factory.CreateDalSession())
                {
                    var d80003 = new D_80003<UIModel_80003>(das);
                    MainFormData = d80003.GetUserAndCard(userId);
                }
            });

            await task;
        }

        public void SearchByKey(KeyEventArgs e)
        {
        }
    }

    [HighlightedClass]
    public class UIModel_80003 : UPF
    {
        public virtual string UPFCRD_CARD_NO { get; set; }
    }
}