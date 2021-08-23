using ChangeTracking;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TradeFutNight
{
    internal class TestViewModel : ViewModelBase
    {
        public TestViewModel()
        {
            if (IsInDesignMode)
            {
                //Orders = new ObservableCollection<Order>();
                Orders = new List<Order>();
            }
            else
            {
                //var tt = new ObservableCollection<Order>();

                IList<Order> tt = new List<Order>();
                tt.Add(new Order() { OrderID = 1, CustomerID = "AA" });
                tt.Add(new Order() { OrderID = 2, CustomerID = "BB" });
                tt.Add(new Order() { OrderID = 3, CustomerID = "CC" });
                //Orders = tt;

                var trackTT = tt.AsTrackable();
                Orders = trackTT;
            }

            SaveCommand = new DelegateCommand(DoSave);
        }

        public IList<Order> Orders
        {
            //get => GetValue<IList<Order>>();
            //private set => SetValue(value);
            get; set;
        }

        public ICommand SaveCommand { get; }

        private void DoSave()
        {
            var test = Orders;
            //var tt = new ObservableCollection<Order>();

            ////IList<Order> tt = new List<Order>();
            //tt.Add(new Order() { OrderID = 1, CustomerID = "QQ" });
            //tt.Add(new Order() { OrderID = 2, CustomerID = "KK" });
            //tt.Add(new Order() { OrderID = 3, CustomerID = "WW" });
            //Orders = tt;

            //var trackable = Orders.CastToIChangeTrackableCollection();

            //bool isChanged = trackable.IsChanged;

            //// Will return all items with ChangeTrackingStatus of Unchanged
            //IEnumerable<Order> unchangedOrders = trackable.UnchangedItems;
            //// Will return all items that were added to the collection - with ChangeTrackingStatus of Added
            //IEnumerable<Order> addedOrders = trackable.AddedItems;
            //// Will return all items with ChangeTrackingStatus of Changed
            //IEnumerable<Order> changedOrders = trackable.ChangedItems;
            //// Will return all items that were removed from the collection - with ChangeTrackingStatus of Deleted
            //IEnumerable<Order> deletedOrders = trackable.DeletedItems;

            //trackable.AcceptChanges();
            //trackable.RejectChanges();
        }

        //public ObservableCollection<Order> Orders
        //{
        //    get => GetValue<ObservableCollection<Order>>();
        //    private set => SetValue(value);
        //}
    }
}