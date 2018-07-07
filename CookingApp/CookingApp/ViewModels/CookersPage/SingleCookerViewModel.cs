using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.ViewModels.RecipesPage;
using CookingApp.ViewModels.UserPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.RecipesPage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.CookersPage
{
    public class SingleCookerViewModel : ObservableViewModel
    {
        public SingleCookerViewModel(CookerViewModel model)
        {
            Cooker = model;
            cookerID = model.ID;
            cookerName = model.Name;
            RecipesCanCook = _model.GetCookerRecipes(cookerID);
            CuisineTypes = _model.GetCookerCuisisnes(cookerID);
            TimeTable = _model.GetTimeTable(cookerID);
        }

        private int cookerID;

        private string cookerName;

        private CookersModel _model = new CookersModel();

        public CookerViewModel Cooker { get; set; }

        public List<TimeTableRowViewModel> TimeTable { get; set; }

        public List<RecipeViewModel> RecipesCanCook { get; set; }

        public ObservableCollection<CuisineTypeViewModel> CuisineTypes { get; set; }

        public ICommand Navigate
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    RecipeViewModel recipe = RecipesCanCook.FirstOrDefault(x => x.ID == para);
                    await PageTemplate.CurrentPage.NavigateAsync(new SingleRecipePage(recipe) { Title = recipe.Title });
                });
            }
        }

        public ICommand Order
        {
            get
            {
                return new Command(async () =>
                {
                    await PageTemplate.CurrentPage.NavigateAsync(new OrderPage(cookerID, cookerName) { Title = cookerName });
                });
            }
        }
    }
}
