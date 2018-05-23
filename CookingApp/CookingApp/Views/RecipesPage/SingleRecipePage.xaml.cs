using CookingApp.ViewModels.RecipesPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.RecipesPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleRecipePage : ContentPage
    {
        public SingleRecipePage(RecipeViewModel model)
        {
            InitializeComponent();
            this.BindingContext = new SingleRecipeViewModel(model);
        }
    }
}