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

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((CookersPageViewModel)this.BindingContext).FillCuisineFilters();
            this.Cuisines.IsVisible = true;
            this.Cookers.IsVisible = false;
        }

        private void Picker_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            ((CookersPageViewModel)this.BindingContext).FillCookers();
            this.Cuisines.IsVisible = true;
            this.Cookers.IsVisible = true;
        }
    }
}