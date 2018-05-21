﻿using CookingApp.Models;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using System.Collections.Generic;

namespace CookingApp.ViewModels.ReceipsPage
{
    public class ReceipsPageViewModel : ObservableViewModel
    {
        public ReceipsPageViewModel()
        {
            Receips = new List<ReceipViewModel>();
            Cuisines = new List<CuisineDTO>();
            CuisineFilters = model.GetCuisineFilters();
        }

        private CuisinesModel model = new CuisinesModel();

        public CuisineFilterDTO SelectedCuisineFilter { get; set; }

        public CuisineDTO SelectedCuisine { get; set; }

        public List<CuisineFilterDTO> CuisineFilters { get; set; }

        public List<CuisineDTO> Cuisines { get; set; }

        public List<ReceipViewModel> Receips { get; set; }

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
        }

        public void FillReceipsData()
        {
            Receips = model.GetAllReceips();
            OnPropertyChangedModel(nameof(Receips));
        }
    }
}
