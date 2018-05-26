using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Interfaces;
using CookingApp.Models;
using CookingApp.Views.MainPage;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.MainPage
{
    public class MainPageViewModel : ObservableViewModel
    {
        public MainPageViewModel()
        {
        }

        public ICommand Navigate
        {
            get
            {
                return new Command<string>(async (para) =>
                {
                    PageNavigateEnums page = Utility.ParseEnum<PageNavigateEnums>(para);
                    await PageTemplate.CurrentPage.NavigateAsync(Utility.PageParser(page));
                });
            }
        }
    }
}
