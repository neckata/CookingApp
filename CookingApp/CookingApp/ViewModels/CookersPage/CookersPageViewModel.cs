using CookingApp.Enums;
using CookingApp.Models;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using System.Collections.Generic;

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

        public CuisineFilterDTO SelectedCuisineFilter { get; set; }

        public CuisineDTO SelectedCuisine { get; set; }

        public List<CuisineFilterDTO> CuisineFilters { get; set; }

        public List<CuisineDTO> Cuisines { get; set; }

        public List<CookerViewModel> Cookers { get; set; }

        public void FillCuisineFilters()
        {
            switch (SelectedCuisineFilter.Code)
            {
                case (CuisineTypeEnums.Type):
                    {
                        Cuisines = _recipesModel.GetAllCuisinesTypes();
                        break;
                    }
                case (CuisineTypeEnums.Country):
                    {
                        Cuisines = _recipesModel.GetAllCuisinesCountries();
                        break;
                    }
                case (CuisineTypeEnums.CookingType):
                    {
                        Cuisines = _recipesModel.GetAllCuisinesCookingType();
                        break;
                    }
                case (CuisineTypeEnums.Season):
                    {
                        Cuisines = _recipesModel.GetAllCuisinesSeasons();
                        break;
                    }
            }

            OnPropertyChangedModel(nameof(Cuisines));
            Cookers = new List<CookerViewModel>();
            OnPropertyChangedModel(nameof(Cookers));
        }

        public void FillCookers()
        {
            if (SelectedCuisine != null)
            {
                Cookers = _cookersModel.GetCookers(SelectedCuisine.Code);
                OnPropertyChangedModel(nameof(Cookers));
            }
        }
    }
}
