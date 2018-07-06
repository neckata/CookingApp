using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.Views.MainPage;
using System.Collections.Generic;
using System.Linq;
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

        public async void LoadNomenclatures()
        {
            RestfulClient client = new RestfulClient();

            List<CuisineFilterDTO> cuisineFilterDTOs = await client.GetDataAsync<List<CuisineFilterDTO>>(GetActionMethods.CuisinesFilters); 
            if(cuisineFilterDTOs.Count > 0)
            {
                foreach (var item in DataBase.Instance.Query<CuisineFilterDTO>())
                    DataBase.Instance.Delete<CuisineFilterDTO>(item.Code);

                foreach (var item in cuisineFilterDTOs)
                    DataBase.Instance.Add(item);
            }

            List<CuisineDTO> cuisineDTOs = await client.GetDataAsync<List<CuisineDTO>>(GetActionMethods.Cuisines);
            if (cuisineDTOs.Count > 0)
            {
                var selectedCuisines = DataBase.Instance.Query<CuisineSelectedDTO>().ToList();

                foreach (var item in DataBase.Instance.Query<CuisineDTO>())
                    DataBase.Instance.Delete<CuisineDTO>(item.Code);

                foreach (var item in cuisineDTOs)
                {
                    DataBase.Instance.Add(item);

                    for (int i = 0; i < selectedCuisines.Count; i++)
                        if (selectedCuisines[i].Code == item.Code)
                        {
                            selectedCuisines.Remove(selectedCuisines[0]);
                            i--;
                        }
                }

                foreach (var item in selectedCuisines)
                    DataBase.Instance.Delete<CuisineSelectedDTO>(item.Code);
            }
        }
    }
}
