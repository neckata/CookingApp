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
            NecessaryIngredients = _model.GetNecessaryIngredients(model.ID);
            HowToCook = _model.GetHowToCook(model.ID);
        }

        private RecipesModel _model = new RecipesModel();

        public RecipeViewModel Recipe { get; set; }

        public List<string> NecessaryIngredients { get; set; }

        public string HowToCook { get; set; }
    }
}
