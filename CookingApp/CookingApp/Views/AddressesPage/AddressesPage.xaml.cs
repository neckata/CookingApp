using CookingApp.ViewModels.AddressesPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.AddressesPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressesPage : ContentPage
    {
        public AddressesPage()
        {
            InitializeComponent();
            this.BindingContext = new AddressesPageViewModel();
        }
    }
}