using CookingApp.ViewModels.RecipesPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.RecipesPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipesPage : ContentPage
    {
        public RecipesPage()
        {
            InitializeComponent();
            this.BindingContext = new RecipesPageViewModel();
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((RecipesPageViewModel)this.BindingContext).FillCuisineFilters();
            this.cuisines.IsVisible = true;
            this.Recipes.IsVisible = false;
        }

        private void Picker_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            ((RecipesPageViewModel)this.BindingContext).FillRecipesData();
            this.Recipes.IsVisible = true;
        }
    }
}