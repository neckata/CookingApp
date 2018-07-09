using CookingApp.Models;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.CookersPage
{
    public class CookersPageViewModel : ObservableViewModel
    {
        public CookersPageViewModel()
        {
            Cuisines = new List<CuisineDTO>();
            Cookers = new List<CookerViewModel>();
            CuisineFilters = _recipesModel.GetCuisineFilters();
        }

        private RecipesModel _recipesModel = new RecipesModel();

        private CookersModel _cookersModel = new CookersModel();

        public bool IsBusy { get; set; }

        public CuisineFilterDTO SelectedCuisineFilter { get; set; }

        public CuisineDTO SelectedCuisine { get; set; }

        public List<CuisineFilterDTO> CuisineFilters { get; set; }

        public List<CuisineDTO> Cuisines { get; set; }

        public List<CookerViewModel> Cookers { get; set; }

        public void FillCuisineFilters()
        {
            Cuisines = _recipesModel.GetCuisines(SelectedCuisineFilter.Code);
            OnPropertyChangedModel(nameof(Cuisines));
            Cookers = new List<CookerViewModel>();
            OnPropertyChangedModel(nameof(Cookers));
        }

        public async void FillCookers()
        {
            if (SelectedCuisine != null)
            {
                IsBusy = true;
                OnPropertyChangedModel(nameof(IsBusy));

                Cookers = await _cookersModel.GetCookers(SelectedCuisine.Code);
                OnPropertyChangedModel(nameof(Cookers));

                IsBusy = false;
                OnPropertyChangedModel(nameof(IsBusy));
            }
        }

        public ICommand Navigate
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    CookerViewModel cooker = Cookers.FirstOrDefault(x => x.ID == para);
                    await PageTemplate.CurrentPage.NavigateAsync(new SingleCookerPage(cooker) { Title = cooker.Name });
                });
            }
        }
    }
}
