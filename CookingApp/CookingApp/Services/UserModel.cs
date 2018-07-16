using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.Resources;
using CookingApp.ViewModels.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (!user.IsRegistered)
            {
                RegisterUserDTO FCM = new RegisterUserDTO() { FcmID = user.FCM };
                ResponseModel model = await _rc.PostDataAsync(PostActionMethods.CreateUser, FCM);
                if (model.IsSuccessStatusCode)
                {
                    user.IsRegistered = true;
                    DataBase.Instance.Update(user);
                }
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
                Rating = 4,
                UserCuisines = new List<CuisineSelectedDTO>()
                {
                    new CuisineSelectedDTO() { Code = "VEGAN" }, new CuisineSelectedDTO() { Code = "DIETIC" } ,  new CuisineSelectedDTO() { Code = "WINTER" },
                    new CuisineSelectedDTO() { Code = "AU" }, new CuisineSelectedDTO() { Code = "NOCOOK" }, new CuisineSelectedDTO() { Code = "VAREN" }, new CuisineSelectedDTO() { Code = "SUMMER" }
                },
                TimeTables = new List<UserTimeTableDTO>()
                {
                    new UserTimeTableDTO(){Code = WeekDaysEnum.MODAY,IsWorking=true, From=new TimeSpan(8,0,0),To=new TimeSpan(16,0,0)},
                    new UserTimeTableDTO(){Code = WeekDaysEnum.TUESDAY,IsWorking=true, From=new TimeSpan(10,0,0),To=new TimeSpan(18,0,0)},
                    new UserTimeTableDTO(){Code = WeekDaysEnum.WEDNESDAY},
                    new UserTimeTableDTO(){Code = WeekDaysEnum.TUESDAY},
                    new UserTimeTableDTO(){Code = WeekDaysEnum.FRIDAY},
                    new UserTimeTableDTO(){Code = WeekDaysEnum.SATURDAY,IsWorking=true, From=new TimeSpan(10,0,0),To=new TimeSpan(18,0,0)},
                    new UserTimeTableDTO(){Code = WeekDaysEnum.SUNDAY}
                }
            };

            var timeTablesBase = DataBase.Instance.Query<UserTimeTableDTO>();

            if (timeTablesBase.Count() == 0)
            {
                DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.MODAY, Day = AppResources.ResourceManager.GetString("monday") });
                DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.TUESDAY, Day = AppResources.ResourceManager.GetString("tuesday") });
                DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.WEDNESDAY, Day = AppResources.ResourceManager.GetString("wednesday") });
                DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.THURDSAY, Day = AppResources.ResourceManager.GetString("thursday") });
                DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.FRIDAY, Day = AppResources.ResourceManager.GetString("friday") });
                DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.SATURDAY, Day = AppResources.ResourceManager.GetString("saturday") });
                DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.SUNDAY, Day = AppResources.ResourceManager.GetString("sunday") });
                timeTablesBase = DataBase.Instance.Query<UserTimeTableDTO>();
            }

            foreach (var item in data.TimeTables)
            {
                var time = timeTablesBase.First(x => x.Code == item.Code);
                time.IsWorking = item.IsWorking;
                time.From = item.From;
                time.To = item.To;
                DataBase.Instance.Update(time);
            }

            foreach (var item in data.UserCuisines)
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

        public List<UserTimeTableViewModel> GetTimeTable()
        {
            List<UserTimeTableViewModel> data = new List<UserTimeTableViewModel>();

            foreach (var item in DataBase.Instance.Query<UserTimeTableDTO>())
                data.Add(new UserTimeTableViewModel()
                {
                    Day = item.Day,
                    From = item.From,
                    IsWorking = item.IsWorking,
                    To = item.To,
                    Code = item.Code
                });

            return data;
        }
    }
}
