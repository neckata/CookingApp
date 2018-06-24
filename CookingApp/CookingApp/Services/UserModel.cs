using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.UserPage;
using System.Collections.Generic;
using System.Linq;

namespace CookingApp.Services
{
    public class UserModel
    {
        public bool IsUserLogged()
        {
            UserDTO user = GetUser();
            return user.UserType == Enums.UserTypesEnum.Cooker;
        }

        public bool Login(string userName, string password)
        {
            UserDTO data = new UserDTO()
            {
                Name = "Иван",
                Email = "mail@gmail.com",
                Family = "Иванов",
                OrdersCount = 120,
                Phone = "0686864",
                Image = "http://icons.iconarchive.com/icons/paomedia/small-n-flat/512/user-male-icon.png",
                Description = "Готвач с 20 години опит във веганската кухня, перфекционист",
                IsWorking = true,
                HoursPricing = 40,
                Rating = 4
            };

            List<CuisineDTO> userCuisines = new List<CuisineDTO>() {new CuisineDTO() { Code = "VEGAN" }, new CuisineDTO() { Code = "DIETIC" } , new CuisineDTO() { Code = "AU" }, new CuisineDTO() { Code = "NOCOOK" },
              new CuisineDTO() { Code = "VAREN" }, new CuisineDTO() { Code = "WINTER" }, new CuisineDTO() { Code = "SUMMER" }};

            foreach (var item in userCuisines)
            {
                var cuisine = DataBase.Instance.Query<CuisineDTO>().Single(x => x.Code == item.Code);
                cuisine.IsSelected = true;
                DataBase.Instance.Update(cuisine);
            }

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

            foreach(var item in DataBase.Instance.Query<CuisineDTO>())
            {
                item.IsSelected = false;
                DataBase.Instance.Update(item);
            }

            return true;
        }

        public UserDTO GetUser()
        {
            return DataBase.Instance.Query<UserDTO>().First();
        }

        public List<CuisineTypeViewModel> GetUserCuisineTypes()
        {
            List<CuisineTypeViewModel> data = new List<CuisineTypeViewModel>();
            foreach (var item in DataBase.Instance.Query<CuisineFilterDTO>())
            {
                var cuisineType = new CuisineTypeViewModel() { Description = item.Description, Code = item.Code };
                foreach (var item2 in DataBase.Instance.Query<CuisineDTO>().Where(x => x.CuisineTypeCode == item.Code))
                    cuisineType.Cuisines.Add(new CuisineViewModel() { Code = item2.Code, Description = item2.Description, IsSelected = item2.IsSelected });
                data.Add(cuisineType);
            }

            return data;
        }
    }
}
