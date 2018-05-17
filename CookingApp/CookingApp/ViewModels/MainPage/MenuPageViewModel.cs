using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Views.MainPage;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.MainPage
{
    public class MenuPageViewModel : ObservableViewModel
    {
        public MenuPageViewModel()
        {
           
        }

        public string Name { get; set; }
        
        public string Email { get; set; }

        public ICommand Navigate
        {
            get
            {
                return new Command<string>(async (para) =>
                {
                    PageNavigateEnums page = Utility.ParseEnum<PageNavigateEnums>(para);
                    PageTemplate.CurrentPage.IsPresented = false;
                    await PageTemplate.CurrentPage.NavigateAsync(Utility.PageParser(page));
                });
            }
        }
    }
}
