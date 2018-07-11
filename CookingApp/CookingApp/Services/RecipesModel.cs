using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.RecipesPage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingApp.Services
{
    public class RecipesModel
    {
        RestfulClient _rc = new RestfulClient();

        public List<CuisineDTO> GetCuisines(CuisineTypeEnums type)
        {
            return new List<CuisineDTO>(DataBase.Instance.Query<CuisineDTO>().Where(x => x.CuisineTypeCode == type).OrderBy(x=>x.Description));
        }

        public List<CuisineFilterDTO> GetCuisineFilters()
        {
            return new List<CuisineFilterDTO>(DataBase.Instance.Query<CuisineFilterDTO>().OrderBy(x => x.Description));
        }

        public async Task<List<RecipeViewModel>> GetAllRecipes(string cuisineCode)
        {
            List<RecipeDTO> data = await _rc.GetDataAsync<List<RecipeDTO>>(GetActionMethods.Receipts, cuisineCode);

            List<RecipeViewModel> recipes = new List<RecipeViewModel>();
            foreach (var item in data)
                recipes.Add(new RecipeViewModel() { ID = item.ID, Image = item.Image, TimeToCook = item.TimeToCook, Title = item.Title, Portions = item.Portions });

            return recipes;
        }

        public async Task<RecipeViewModel> GetOtherInformation(int recipeID)
        {
            RecipeDTO data = await _rc.GetDataAsync<RecipeDTO>(GetActionMethods.RecipeInformation, recipeID.ToString());
            return new RecipeViewModel() { NecessaryIngredients = data.NecessaryIngredients, HowToCook = data.HowToCook };
        }
    }
}
