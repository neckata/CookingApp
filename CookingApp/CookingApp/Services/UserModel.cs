using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.Resources;
using CookingApp.ViewModels.AddressesPage;
using CookingApp.ViewModels.UserPage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CookingApp.Services
{
    public class UserModel
    {
        RestfulClient _rc = new RestfulClient();

        public bool IsUserLogged()
        {
            UserDTO user = GetUser();
            return user.UserType == UserTypesEnum.Cooker;
        }

        public async void RegisterUser()
        {
            UserDTO user = DataBase.Instance.Query<UserDTO>().First();
            RegisterUserDTO FCM = new RegisterUserDTO() { FcmId = user.FCM };
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.CreateUser, FCM);
            if (model.IsSuccessStatusCode)
            {
                user.IsRegistered = true;
                DataBase.Instance.Update(user);
                FillAddresses();
            }
        }

        public async Task<bool> SaveUserInformation(UserInformationDTO userInformation)
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.SaveUserInformation, userInformation);
            if (model.IsSuccessStatusCode)
            {
                UserDTO user = GetUser();
                user.Name = userInformation.FirstName;
                user.Phone = userInformation.Phone;
                user.Email = userInformation.Email;
                user.Family = userInformation.LastName;
                DataBase.Instance.Update(user);
            }

            return model.IsSuccessStatusCode;
        }

        public async void FillAddresses()
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.GetAddresses, "");
            if (model.IsSuccessStatusCode)
            {
                foreach (var item in DataBase.Instance.Query<AddressesDTO>())
                    DataBase.Instance.Delete<AddressesDTO>(item.Id);

                List<AddressesDTO> data = JsonConvert.DeserializeObject<List<AddressesDTO>>(model.ResponseContent);
                foreach (var item in data)
                {
                    DataBase.Instance.Add(item);
                }
            }
        }

        public ObservableCollection<AddressViewModel> GetAddresses()
        {
            ObservableCollection<AddressViewModel> list = new ObservableCollection<AddressViewModel>();
            List<AddressesDTO> data = DataBase.Instance.Query<AddressesDTO>().ToList();
            if (data.Count == 0)
            {
                list.Add(new AddressViewModel());
            }
            else
            {
                int counter = 0;
                foreach (var item in data)
                {
                    counter++;
                    list.Add(new AddressViewModel()
                    {
                        City = item.City,
                        Neighborhood = item.Neighborhood,
                        Street = item.Street,
                        Name = item.AddressName,
                        IDInBase = item.Id.Value,
                        ID = counter
                    });
                }
            }

            return list;
        }

        public async Task<int?> SaveAddress(AddressesDTO address)
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.SaveAddress, address);
            if (model.IsSuccessStatusCode)
            {
                if (address.Id.HasValue)
                {
                    DataBase.Instance.Update(address);
                    return address.Id.Value;
                }
                else
                {
                    address.Id = int.Parse(model.ResponseContent);
                    DataBase.Instance.Add(address);
                    return address.Id;
                }
            }

            return null;
        }

        public async Task<bool> DeleteAddress(int addressesID)
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.DeleteAddress, new IDDTO() { Id = addressesID });

            if (model.IsSuccessStatusCode)
                DataBase.Instance.Delete<AddressesDTO>(addressesID);

            return model.IsSuccessStatusCode;
        }

        public async Task<bool> SaveCooker(UserCookerDTO cooker)
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.SaveCooker, cooker);
            if (model.IsSuccessStatusCode)
            {
                UserDTO user = GetUser();
                user.Description = cooker.Description;
                user.HoursPricing = cooker.HoursPricing;
                DataBase.Instance.Update(user);

                ClearUserCuisines();
                foreach (var item in cooker.Cuisines)
                    DataBase.Instance.Add(new CuisineSelectedDTO() { Code = item });

                var timeTablesBase = DataBase.Instance.Query<UserTimeTableDTO>();
                foreach (var item in cooker.TimeTable)
                {
                    var time = timeTablesBase.First(x => x.Code == item.Code);
                    time.IsWorking = item.IsWorking;
                    time.From = item.From;
                    time.To = item.To;
                    DataBase.Instance.Update(time);
                }
            }

            return model.IsSuccessStatusCode;
        }

        public async Task<bool> Login(string userName, string password)
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.Login, new UserLoginDTO() { UserName = userName, Password = password });
            if (model.IsSuccessStatusCode)
            {
                UserDTO data = JsonConvert.DeserializeObject<UserDTO>(model.ResponseContent);

                var timeTablesBase = DataBase.Instance.Query<UserTimeTableDTO>();

                if (timeTablesBase.Count() == 0)
                {
                    DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.MONDAY, Day = AppResources.ResourceManager.GetString("monday") });
                    DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.TUESDAY, Day = AppResources.ResourceManager.GetString("tuesday") });
                    DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.WEDNESDAY, Day = AppResources.ResourceManager.GetString("wednesday") });
                    DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.THURSDAY, Day = AppResources.ResourceManager.GetString("thursday") });
                    DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.FRIDAY, Day = AppResources.ResourceManager.GetString("friday") });
                    DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.SATURDAY, Day = AppResources.ResourceManager.GetString("saturday") });
                    DataBase.Instance.Add(new UserTimeTableDTO() { Code = WeekDaysEnum.SUNDAY, Day = AppResources.ResourceManager.GetString("sunday") });
                    timeTablesBase = DataBase.Instance.Query<UserTimeTableDTO>();
                }

                foreach (var item in timeTablesBase)
                {
                    var time = data.TimeTable.First(x => x.Code == item.Code);
                    item.IsWorking = time.IsWorking;
                    item.From = time.From;
                    item.To = time.To;
                    DataBase.Instance.Update(item);
                }

                foreach (var item in data.Cuisines)
                    DataBase.Instance.Add(new CuisineSelectedDTO() { Code = item });

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
                user.UserName = userName;
                user.Password = password;
                DataBase.Instance.Update(user);

                FillAddresses();
            }

            return model.IsSuccessStatusCode;
        }

        public async Task<bool> Logout()
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.Logout, string.Empty);
            if (model.IsSuccessStatusCode)
            {
                UserDTO userData = JsonConvert.DeserializeObject<UserDTO>(model.ResponseContent);

                UserDTO user = GetUser();
                user.UserType = UserTypesEnum.Client;
                user.Name = userData.Name;
                user.Family = userData.Family;
                user.Phone = userData.Phone;
                user.Email = userData.Email;
                user.UserName = "Anonymous";
                user.Password = string.Empty;
                DataBase.Instance.Update(user);

                ClearUserCuisines();

                FillAddresses();
            }
 
            return model.IsSuccessStatusCode;
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
                    From = item.From.HasValue ? item.From.Value : new TimeSpan(),
                    IsWorking = item.IsWorking,
                    To = item.To.HasValue ? item.To.Value : new TimeSpan(),
                    Code = item.Code
                });

            return data;
        }
    }
}
