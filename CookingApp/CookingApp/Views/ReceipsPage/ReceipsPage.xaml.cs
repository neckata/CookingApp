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

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((ReceipsPageViewModel)this.BindingContext).FillCuisineFilters();
            this.cuisines.IsVisible = true;
            this.receips.IsVisible = false;
        }

        private void Picker_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            ((ReceipsPageViewModel)this.BindingContext).FillReceipsData();
            this.receips.IsVisible = true;
        }
    }
}