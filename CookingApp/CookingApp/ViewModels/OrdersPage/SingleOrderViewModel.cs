using System;

namespace CookingApp.ViewModels.OrdersPage
{
    public class SingleOrderViewModel
    {
        public DateTime Date { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public string Address { get; set; }

        public string CookerName { get; set; }

        public int CookerID { get; set; }

        public bool ProductsIncluded { get; set; }

        public int? AddressID { get; set; }

        public string AddressName { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Street { get; set; }
    }
}
