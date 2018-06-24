using CookingApp.Enums;
using CookingApp.ViewModels.MainPage;
using System.Collections.Generic;

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

        public List<CuisineViewModel> Cuisines { get; set; }
    }
}
