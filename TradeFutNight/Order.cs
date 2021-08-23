using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeFutNight
{
    public partial class Order
    {
        public Order()
        {
        }

        public virtual int OrderID { get; set; }
        public virtual string CustomerID { get; set; }
        public virtual Nullable<int> EmployeeID { get; set; }
        public virtual Nullable<System.DateTime> OrderDate { get; set; }
        public virtual Nullable<System.DateTime> RequiredDate { get; set; }
        public virtual Nullable<System.DateTime> ShippedDate { get; set; }
        public virtual Nullable<int> ShipVia { get; set; }
        public virtual Nullable<decimal> Freight { get; set; }
        public virtual string ShipName { get; set; }
        public virtual string ShipAddress { get; set; }
        public virtual string ShipCity { get; set; }
        public virtual string ShipRegion { get; set; }
        public virtual string ShipPostalCode { get; set; }
        public virtual string ShipCountry { get; set; }
    }
}