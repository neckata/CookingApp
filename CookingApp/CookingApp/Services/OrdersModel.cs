using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.OrdersPage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookingApp.Services
{
    public class OrdersModel
    {
        private RestfulClient _rc = new RestfulClient();

        public async Task<ObservableCollection<SingleOrderViewModel>> GetOrders()
        {
            var list = new ObservableCollection<SingleOrderViewModel>();

            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.Orders, string.Empty);

            if (model.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<List<OrderDTO>>(model.ResponseContent);
                foreach (var item in data)
                {
                    AddressesDTO address;
                    if (item.AddressID.HasValue)
                        address = DataBase.Instance.Query<AddressesDTO>().Single(x => x.Id == item.AddressID.Value);
                    else
                        address = new AddressesDTO() { AddressName = item.AddressName, City = item.City, Neighborhood = item.Neighborhood, Street = item.Street };

                    list.Add(new SingleOrderViewModel()
                    {
                        ID = item.ID,
                        AddressID = item.AddressID,
                        AddressName = address.AddressName,
                        Address = string.Format("{0},{1},{2}", address.City, address.Neighborhood, address.Street),
                        City = address.City,
                        CookerID = item.CookerID,
                        Neighborhood = address.Neighborhood,
                        ProductsIncluded = item.ProductsIncluded,
                        Street = address.Street,
                        CookerName = item.CookerName,
                        Date = item.Date,
                        FromTime = item.FromTime.ToString(),
                        ToTime = item.ToTime.ToString(),
                        IsRatingVisible = item.Date < DateTime.Now ? item.Rating.HasValue : false,
                        IsRatеVisible = item.Date < DateTime.Now ? !item.Rating.HasValue : false,
                        OrderColor = item.Date < DateTime.Now ? Color.LightGreen : Color.Yellow,
                        Rating = item.Rating.HasValue ? item.Rating.Value : 0
                    });
                }
            }

            return list;
        }

        public async Task<CookerViewModel> GetCooker(int cookerID)
        {
            CookerDTO data = await _rc.GetDataAsync<CookerDTO>(GetActionMethods.Cooker, string.Format("{0}?fromDate={1}", cookerID, DateTime.Now.ToString("yyyy-MM-dd")));

            return new CookerViewModel()
            {
                ID = data.ID,
                Description = data.Description,
                HoursPricing = data.HoursPricing,
                Image = data.Image,
                Name = string.Format("{0} {1}", data.FirstName, data.LastName),
                OrdersCount = data.OrdersCount,
                Rating = data.Rating
            };
        }

        public async Task<bool> VoteOrder(int orderID, double rating)
        {
            ResponseModel model = await _rc.PostDataAsync(PostActionMethods.VoteOrder, new OrderRatingDTO() { OrderID = orderID, Rating = rating });

            return model.IsSuccessStatusCode;
        }
    }
}
