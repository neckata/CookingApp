﻿using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.ViewModels.RecipesPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.RecipesPage;
using System.Collections.Generic;
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
            RecipesCanCook = _model.GetCookerRecipes(model.ID);
        }

        private CookersModel _model = new CookersModel();

        public CookerViewModel Cooker { get; set; }

        public List<RecipeViewModel> RecipesCanCook { get; set; }

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
    }
}