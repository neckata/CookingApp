using CookingApp.Enums;
using CookingApp.ViewModels.MainPage;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.UserPage
{
    public class CuisineTypeViewModel : ObservableViewModel
    {
        public CuisineTypeViewModel()
        {
            Cuisines = new List<CuisineViewModel>();
        }

        public CuisineTypeEnums Code { get; set; }

        public string Description { get; set; }

        public bool IsVisible {get;set;}

        public List<CuisineViewModel> Cuisines { get; set; }

        public ICommand ChangeVisibility
        {
            get
            {
                return new Command(() =>
                {
                    IsVisible = !IsVisible;
                    OnPropertyChangedModel(nameof(IsVisible));
                });
            }
        }
    }
}
