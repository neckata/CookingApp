using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Interfaces;
using CookingApp.Models;
using CookingApp.Resources;
using CookingApp.Views.MainPage;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.MainPage
{
    public class MenuPageViewModel : ObservableViewModel
    {
        public MenuPageViewModel()
        {
            ReloadUser();
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

        public void ReloadUser()
        {
            UserDTO user = DataBase.Instance.Query<UserDTO>().FirstOrDefault();
            if (user == null)
            {
                user = new UserDTO
                {
                    UserType = UserTypesEnum.Client,
                    UserName = "AnonymousUser",
                    Email = "email@mail.com",
                    Name = AppResources.ResourceManager.GetString("lblAnonymousUser"),
                    IMEI = DependencyService.Get<IDevice>().GetIdentifier(),
                    FCM = "FCM"
                };
                DataBase.Instance.Add(user);
            }

            Name = string.Format("{0} {1}", user.Name, user.Family);
            Email = user.Email;
            OnPropertyChangedModel(nameof(Name));
            OnPropertyChangedModel(nameof(Email));
        }
    }
}
