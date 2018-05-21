using CookingApp.Models;
using CookingApp.ViewModels.ReceipsPage;
using System.Collections.Generic;

namespace CookingApp.Services
{
    public class CuisinesModel
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

        public List<ReceipViewModel> GetAllReceips()
        {
            //TODO DTO
            return new List<ReceipViewModel>();
        }
    }
}
