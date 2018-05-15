using CookingApp.Interfaces;
using CookingApp.ViewModels.MainPage;
using Plugin.Multilingual;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookingApp.Views.MainPage
{
    public class PageTemplate : MasterDetailPage
    {
        public PageTemplate()
        {
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo("bg");
            Master = new MenuPage();
            BindingContext = new MainPageViewModel();
            Detail = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Blue,
                BarTextColor = Color.White
            };
            CurrentPage = this;
        }  
        private bool IsNavigating { get; set; }
        private object lockOn = new object();
        private event EventHandler<NavigationEventArgs> _navigated;

        public static PageTemplate CurrentPage { get; set; }

        public static Type PreviousPageType { get; set; }

        public static event EventHandler<NavigationEventArgs> OnNavigated
        {
            add
            {
                CurrentPage.Navigated += value;
            }
            remove
            {
                CurrentPage.Navigated -= value;
            }
        }

        public async Task NavigateAsync(object id)
        {
            if (IsNavigating)
                return;
            IsNavigating = true;
            PreviousPageType = Detail.Navigation.NavigationStack.Last().GetType();

            Page newPage = new Page();

            if (newPage == null || newPage.GetType() == Detail.Navigation.NavigationStack.Last().GetType())
            {
                DisposeNavigation();
                return;
            }
            await Detail.Navigation.PushAsync(newPage);
            DisposeNavigation();
        }

        public async Task NavigateMainPageAsync()
        {
            if (IsNavigating)
                return;
            IsNavigating = true;
            PreviousPageType = Detail.Navigation.NavigationStack.Last().GetType();
            Page newPage = new MainPage();

            if (newPage == null || newPage.GetType() == Detail.Navigation.NavigationStack.Last().GetType())
            {
                DisposeNavigation();
                return;
            }
            await Detail.Navigation.PushAsync(newPage);
            DisposeNavigation();
        }

        private void DisposeNavigation()
        {
            IsNavigating = false;
        }

        public void HideMenuPage()
        {
            IsPresented = false;
        }

        protected override bool OnBackButtonPressed()
        {
            if (Detail.Navigation.NavigationStack.Count != 1)
                Detail.Navigation.PopAsync();
            else
            {
                if (Device.RuntimePlatform == Device.Android)
                    DependencyService.Get<ICloseProgram>().CloseApp();
            }
            return true;
        }

        public event EventHandler<NavigationEventArgs> Navigated
        {
            add
            {
                lock (lockOn)
                {
                    _navigated += value;
                }
            }
            remove
            {
                lock (lockOn)
                {
                    _navigated -= value;
                }
            }
        }
    }
}
