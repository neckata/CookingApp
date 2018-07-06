using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using System.Collections.Generic;

namespace CookingApp.ViewModels.RecipesPage
{
    public class SingleRecipeViewModel : ObservableViewModel
    {
        public SingleRecipeViewModel(RecipeViewModel model)
        {
            Recipe = model;
            FillData();
        }

        private RecipesModel _model = new RecipesModel();

        public RecipeViewModel Recipe { get; set; }

        public List<string> NecessaryIngredients { get; set; }

        public string HowToCook { get; set; }

        private  async void FillData()
        {
            RecipeViewModel data = await _model.GetOtherInformation(Recipe.ID);
            NecessaryIngredients = data.NecessaryIngredients;
            HowToCook = data.HowToCook;
            OnPropertyChangedModel(nameof(NecessaryIngredients));
            OnPropertyChangedModel(nameof(HowToCook));
        }
    }
}
