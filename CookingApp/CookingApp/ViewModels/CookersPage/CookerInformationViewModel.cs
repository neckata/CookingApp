using CookingApp.ViewModels.RecipesPage;
using CookingApp.ViewModels.UserPage;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CookingApp.ViewModels.CookersPage
{
    public class CookerInformationViewModel
    {
        public List<TimeTableRowViewModel> TimeTable { get;set;}

        public List<RecipeViewModel> RecipesCanCook { get; set; }

        public ObservableCollection<CuisineTypeViewModel> CuisineTypes { get; set; }
    }
}
