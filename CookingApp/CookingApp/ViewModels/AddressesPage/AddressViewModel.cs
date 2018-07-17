using CookingApp.ViewModels.MainPage;

namespace CookingApp.ViewModels.AddressesPage
{
    public class AddressViewModel : ObservableViewModel
    {
        public int ID { get; set; }

        public int IDInBase { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }
    }
}
