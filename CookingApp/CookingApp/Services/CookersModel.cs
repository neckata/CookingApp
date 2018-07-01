using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.RecipesPage;
using CookingApp.ViewModels.UserPage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
                new CookerDTO() {ID=5,Description="Готвач с 20 години опит във веганската кухня, перфекционист",HoursPricing=50,IsWorking=false,Name="Димитър Димитров",OrdersCount=45,Rating=1.5,Image="http://icons.iconarchive.com/icons/paomedia/small-n-flat/512/user-male-icon.png" }
            };

            List<CookerViewModel> cookers = new List<CookerViewModel>();
            foreach (var item in data)
                cookers.Add(new CookerViewModel() { ID = item.ID, Description = item.Description, HoursPricing = item.HoursPricing, Image = item.Image, Name = item.Name, OrdersCount = item.OrdersCount, Rating = item.Rating });

            return cookers;
        }

        public List<RecipeViewModel> GetCookerRecipes(int cookerID)
        {
            List<RecipeDTO> data = new List<RecipeDTO>()
            {
                new RecipeDTO(){ID=1,Title="Ягодова Панакота",Image="http://recepti.gotvach.bg/files/lib/600x350/starawberry-pannacotta.jpg",TimeToCook=8,Portions=6},
                new RecipeDTO(){ID=2,Title="Домати Конкасе",Image="http://recepti.gotvach.bg/files/lib/600x350/sandvichi_domati_new.jpg",TimeToCook=45,Portions=4},
                new RecipeDTO(){ID=3,Title="Крем карамел",Image="http://recepti.gotvach.bg/files/lib/600x350/krem4.jpg",TimeToCook=75,Portions=8},
                new RecipeDTO(){ID=4,Title="Айс Кола",Image="http://recepti.gotvach.bg/files/lib/600x350/icecola.jpg",TimeToCook=5,Portions=2},
                new RecipeDTO(){ID=5,Title="Постни Вегански Хапки",Image="http://recepti.gotvach.bg/files/lib/600x350/postni-vegan-hapki-basil2.JPG",TimeToCook=20,Portions=7}
            };

            List<RecipeViewModel> recipes = new List<RecipeViewModel>();
            foreach (var item in data)
                recipes.Add(new RecipeViewModel() { ID = item.ID, Image = item.Image, TimeToCook = item.TimeToCook, Title = item.Title, Portions = item.Portions });

            return recipes;
        }

        public ObservableCollection<CuisineTypeViewModel> GetCookerCuisisnes(int cookerID)
        {
            List<CuisineDTO> data = new List<CuisineDTO>()
            {
                 new CuisineDTO{Code="VEGAN"}, new CuisineDTO{Code="DIETIC"}, new CuisineDTO{Code="AVST"},
                 new CuisineDTO{Code="AU"},new CuisineDTO{Code="NOCOOK"}, new CuisineDTO{Code="AUTUMN"}, new CuisineDTO{Code="WINTER"},
            };

            var cuisines = DataBase.Instance.Query<CuisineFilterDTO>();
            var cuisinesTypes = DataBase.Instance.Query<CuisineDTO>();

            ObservableCollection<CuisineTypeViewModel> list = new ObservableCollection<CuisineTypeViewModel>();

            foreach (var item in cuisines)
                list.Add(new CuisineTypeViewModel() { Code = item.Code, Description = item.Description, Cuisines = new List<CuisineViewModel>()});

            foreach (var item in data)
            {
                var cuisineType = cuisinesTypes.Single(x => x.Code == item.Code);
                list.Single(x => x.Code == cuisineType.CuisineTypeCode).Cuisines.Add(new CuisineViewModel { Description = cuisineType.Description });
            }

            return new ObservableCollection<CuisineTypeViewModel>(list.Where(x => x.Cuisines.Count > 0));
        }
    }
}
