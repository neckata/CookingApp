using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Interfaces;
using CookingApp.Models;
using CookingApp.Resources;
using CookingApp.ViewModels.UserPage;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CookingApp.Services
{
    public class UserModel
    {
        RestfulClient _rc = new RestfulClient();

        public bool IsUserLogged()
        {
            UserDTO user = GetUser();
            return user.UserType == Enums.UserTypesEnum.Cooker;
        }

        public async void RegisterUser()
        {
            UserDTO user = DataBase.Instance.Query<UserDTO>().First();
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

                ResponseModel model = await _rc.PostDataAsync(PostActionMethods.CreateUser, user);
            }
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

            List<CuisineSelectedDTO> userCuisines = new List<CuisineSelectedDTO>()
            {
                new CuisineSelectedDTO() { Code = "VEGAN" }, new CuisineSelectedDTO() { Code = "DIETIC" } ,  new CuisineSelectedDTO() { Code = "WINTER" },
                new CuisineSelectedDTO() { Code = "AU" }, new CuisineSelectedDTO() { Code = "NOCOOK" }, new CuisineSelectedDTO() { Code = "VAREN" }, new CuisineSelectedDTO() { Code = "SUMMER" }
            };

            ClearUserCuisines();

            foreach (var item in userCuisines)
                DataBase.Instance.Add(item);

            UserDTO user = GetUser();
            user.UserType = UserTypesEnum.Cooker;

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
            user.UserType = UserTypesEnum.Client;
            DataBase.Instance.Update(user);
            ClearUserCuisines();
            return true;
        }

        public void ClearUserCuisines()
        {
            var cuisines = DataBase.Instance.Query<CuisineSelectedDTO>();
            foreach (var item in cuisines)
                DataBase.Instance.Delete<CuisineSelectedDTO>(item.Code);
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
                    cuisineType.Cuisines.Add(new CuisineViewModel()
                    {
                        Code = item2.Code,
                        Description = item2.Description,
                        IsSelected = DataBase.Instance.Query<CuisineSelectedDTO>().SingleOrDefault(x => x.Code == item2.Code) != null
                    });
                data.Add(cuisineType);
            }

            return data;
        }
    }
}
