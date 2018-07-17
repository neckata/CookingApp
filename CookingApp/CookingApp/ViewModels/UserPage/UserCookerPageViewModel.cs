﻿using CookingApp.Models;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.Views.MainPage;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using CookingApp.Resources;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

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

        public ObservableCollection<UserTimeTableViewModel> TimeTable { get; set; }

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
                return new Command(async () =>
                {
                    List<string> cuisines = new List<string>();
                    foreach (var item in CuisineTypes)
                    {
                        foreach (var item2 in item.Cuisines.Where(x => x.IsSelected))
                            cuisines.Add(item2.Code);
                    }

                    List<UserTimeTableDTO> timeTable = new List<UserTimeTableDTO>();
                    foreach (var item in TimeTable)
                    {
                        timeTable.Add(new UserTimeTableDTO()
                        {
                            Code = item.Code,
                            Day = item.Day,
                            To = item.To,
                            From = item.From,
                            IsWorking = item.IsWorking
                        });
                    }

                    bool isSuccess = await _model.SaveCooker(new UserCookerDTO()
                    {
                        Description = Description,
                        HoursPricing = HoursPricing,
                        Image = Image,
                        TimeTable = timeTable,
                        Cuisines = cuisines
                    });

                    if (isSuccess)
                    {
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblSaveSuccess"));
                        await PageTemplate.CurrentPage.NavigateBack();
                    }
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
            TimeTable = new ObservableCollection<UserTimeTableViewModel>(_model.GetTimeTable());

            OnPropertyChangedModel(nameof(CuisineTypes));
            OnPropertyChangedModel(nameof(TimeTable));
            OnPropertyChangedModel(nameof(IsWorking));
            OnPropertyChangedModel(nameof(OrdersCount));
            OnPropertyChangedModel(nameof(Rating));
            OnPropertyChangedModel(nameof(HoursPricing));
            OnPropertyChangedModel(nameof(Image));
            OnPropertyChangedModel(nameof(Description));
        }
    }
}
