using CookingApp.ViewModels.MainPage;
using System;

namespace CookingApp.ViewModels.ReceipsPage
{
    public class ReceipViewModel : ObservableViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int Description { get; set; }

        public int Image { get; set; }

        public DateTime TimeToCook { get; set; }
    }
}
