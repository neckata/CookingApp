using CookingApp.Models;
using CookingApp.ViewModels.RecipesPage;
using System.Collections.Generic;

namespace CookingApp.Services
{
    public class RecipesModel
    {
        public List<CuisineDTO> GetAllCuisinesTypes()
        {
            List<CuisineDTO> data = new List<CuisineDTO>()
            {
                new CuisineDTO(){Code="VEGAN",Description="Веганска Кухня"},
                new CuisineDTO(){Code="VEGE",Description="Вегетариански Ястия"},
                new CuisineDTO(){Code="DIETIC",Description="Диетична Кухня"}
            };

            return data;
        }


        public List<CuisineDTO> GetAllCuisinesCountries()
        {
            List<CuisineDTO> data = new List<CuisineDTO>()
            {
                new CuisineDTO(){Code="AVST",Description="Австралийска Кухня"},
                new CuisineDTO(){Code="AU",Description="Австрийска Кухня"},
                new CuisineDTO(){Code="ALB",Description="Албанска Кухня"}
            };

            return data;
        }

        public List<CuisineDTO> GetAllCuisinesCookingType()
        {
            List<CuisineDTO> data = new List<CuisineDTO>()
            {
                new CuisineDTO(){Code="NOCOOK",Description="Без Термична Обработка"},
                new CuisineDTO(){Code="BALAN",Description="Ястия за Бланширане"},
                new CuisineDTO(){Code="VAREN",Description="Ястия за Варене"}
            };

            return data;
        }

        public List<CuisineDTO> GetAllCuisinesSeasons()
        {
            List<CuisineDTO> data = new List<CuisineDTO>()
            {
                new CuisineDTO(){Code="AUTUMN",Description="Есенни Ястия"},
                new CuisineDTO(){Code="WINTER",Description="Зимни Ястия"},
                new CuisineDTO(){Code="SRPING",Description="Пролетни Ястия"},
                 new CuisineDTO(){Code="SUMMER",Description="Летни Ястия"}
            };

            return data;
        }

        public List<CuisineFilterDTO> GetCuisineFilters()
        {
            List<CuisineFilterDTO> data = new List<CuisineFilterDTO>()
            {
                new CuisineFilterDTO(){Code="TYPE",Description = "Тип Кухня"},
                new CuisineFilterDTO(){Code="COUNTRY",Description = "Интернационална Кухня"},
                new CuisineFilterDTO(){Code="COOKINGTYPE",Description = "Начин на обработка"},
                new CuisineFilterDTO(){Code="SEASON",Description = "Сезонна Кухня"}
            };

            return data;
        }

        public List<RecipeViewModel> GetAllRecipes(string cuisineCode)
        {
            //Данни Нужни само при страницата с всички рецепти
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
    }
}
