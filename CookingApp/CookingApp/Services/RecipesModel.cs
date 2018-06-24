using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.RecipesPage;
using System.Collections.Generic;

namespace CookingApp.Services
{
    public class RecipesModel
    { 
        public List<CuisineDTO> GetCuisines(CuisineTypeEnums type)
        {
            return new List<CuisineDTO>(DataBase.Instance.Query<CuisineDTO>().Where(x => x.CuisineTypeCode == type));
        }

        public List<CuisineFilterDTO> GetCuisineFilters()
        {
            return new List<CuisineFilterDTO>(DataBase.Instance.Query<CuisineFilterDTO>());
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

        public List<string> GetNecessaryIngredients(int recipeID)
        {
            return new List<string>() { "прясно мляко - 250 мл", "течна сметана - 250 мл", "захар - 6 - с.л.", "ванилия - 1 бр.", "ягоди - 400 г пресни", "желатин - 2 пакетчета по 10 г", "мента - за декорация" };
        }

        public string GetHowToCook(int recipeID)
        {
            return @"В малка тенджерка се загряват смесените прясно мляко и течна сметана. Прибавят се три супени лъжици захар и сместа се разбърква периодично. 

                    След като се разтопи захарта, преди да заври сместа, тенджерката се отстранява от котлона. 

                    Едното пакетче желатин се накисва в 50 мл вода за да набъбне, докато млякото и сметаната се загряват. Набъбналият желатин се прибавя към тях, след като се охладят леко. Разбърква се за да се разтвори напълно, след което млечната смес се разпределя във чаши или формички(предварително леко намаслени). 

                    Формичките се слагат в хладилника, за да се стегне панакотата.Аз ги сложих във фризера, за да ускоря процеса на охлаждане. 

                    През това време ягодите се измиват и смилат в блендер. Оставят се няколко за украса на десерта.Смлените ягоди се прехвърлят в тенджерка, посипват се с останалата захар и също се загряват, докато се разтопи захарта.

                    Към ягодовата смес се добавя предварително накиснатият желатин. 

                    Ягодовата смес се разбърква и се оставя да се охлади. Вече охладена се разпределя във формичките с панакота, която трябва да се е стегнала.

                    Десертът се охлажда отново. 

                    При поднасяне формичките се обръщат върху чинийка, гарнират се с пресни ягоди и прясна мента.";
        }
    }
}
