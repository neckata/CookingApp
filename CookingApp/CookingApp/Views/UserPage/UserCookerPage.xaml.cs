using CookingApp.ViewModels.UserPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.UserPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCookerPage : ContentPage
    {
        public UserCookerPage()
        {
            InitializeComponent();
            this.BindingContext = new UserCookerPageViewModel();
        }
    }
}