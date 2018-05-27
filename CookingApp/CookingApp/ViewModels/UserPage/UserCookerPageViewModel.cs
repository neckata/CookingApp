using CookingApp.Helpers;
using CookingApp.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.UserPage
{
    public class UserCookerPageViewModel
    {
        public UserCookerPageViewModel()
        {
            
        }

        public string Password { get; set; }

        public string UserName { get; set; }

        public ICommand Login
        {
            get
            {
                return new Command(() =>
                {
                    UserDTO user = DataBase.Instance.Query<UserDTO>().First();
                });
            }
        }
    }
}
