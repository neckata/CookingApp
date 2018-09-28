using Acr.UserDialogs;
using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.Resources;
using CookingApp.Services;
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
            _model = new NotificationsModel();

            var data = DataBase.Instance.Query<NotificationDTO>();
            Notifications = new List<NotificationViewModel>();
            foreach (var item in data)
            {
                var vm = new NotificationViewModel()
                {
                    NotificationBody = item.NotificationBody,
                    NotificationID = item.NotificationID,
                    NotificationSentTime = item.NotificationSentTime,
                    NotificationTitle = item.NotificationTitle,
                    NotificationType = item.NotificationType,
                    IsOrderPending = item.IsOrderPending,
                    IsOrderProcessed = item.IsOrderAccepted || item.IsOrderRejected,
                    IsOrderAccepted = item.IsOrderAccepted,
                    IsOrderRejected = item.IsOrderRejected
                };

                if(vm.NotificationType == NotificationsTypesEnum.Cooker)
                {
                    vm.NotificationColor = Color.Blue;
                }
                else if (vm.NotificationType == NotificationsTypesEnum.Recipe)
                {
                    vm.NotificationColor = Color.Purple;
                }
                else if (vm.NotificationType == NotificationsTypesEnum.Order)
                {
                    if(vm.IsOrderPending)
                    vm.NotificationColor = Color.Yellow;
                    else
                    {
                        if(item.IsOrderAccepted)
                            vm.NotificationColor = Color.LightGreen;
                        else
                            vm.NotificationColor = Color.Red;
                    }
                }

                Notifications.Add(vm);
            }

            if (Notifications.Count == 0)
            {
                NoNotifications = true;
                OnPropertyChangedModel(nameof(NoNotifications));
            }
        }

        private NotificationsModel _model;

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

        public ICommand Confirm
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    IsBusy = true;
                    OnPropertyChangedModel(nameof(IsBusy));

                    if (await _model.Confirm(para))
                    {
                        var item = Notifications.First(x => x.NotificationID == para);
                        item.ChangeColor(Color.Green);

                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblOrderAcceptedSuccess"));
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblOrderAcceptedNotSuccess"));
                    }

                    IsBusy = false;
                    OnPropertyChangedModel(nameof(IsBusy));
                });
            }
        }

        public ICommand Reject
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    if (await _model.Reject(para))
                    {
                        var item = Notifications.First(x => x.NotificationID == para);
                        item.ChangeColor(Color.Red);

                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblOrderRejectedSuccess"));
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblOrderRejectedNotSuccess"));
                    }
                });
            }
        }
    }
}
