using CookingApp.Helpers;
using CookingApp.Models;

namespace CookingApp.Services
{
    public class UserModel
    {
        public bool IsUserLogged()
        {
            UserDTO user = GetUser();
            return user.UserType == Enums.UserTypesEnum.Cooker;
        }

        public bool Login(string userName,string password)
        {
            UserDTO data = new UserDTO() { Name="Иван",Email="mail@gmail.com",Family="Иванов",OrdersCount=120,Phone="0686864",Image = "http://icons.iconarchive.com/icons/paomedia/small-n-flat/512/user-male-icon.png",
                Description = "Готвач с 20 години опит във веганската кухня, перфекционист", IsWorking = true, HoursPricing = 40,Rating=4 };

            UserDTO user = GetUser();
            user.UserType = Enums.UserTypesEnum.Cooker;

            user.Name = data.Name;
            user.Email = data.Email;
            user.OrdersCount = data.OrdersCount;
            user.Family = data.Family;
            user.Phone = data.Phone;
            user.Image = data.Image;
            user.Description = data.Description;
            user.IsWorking = data.IsWorking;
            user.HoursPricing = data.HoursPricing;
            user.Rating = data.Rating;
            DataBase.Instance.Update(user);

            return true;
        }

        public bool Logout()
        {
            UserDTO user = GetUser();
            user.UserType = Enums.UserTypesEnum.Client;
            DataBase.Instance.Update(user);
            return true;
        }

        public UserDTO GetUser()
        {
            return DataBase.Instance.Query<UserDTO>().First();
        }
    }
}
