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


            TimeTable = new List<TimeTableRowViewModel>()
            {
                new TimeTableRowViewModel()
                {
                    Day="Понеделник",
                    Hours= new List<TimeTableCellViewModel>()
                    {
                       new TimeTableCellViewModel() { FromHour=0, ToHours=8, IsTaken=true, Percentage=0.3, IsWorking=false},
                       new TimeTableCellViewModel() { FromHour=8, ToHours=12, IsTaken=false, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=12, ToHours=14, IsTaken=true, Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=14, ToHours=18, IsTaken=false, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=18, ToHours=20, IsTaken=true,  Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=20, ToHours=24, IsTaken=true, Percentage=0.15, IsWorking=false},
                    }
                },
                new TimeTableRowViewModel()
                {
                    Day="Вторник",
                    Hours= new List<TimeTableCellViewModel>()
                    {
                       new TimeTableCellViewModel() { FromHour=0, ToHours=8, IsTaken=true, Percentage=0.3, IsWorking=false},
                       new TimeTableCellViewModel() { FromHour=8, ToHours=10, IsTaken=false, Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=10, ToHours=14, IsTaken=true, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=14, ToHours=16, IsTaken=false, Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=16, ToHours=20, IsTaken=true, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=20, ToHours=24, IsTaken=true, Percentage=0.15, IsWorking=false},
                    }
                },
                new TimeTableRowViewModel()
                {
                    Day="Сряда",
                    Hours= new List<TimeTableCellViewModel>()
                    {
                       new TimeTableCellViewModel() { FromHour=0, ToHours=8, IsTaken=true, Percentage=0.3, IsWorking=false},
                       new TimeTableCellViewModel() { FromHour=8, ToHours=12, IsTaken=false, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=12, ToHours=14, IsTaken=true, Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=14, ToHours=18, IsTaken=false, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=18, ToHours=20, IsTaken=true,  Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=20, ToHours=24, IsTaken=true, Percentage=0.15, IsWorking=false},
                    }
                },
                new TimeTableRowViewModel()
                {
                    Day="Четврътък",
                    Hours= new List<TimeTableCellViewModel>()
                    {
                       new TimeTableCellViewModel() { FromHour=0, ToHours=8, IsTaken=true, Percentage=0.3, IsWorking=false},
                       new TimeTableCellViewModel() { FromHour=8, ToHours=10, IsTaken=false, Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=10, ToHours=14, IsTaken=true, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=14, ToHours=16, IsTaken=false, Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=16, ToHours=20, IsTaken=true, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=20, ToHours=24, IsTaken=true, Percentage=0.15, IsWorking=false},
                    }
                },
                new TimeTableRowViewModel()
                {
                    Day="Петък",
                    Hours= new List<TimeTableCellViewModel>()
                    {
                       new TimeTableCellViewModel() { FromHour=0, ToHours=8, IsTaken=true, Percentage=0.3, IsWorking=false},
                       new TimeTableCellViewModel() { FromHour=8, ToHours=12, IsTaken=false, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=12, ToHours=14, IsTaken=true, Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=14, ToHours=18, IsTaken=false, Percentage=0.15, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=18, ToHours=20, IsTaken=true,  Percentage=0.075, IsWorking=true},
                       new TimeTableCellViewModel() { FromHour=20, ToHours=24, IsTaken=true, Percentage=0.15, IsWorking=false},
                    }
                },
                new TimeTableRowViewModel()
                {
                    Day="Събота",
                    Hours= new List<TimeTableCellViewModel>()
                    {
                       new TimeTableCellViewModel() { FromHour=0, ToHours=24, IsTaken=true, Percentage=1, IsWorking=false},
                    }
                },
                new TimeTableRowViewModel()
                {
                    Day="Неделя",
                    Hours= new List<TimeTableCellViewModel>()
                    {
                       new TimeTableCellViewModel() { FromHour=0, ToHours=24, IsTaken=true, Percentage=1, IsWorking=false},
                    }
                }
            };
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
