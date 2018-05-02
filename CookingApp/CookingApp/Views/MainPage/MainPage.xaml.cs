using CookingApp.ViewModels.MainPage;
using Xamarin.Forms;

namespace CookingApp.Views.MainPage
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }
    }
}