using CookingApp.Models;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.RecipesPage;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.RecipesPage
{
    public class RecipesPageViewModel : ObservableViewModel
    {
        public RecipesPageViewModel()
        {
            Recipes = new List<RecipeViewModel>();
            Cuisines = new List<CuisineDTO>();
            CuisineFilters = _model.GetCuisineFilters();
        }

        private RecipesModel _model = new RecipesModel();

        public CuisineFilterDTO SelectedCuisineFilter { get; set; }

        public CuisineDTO SelectedCuisine { get; set; }

        public List<CuisineFilterDTO> CuisineFilters { get; set; }

        public List<CuisineDTO> Cuisines { get; set; }

        public List<RecipeViewModel> Recipes { get; set; }

        public void FillCuisineFilters()
        {
            Cuisines = _model.GetCuisines(SelectedCuisineFilter.Code);
            OnPropertyChangedModel(nameof(Cuisines));
            Recipes = new List<RecipeViewModel>();
            OnPropertyChangedModel(nameof(Recipes));
        }

        public void FillRecipesData()
        {
            if (SelectedCuisine != null)
            {
                Recipes = _model.GetAllRecipes(SelectedCuisine.Code);
                OnPropertyChangedModel(nameof(Recipes));
            }
        }

        public ICommand Navigate
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    RecipeViewModel recipe = Recipes.FirstOrDefault(x => x.ID == para);
                    await PageTemplate.CurrentPage.NavigateAsync(new SingleRecipePage(recipe) { Title = recipe.Title });
                });
            }
        }
    }
}
