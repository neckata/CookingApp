using CookingApp.ViewModels.MainPage;

namespace CookingApp.ViewModels.UserPage
{
    public class CuisineViewModel : ObservableViewModel
    {
        public string Code { get; set; }

        public bool IsSelected { get; set; }

        public string Description { get; set; }
    }
}
