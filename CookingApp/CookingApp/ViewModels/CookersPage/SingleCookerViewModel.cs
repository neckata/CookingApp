using CookingApp.Helpers;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.ViewModels.RecipesPage;
using CookingApp.ViewModels.UserPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.RecipesPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
            FromDate = Utility.FirstDateOfWeek(DateTime.Today.Year, Utility.GetWeekOfYear(DateTime.Now), CultureInfo.CurrentCulture);
            ToDate = FromDate.AddDays(6);
            FilldData(FromDate, ToDate);
        }

        private int cookerID;

        private string cookerName;

        public bool IsBusy { get; set; }

        private CookersModel _model = new CookersModel();

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

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

        public ICommand LeftDate
        {
            get
            {
                return new Command(() =>
                {
                    GetTimeTable(-7);
                });
            }
        }

        public ICommand RightDate
        {
            get
            {
                return new Command(() =>
                {
                    GetTimeTable(7);
                });
            }
        }

        private async void FilldData(DateTime fromDate, DateTime toDate)
        {
            IsBusy = true;
            OnPropertyChangedModel(nameof(IsBusy));

            var cookerInformation = await _model.GetCookerInformation(cookerID, fromDate, toDate);
            TimeTable = cookerInformation.TimeTable;
            RecipesCanCook = cookerInformation.RecipesCanCook;
            CuisineTypes = cookerInformation.CuisineTypes;

            OnPropertyChangedModel(nameof(TimeTable));
            OnPropertyChangedModel(nameof(RecipesCanCook));
            OnPropertyChangedModel(nameof(CuisineTypes));

            IsBusy = false;
            OnPropertyChangedModel(nameof(IsBusy));
        }

        public void GetTimeTable(int weekChange)
        {
            IsBusy = true;
            OnPropertyChangedModel(nameof(IsBusy));

            FromDate = FromDate.AddDays(weekChange);
            ToDate = FromDate.AddDays(weekChange);
            TimeTable = _model.GetTimeTable(cookerID, FromDate, ToDate);

            OnPropertyChangedModel(nameof(TimeTable));
            OnPropertyChangedModel(nameof(FromDate));
            OnPropertyChangedModel(nameof(ToDate));

            IsBusy = false;
            OnPropertyChangedModel(nameof(IsBusy));
        }
    }
}
