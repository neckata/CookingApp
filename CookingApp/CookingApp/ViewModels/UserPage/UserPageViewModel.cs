using Acr.UserDialogs;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.Resources;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.UserPage;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.UserPage
{
    public class UserPageViewModel : ObservableViewModel
    {
        public UserPageViewModel()
        {
            ReloadUser();
        }

        private UserModel _model = new UserModel();

        public bool IsBusy { get; set; }

        public bool NameValidation { get; set; }

        public bool EmailValidation { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool HasNotifications { get; set; }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (Validate())
                    {
                        IsBusy = true;
                        OnPropertyChangedModel(nameof(IsBusy));

                        bool isSucess = await _model.SaveUserInformation(new UserInformationDTO() { Email = Email, FirstName = Name, LastName = Family, Phone = Phone, HasNotifications = HasNotifications });

                        IsBusy = false;
                        OnPropertyChangedModel(nameof(IsBusy));

                        if (isSucess)
                        {
                            await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblSaveSuccess"));
                            ((MenuPageViewModel)(((MenuPage)PageTemplate.CurrentPage.Master).BindingContext)).ReloadUser();
                            await PageTemplate.CurrentPage.NavigateMainPageAsync();
                        }
                    }
                });
            }
        }

        public ICommand Navigate
        {
            get
            {
                return new Command(async () =>
                {
                    await PageTemplate.CurrentPage.NavigateAsync(new UserCookerPage() { Title = AppResources.ResourceManager.GetString("lblCookerInfo") });
                });
            }
        }
        public ICommand ChangeHasNotifications
        {
            get
            {
                return new Command(() =>
                {
                    HasNotifications = !HasNotifications;
                    OnPropertyChangedModel(nameof(HasNotifications));
                });
            }
        }

        private bool ValidateName()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameValidation = true;
                isValid = false;
            }
            else
                NameValidation = false;

            OnPropertyChangedModel(nameof(NameValidation));

            return isValid;
        }

        private bool ValidateEmail()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Email) || !Utility.ValidateEmail.EmailIsValid(Email))
            {
                EmailValidation = true;
                isValid = false;
            }
            else
                EmailValidation = false;

            OnPropertyChangedModel(nameof(EmailValidation));

            return isValid;
        }

        private bool Validate()
        {
            bool valid = true;
            if (!ValidateEmail()) valid = false;
            if (!ValidateName()) valid = false;
            return valid;
        }

        public void ReloadUser()
        {
            UserDTO user = _model.GetUser();
            Name = user.Name;
            Family = user.Family;
            Phone = user.Phone;
            Email = user.Email;
            HasNotifications = user.HasNotifications;

            OnPropertyChangedModel(nameof(HasNotifications));
            OnPropertyChangedModel(nameof(Name));
            OnPropertyChangedModel(nameof(Family));
            OnPropertyChangedModel(nameof(Phone));
            OnPropertyChangedModel(nameof(Email));
        }
    }
}
