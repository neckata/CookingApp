using CookingApp.ViewModels.CookersPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.CookersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CookersPage : ContentPage
    {
        public CookersPage()
        {
            InitializeComponent();
            this.BindingContext = new CookersPageViewModel();
        }
    }
}