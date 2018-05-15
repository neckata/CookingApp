using CookingApp.Enums;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.NotificationsPage;
using CookingApp.Views.OrdersPage;
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
                case PageNavigateEnums.CookersPage: page = new CookersPage(); break;
                case PageNavigateEnums.MainPage: page = new MainPage(); break;
                case PageNavigateEnums.NotificationsPage: page = new NotificationsPage(); break;
                case PageNavigateEnums.OrdersPage: page = new OrdersPage(); break;
                case PageNavigateEnums.UserPage: page = new UserPage(); break;
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
