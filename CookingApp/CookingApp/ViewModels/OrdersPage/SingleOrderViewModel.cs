using CookingApp.ViewModels.MainPage;
using System;
using Xamarin.Forms;

namespace CookingApp.ViewModels.OrdersPage
{
    public class SingleOrderViewModel : ObservableViewModel
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string FromTime { get; set; }

        public string ToTime { get; set; }

        public string Address { get; set; }

        public string CookerName { get; set; }

        public int CookerID { get; set; }

        public bool ProductsIncluded { get; set; }

        public int? AddressID { get; set; }

        public string AddressName { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Street { get; set; }

        public double Rating { get; set; }

        public Color OrderColor { get; set; }

        public bool IsRatingVisible { get; set; }

        public bool IsRatеVisible { get; set; }

        public void UpdateIsRated()
        {
            IsRatingVisible = true;
            OnPropertyChangedModel(nameof(IsRatingVisible));

            IsRatеVisible = false;
            OnPropertyChangedModel(nameof(IsRatеVisible));
        }
    }
}
