using CookingApp.Models;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using System.Collections.Generic;

namespace CookingApp.ViewModels.RecipesPage
{
    public class RecipesPageViewModel : ObservableViewModel
    {
        public RecipesPageViewModel()
        {
            Recipes = new List<RecipeViewModel>();
            Cuisines = new List<CuisineDTO>();
            CuisineFilters = model.GetCuisineFilters();
        }

        private RecipesModel model = new RecipesModel();

        public CuisineFilterDTO SelectedCuisineFilter { get; set; }

        public CuisineDTO SelectedCuisine { get; set; }

        public List<CuisineFilterDTO> CuisineFilters { get; set; }

        public List<CuisineDTO> Cuisines { get; set; }

        public List<RecipeViewModel> Recipes { get; set; }

        public void FillCuisineFilters()
        {
            switch (SelectedCuisineFilter.Code)
            {
                case ("TYPE"):
                    {
                        Cuisines = model.GetAllCuisinesTypes();
                        break;
                    }
                case ("COUNTRY"):
                    {
                        Cuisines = model.GetAllCuisinesCountries();
                        break;
                    }
                case ("COOKINGTYPE"):
                    {
                        Cuisines = model.GetAllCuisinesCookingType();
                        break;
                    }
                case ("SEASON"):
                    {
                        Cuisines = model.GetAllCuisinesSeasons();
                        break;
                    }
            }

            OnPropertyChangedModel(nameof(Cuisines));
            Recipes = new List<RecipeViewModel>();
            OnPropertyChangedModel(nameof(Recipes));
        }

        public void FillRecipesData()
        {
            if (SelectedCuisine != null)
            {
                Recipes = model.GetAllRecipes(SelectedCuisine.Code);
                OnPropertyChangedModel(nameof(Recipes));
            }
        }
    }
}
