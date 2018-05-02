using CookingApp.ViewModels.MainPage;
using Xamarin.Forms;

namespace CookingApp.Views.MainPage
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            this.BindingContext = new MenuPageViewModel();
        }
    }
}