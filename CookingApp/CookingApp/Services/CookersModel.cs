using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.RecipesPage;
using CookingApp.ViewModels.UserPage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CookingApp.Services
{
    public class CookersModel
    {
        RestfulClient _rc = new RestfulClient();

        public async Task<List<CookerViewModel>> GetCookers(string cuisineCode)
        {
            List<CookerDTO> data = await _rc.GetDataAsync<List<CookerDTO>>(Enums.GetActionMethods.Cookers, cuisineCode);

            List<CookerViewModel> cookers = new List<CookerViewModel>();
            foreach (var item in data)
                cookers.Add(new CookerViewModel() { ID = item.ID, Description = item.Description, HoursPricing = item.HoursPricing, Image = item.Image, Name = item.Name, OrdersCount = item.OrdersCount, Rating = item.Rating });

            return cookers;
        }

        public async Task<CookerInformationViewModel> GetCookerInformation(int cookerID, DateTime fromDate)
        {
            CookerInformationViewModel cooker = new CookerInformationViewModel();

            CookerInformationDTO data = await _rc.GetDataAsync<CookerInformationDTO>(Enums.GetActionMethods.Cooker, string.Format("{0}?fromDate={1}", cookerID, fromDate.ToString("yyyy-MM-dd")));

            List<RecipeViewModel> recipes = new List<RecipeViewModel>();
            foreach (var item in data.Receipts)
                recipes.Add(new RecipeViewModel() { ID = item.ID, Image = item.Image, TimeToCook = item.TimeToCook, Title = item.Title, Portions = item.Portions });

            var cuisines = DataBase.Instance.Query<CuisineFilterDTO>();
            var cuisinesTypes = DataBase.Instance.Query<CuisineDTO>();

            ObservableCollection<CuisineTypeViewModel> list = new ObservableCollection<CuisineTypeViewModel>();

            foreach (var item in cuisines)
                list.Add(new CuisineTypeViewModel() { Code = item.Code, Description = item.Description, Cuisines = new List<CuisineViewModel>() });

            foreach (var item in data.Cuisines)
            {
                var cuisineType = cuisinesTypes.Single(x => x.Code == item);
                list.Single(x => x.Code == cuisineType.CuisineTypeCode).Cuisines.Add(new CuisineViewModel { Description = cuisineType.Description });
            }

            list = new ObservableCollection<CuisineTypeViewModel>(list.Where(x => x.Cuisines.Count > 0));

            List<TimeTableRowViewModel> timeTableRows = new List<TimeTableRowViewModel>();
            foreach (var item in data.Schedule)
            {
                var vm = new TimeTableRowViewModel()
                {
                    Day = item.Day,
                    Hours = new List<TimeTableCellViewModel>()
                };
                foreach (var cell in item.Hours)
                    vm.Hours.Add(new TimeTableCellViewModel()
                    {
                        IsTaken = cell.IsTaken,
                        IsWorking = cell.IsWorking,
                        Percentage = cell.Percentage
                    });
                timeTableRows.Add(vm);
            }

            cooker.TimeTable = timeTableRows;
            cooker.RecipesCanCook = recipes;
            cooker.CuisineTypes = list;

            return cooker;
        }

        public async Task<List<TimeTableRowViewModel>> GetTimeTable(int cookerID, DateTime fromDate)
        {
            var data = await _rc.GetDataAsync<List<TimeTableRowDTO>>(Enums.GetActionMethods.TimeTable, string.Format("{0}?fromDate={1}", cookerID, fromDate.ToString("yyyy-MM-dd")));

            List<TimeTableRowViewModel> list = new List<TimeTableRowViewModel>();
            foreach (var item in data)
            {
                var vm = new TimeTableRowViewModel()
                {
                    Day = item.Day,
                    Hours = new List<TimeTableCellViewModel>()
                };
                foreach (var cell in item.Hours)
                    vm.Hours.Add(new TimeTableCellViewModel()
                    {
                        IsTaken = cell.IsTaken,
                        IsWorking = cell.IsWorking,
                        Percentage = cell.Percentage
                    });
                list.Add(vm);
            }

            return list;
        }

        public bool MakeOrder(OrderDTO order)
        {
            //TODO
            DataBase.Instance.Add(order);
            return true;
        }

        public async Task<List<AddressesDTO>> GetAddresses()
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.GetAddresses, "");
            if (model.IsSuccessStatusCode)
            {
                List<AddressesDTO> data = JsonConvert.DeserializeObject<List<AddressesDTO>>(model.ResponseContent);
                return data;
            }
            else
            {
                return new List<AddressesDTO>();
            }
        }
    }
}
