using CookingApp.Models;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.Views.MainPage;
using CookingApp.Helpers;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using CookingApp.Resources;
using System.Collections.ObjectModel;
using System.Linq;

namespace CookingApp.ViewModels.UserPage
{
    public class UserCookerPageViewModel : ObservableViewModel
    {
        public UserCookerPageViewModel()
        {
            IsUserLogged = _model.IsUserLogged();
            if (IsUserLogged)
                ReloadUser();
        }

        private UserModel _model = new UserModel();

        public string Password { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public bool IsWorking { get; set; }

        public double HoursPricing { get; set; }

        public double Rating { get; set; }

        public int OrdersCount { get; set; }

        public string Image { get; set; }

        public bool IsUserLogged { get; set; }

        public ObservableCollection<CuisineTypeViewModel> CuisineTypes { get; set; }

        public ICommand Login
        {
            get
            {
                return new Command(() =>
                {
                    bool isLogged = _model.Login(UserName, Password);
                    if (isLogged)
                    {
                        IsUserLogged = true;
                        OnPropertyChangedModel(nameof(IsUserLogged));
                        ((MenuPageViewModel)(((MenuPage)PageTemplate.CurrentPage.Master).BindingContext)).ReloadUser();
                        ((UserPageViewModel)(((Views.UserPage.UserPage)PageTemplate.CurrentPage.Detail.Navigation.NavigationStack[PageTemplate.CurrentPage.Detail.Navigation.NavigationStack.Count - 2])
                        .BindingContext)).ReloadUser();
                        ReloadUser();
                    }
                });
            }
        }

        public ICommand Logout
        {
            get
            {
                return new Command(() =>
                {
                    bool isLogout = _model.Logout();
                    if (isLogout)
                    {
                        IsUserLogged = false;
                        OnPropertyChangedModel(nameof(IsUserLogged));
                        ((MenuPageViewModel)(((MenuPage)PageTemplate.CurrentPage.Master).BindingContext)).ReloadUser();
                        ((UserPageViewModel)(((Views.UserPage.UserPage)PageTemplate.CurrentPage.Detail.Navigation.NavigationStack[PageTemplate.CurrentPage.Detail.Navigation.NavigationStack.Count - 2])
                        .BindingContext)).ReloadUser();
                        ReloadUser();
                    }
                });
            }
        }

        public ICommand Save
        {
            get
            {
                return new Command(async() =>
                {
                    UserDTO user = _model.GetUser();
                    user.Description = Description;
                    user.HoursPricing = HoursPricing;
                    DataBase.Instance.Update(user);

                    _model.ClearUserCuisines();
                    foreach(var item in CuisineTypes)
                    {
                        foreach (var item2 in item.Cuisines.Where(x => x.IsSelected))
                            DataBase.Instance.Add(new CuisineSelectedDTO() { Code=item2.Code});
                    }

                    await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblSaveSuccess"));
                });
            }
        }

        public ICommand ChangePicture
        {
            get
            {
                return new Command(() =>
                {

                });
            }
        }

        private void ReloadUser()
        {
            UserDTO user = _model.GetUser();
            IsWorking = user.IsWorking;
            HoursPricing = user.HoursPricing;
            Image = user.Image;
            Description = user.Description;
            Rating = user.Rating;
            OrdersCount = user.OrdersCount;

            CuisineTypes = new ObservableCollection<CuisineTypeViewModel>(_model.GetUserCuisineTypes());

            OnPropertyChangedModel(nameof(CuisineTypes));
            OnPropertyChangedModel(nameof(IsWorking));
            OnPropertyChangedModel(nameof(OrdersCount));
            OnPropertyChangedModel(nameof(Rating));
            OnPropertyChangedModel(nameof(HoursPricing));
            OnPropertyChangedModel(nameof(Image));
            OnPropertyChangedModel(nameof(Description));
        }
    }
}
