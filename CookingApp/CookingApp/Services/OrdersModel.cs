using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.OrdersPage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CookingApp.Services
{
    public class OrdersModel
    {
        private RestfulClient _rc = new RestfulClient();

        public ObservableCollection<SingleOrderViewModel> GetOrders()
        {
            //TODO from server or local base ?
            var data = new ObservableCollection<SingleOrderViewModel>();
            foreach (var item in DataBase.Instance.Query<OrderDTO>())
            {
                AddressesDTO address;
                if (item.AddressID.HasValue)
                    address = DataBase.Instance.Query<AddressesDTO>().Single(x => x.Id == item.AddressID.Value);
                else
                    address = new AddressesDTO() { AddressName = item.AddressName, City = item.City, Neighborhood = item.Neighborhood, Street = item.Street };

                data.Add(new SingleOrderViewModel()
                {
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
                    FromTime = item.FromTime,
                    ToTime = item.ToTime
                });
            }
            return data;
        }

        public async Task<CookerViewModel> GetCooker(int cookerID)
        {
            CookerDTO data = await _rc.GetDataAsync<CookerDTO>(GetActionMethods.Cooker, cookerID.ToString());

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
    }
}
