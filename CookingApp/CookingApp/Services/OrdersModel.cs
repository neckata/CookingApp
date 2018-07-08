using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.OrdersPage;
using System.Collections.ObjectModel;
using System.Linq;

namespace CookingApp.Services
{
    public class OrdersModel
    {
        public ObservableCollection<SingleOrderViewModel> GetOrders()
        {
            var data = new ObservableCollection<SingleOrderViewModel>();
            foreach (var item in DataBase.Instance.Query<OrderDTO>())
            {
                AddressesDTO address;
                if (item.AddressID.HasValue)
                    address = DataBase.Instance.Query<AddressesDTO>().Single(x => x.ID == item.AddressID.Value);
                else
                    address = new AddressesDTO() { AddressName = item.AddressName, City = item.City, Neighborhood = item.Neighborhood, Street = item.Street };

                data.Add(new SingleOrderViewModel(item.Date, item.Time)
                {
                    AddressID = item.AddressID,
                    AddressName = address.AddressName,
                    Address = string.Format("{0},{1},{2}",address.City,address.Neighborhood,address.Street),
                    City=address.City,
                    CookerID=item.CookerID,
                    Neighborhood=address.Neighborhood,
                    ProductsIncluded =item.ProductsIncluded,
                    Street =address.Street,
                    CookerName =item.CookerName
                });
            }
            return data;
        }

        public CookerViewModel GetCooker(int cookerID)
        {
            var data = new CookerDTO()
            {
                ID = 1,
                Description = "Готвач с 20 години опит във веганската кухня, перфекционист",
                HoursPricing = 20,
                Name = "Иван Иванов",
                OrdersCount = 24,
                Rating = 2.5,
                Image = "http://icons.iconarchive.com/icons/paomedia/small-n-flat/512/user-male-icon.png"
            };

            return new CookerViewModel()
            {
                ID=data.ID,
                Description=data.Description,
                HoursPricing=data.HoursPricing,
                Image=data.Image,
                Name=data.Name,
                OrdersCount=data.OrdersCount,
                Rating=data.Rating
            };
        }
    }
}
