using CookingApp.Enums;
using CookingApp.Resources;
using CookingApp.Views.AddressesPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.NotificationsPage;
using CookingApp.Views.OrdersPage;
using CookingApp.Views.RecipesPage;
using CookingApp.Views.UserPage;
using System;
using System.Globalization;
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
                case PageNavigateEnums.RecipesPage: page = new RecipesPage() { Title = AppResources.ResourceManager.GetString("recipesPageTitle") }; break;
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

        public static int GetWeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }
    }
}
