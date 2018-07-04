using CookingApp.Services;
using System;

namespace CookingApp.ViewModels.OrdersPage
{
    public class SingleOrderViewModel
    {
        public SingleOrderViewModel(DateTime date, TimeSpan time)
        {
            Date = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }

        private OrdersModel _model = new OrdersModel();

        public DateTime Date { get; set; }

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
