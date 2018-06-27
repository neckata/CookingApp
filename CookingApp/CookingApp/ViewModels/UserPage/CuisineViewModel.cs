using CookingApp.ViewModels.MainPage;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.UserPage
{
    public class CuisineViewModel : ObservableViewModel
    {
        public string Code { get; set; }

        public bool IsSelected { get; set; }

        public string Description { get; set; }

        public ICommand ChangeIsSelected
        {
            get
            {
                return new Command(() =>
                {
                    IsSelected = !IsSelected;
                    OnPropertyChangedModel(nameof(IsSelected));
                });
            }
        }
    }
}
