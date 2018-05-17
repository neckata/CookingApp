using CookingApp.ViewModels.ReceipsPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.ReceipsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceipsPage : ContentPage
    {
        public ReceipsPage()
        {
            InitializeComponent();
            this.BindingContext = new ReceipsPageViewModel();
        }
    }
}