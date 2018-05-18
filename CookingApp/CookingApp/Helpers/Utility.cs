using CookingApp.Enums;
using CookingApp.Resources;
using CookingApp.Views.AddressesPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.NotificationsPage;
using CookingApp.Views.OrdersPage;
using CookingApp.Views.ReceipsPage;
using CookingApp.Views.UserPage;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CookingApp.Helpers
{
    public static class Utility
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static ContentPage PageParser(PageNavigateEnums data)
        {
            ContentPage page = new MainPage();
            switch (data)
            {
                case PageNavigateEnums.CookersPage: page = new CookersPage() {Title=AppResources.ResourceManager.GetString("cookersPageTitle") }; break;
                case PageNavigateEnums.MainPage: page = new MainPage() { Title = AppResources.ResourceManager.GetString("mainPageTitle") }; break;
                case PageNavigateEnums.NotificationsPage: page = new NotificationsPage() { Title = AppResources.ResourceManager.GetString("notifactionsPageTitle") }; break;
                case PageNavigateEnums.OrdersPage: page = new OrdersPage() { Title = AppResources.ResourceManager.GetString("ordersPageTitle") }; break;
                case PageNavigateEnums.UserPage: page = new UserPage() { Title = AppResources.ResourceManager.GetString("userPageTitle") }; break;
                case PageNavigateEnums.AddressesPage: page = new AddressesPage() { Title = AppResources.ResourceManager.GetString("addressesPageTitle") }; break;
                case PageNavigateEnums.RecipesPage: page = new ReceipsPage() { Title = AppResources.ResourceManager.GetString("recipesPageTitle") }; break;
            }
            return page;
        }

        public static class ValidateEmail
        {
            static Regex ValidEmailRegex = CreateValidEmailRegex();

            private static Regex CreateValidEmailRegex()
            {
                string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                    + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

                return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
            }

            internal static bool EmailIsValid(string emailAddress)
            {
                if (string.IsNullOrEmpty(emailAddress))
                    return false;

                return ValidEmailRegex.IsMatch(emailAddress);
            }
        }
    }
}
