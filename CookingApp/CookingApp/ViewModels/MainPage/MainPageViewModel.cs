using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.Views.MainPage;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.MainPage
{
    public class MainPageViewModel : ObservableViewModel
    {
        public MainPageViewModel()
        {
            if (!AreNomenclatureLoaded)
            {
                LoadNomenclatures();
                AreNomenclatureLoaded = true;
            }
        }

        public static bool AreNomenclatureLoaded = false;

        public ICommand Navigate
        {
            get
            {
                return new Command<string>(async (para) =>
                {
                    PageNavigateEnums page = Utility.ParseEnum<PageNavigateEnums>(para);
                    await PageTemplate.CurrentPage.NavigateAsync(Utility.PageParser(page));
                });
            }
        }

        public void LoadNomenclatures()
        {
            List<CuisineFilterDTO> cuisineFilterDTOs = new List<CuisineFilterDTO>()
            {
                new CuisineFilterDTO(){Code=CuisineTypeEnums.Type,Description = "Тип Кухня"},
                new CuisineFilterDTO(){Code=CuisineTypeEnums.Country,Description = "Интернационална Кухня"},
                new CuisineFilterDTO(){Code=CuisineTypeEnums.CookingType,Description = "Начин на обработка"},
                new CuisineFilterDTO(){Code=CuisineTypeEnums.Season,Description = "Сезонна Кухня"}
            };

            foreach (var item in cuisineFilterDTOs)
                DataBase.Instance.Add(item);

            List<CuisineDTO> cuisineDTOs = new List<CuisineDTO>()
            {
                 new CuisineDTO(){Code="VEGAN",Description="Веганска Кухня",CuisineTypeCode =CuisineTypeEnums.Type},
                 new CuisineDTO(){Code="VEGE",Description="Вегетариански Ястия",CuisineTypeCode =CuisineTypeEnums.Type},
                 new CuisineDTO(){Code="DIETIC",Description="Диетична Кухня",CuisineTypeCode =CuisineTypeEnums.Type},
                 new CuisineDTO(){Code="AVST",Description="Австралийска Кухня",CuisineTypeCode =CuisineTypeEnums.Country},
                 new CuisineDTO(){Code="AU",Description="Австрийска Кухня",CuisineTypeCode =CuisineTypeEnums.Country},
                 new CuisineDTO(){Code="ALB",Description="Албанска Кухня",CuisineTypeCode =CuisineTypeEnums.Country},
                 new CuisineDTO(){Code="NOCOOK",Description="Без Термична Обработка",CuisineTypeCode =CuisineTypeEnums.CookingType},
                 new CuisineDTO(){Code="BALAN",Description="Ястия за Бланширане",CuisineTypeCode =CuisineTypeEnums.CookingType},
                 new CuisineDTO(){Code="VAREN",Description="Ястия за Варене",CuisineTypeCode =CuisineTypeEnums.CookingType},
                 new CuisineDTO(){Code="AUTUMN",Description="Есенни Ястия",CuisineTypeCode =CuisineTypeEnums.Season},
                 new CuisineDTO(){Code="WINTER",Description="Зимни Ястия",CuisineTypeCode =CuisineTypeEnums.Season},
                 new CuisineDTO(){Code="SRPING",Description="Пролетни Ястия",CuisineTypeCode =CuisineTypeEnums.Season},
                 new CuisineDTO(){Code="SUMMER",Description="Летни Ястия",CuisineTypeCode =CuisineTypeEnums.Season}
            };

            foreach (var item in cuisineDTOs)
                DataBase.Instance.Add(item);
        }
    }
}
