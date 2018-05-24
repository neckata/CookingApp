using CookingApp.Models;
using CookingApp.ViewModels.CookersPage;
using System.Collections.Generic;

namespace CookingApp.Services
{
    public class CookersModel
    {
        public List<CookerViewModel> GetCookers(string cuisineCode)
        {
            List<CookerDTO> data = new List<CookerDTO>()
            {
                new CookerDTO() {ID=1,Description="Готвач с 20 години опит във веганската кухня, перфекционист",HoursPricing=20,IsWorking=true,Name="Иван Иванов",OrdersCount=24,Rating=2.5,Image="http://icons.iconarchive.com/icons/paomedia/small-n-flat/512/user-male-icon.png" },
                new CookerDTO() {ID=2,Description="Готвач с 20 години опит във веганската кухня, перфекционист",HoursPricing=25,IsWorking=true,Name="Мария Николова",OrdersCount=55,Rating=4.5,Image="https://www.volleyamerica.com/img/generic-woman.png" },
                new CookerDTO() {ID=3,Description="Готвач с 20 години опит във веганската кухня, перфекционист",HoursPricing=15,IsWorking=true,Name="Петър Петров",OrdersCount=33,Rating=5,Image="http://icons.iconarchive.com/icons/paomedia/small-n-flat/512/user-male-icon.png" },
                new CookerDTO() {ID=4,Description="Готвач с 20 години опит във веганската кухня, перфекционист",HoursPricing=45,IsWorking=true,Name="Гергана Георгиева",OrdersCount=1,Rating=2,Image="https://www.volleyamerica.com/img/generic-woman.png" },
                new CookerDTO() {ID=5,Description="Готвач с 20 години опит във веганската кухня, перфекционист",HoursPricing=50,IsWorking=false,Name="Димитър Димитров",OrdersCount=45,Rating=7,Image="http://icons.iconarchive.com/icons/paomedia/small-n-flat/512/user-male-icon.png" }
            };

            List<CookerViewModel> cookers = new List<CookerViewModel>();
            foreach (var item in data)
                cookers.Add(new CookerViewModel() { ID = item.ID, Description = item.Description, HoursPricing = item.HoursPricing, Image = item.Image, Name = item.Name, OrdersCount = item.OrdersCount, Rating = item.Rating });

            return cookers;
        }
    }
}
