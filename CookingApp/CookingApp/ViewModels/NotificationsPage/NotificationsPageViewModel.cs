using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.MainPage;
using CookingApp.ViewModels.RecipesPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.RecipesPage;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.NotificationsPage
{
    public class NotificationsPageViewModel : ObservableViewModel
    {
        public NotificationsPageViewModel()
        {
            var data = DataBase.Instance.Query<NotificationDTO>();
            Notifications = new List<NotificationViewModel>();
            foreach (var item in data)
            {
                Notifications.Add(new NotificationViewModel()
                {
                    NotificationBody = item.NotificationBody,
                    NotificationID = item.NotificationID,
                    NotificationSentTime = item.NotificationSentTime,
                    NotificationTitle = item.NotificationTitle,
                    NotificationType = item.NotificationType
                });
            }

            if (Notifications.Count == 0)
            {
                NoNotifications = true;
                OnPropertyChangedModel(nameof(NoNotifications));
            }
        }

        public bool IsBusy { get; set; }

        public bool NoNotifications { get; set; }

        public List<NotificationViewModel> Notifications { get; set; }

        public ICommand Navigate
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    NotificationViewModel notification = Notifications.FirstOrDefault(x => x.NotificationID == para);

                    if (notification.NotificationType == NotificationsTypesEnum.Chat)
                    {
                        //TODO    
                    }
                    else if (notification.NotificationType == NotificationsTypesEnum.Cooker)
                    {
                        CookerDTO cookerDTO = DataBase.Instance.Query<CookerDTO>().FirstOrDefault(x => x.ID == notification.NotificationID);

                        CookerViewModel cooker = new CookerViewModel()
                        {
                            Description = cookerDTO.Description,
                            ID = cookerDTO.ID,
                            HoursPricing = cookerDTO.HoursPricing,
                            Image = cookerDTO.Image,
                            Name = string.Format("{0} {1}", cookerDTO.FirstName, cookerDTO.LastName),
                            OrdersCount = cookerDTO.OrdersCount,
                            Rating = cookerDTO.Rating
                        };

                        await PageTemplate.CurrentPage.NavigateAsync(new SingleCookerPage(cooker) { Title = cooker.Name });
                    }
                    else if (notification.NotificationType == NotificationsTypesEnum.Recipe)
                    {
                        RecipeDTO recipeDTO = DataBase.Instance.Query<RecipeDTO>().FirstOrDefault(x => x.ID == notification.NotificationID);

                        RecipeViewModel recipe = new RecipeViewModel()
                        {
                            ID = recipeDTO.ID,
                            HowToCook = recipeDTO.HowToCook,
                            Image = recipeDTO.Image,
                            NecessaryIngredients = recipeDTO.NecessaryIngredients,
                            Portions = recipeDTO.Portions,
                            TimeToCook = recipeDTO.TimeToCook,
                            Title = recipeDTO.Title
                        };

                        await PageTemplate.CurrentPage.NavigateAsync(new SingleRecipePage(recipe) { Title = recipe.Title });
                    }
                });
            }
        }
    }
}
