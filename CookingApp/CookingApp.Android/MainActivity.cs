using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Views;
using CookingApp.Droid.Utils;
using CookingApp.Enums;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.Services;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.RecipesPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using CookingApp.Views.RecipesPage;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Plugin.CurrentActivity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CookingApp.Droid
{
    [Activity(Label = "Chefs Order", Icon = "@drawable/Cook", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MyTheme);

            CrossCurrentActivity.Current.Init(this, bundle);
            base.OnCreate(bundle);

            if (IsPlayServicesAvailable())
            {
                Task serviceInstanceTask = new Task(() =>
                {
                    Intent intent = null;
                    if (!IsServiceRunning(typeof(FireBaseIDService)))
                    {
                        intent = new Intent(this, typeof(FireBaseIDService));
                        StartService(intent);
                    }

                    if (!IsServiceRunning(typeof(CookingAppFirebaseMessagingService)))
                    {
                        intent = new Intent(this, typeof(CookingAppFirebaseMessagingService));
                        StartService(intent);
                    }
                });

                serviceInstanceTask.Start();
            }
            else
            {
                GoogleApiAvailability.Instance.MakeGooglePlayServicesAvailable(this);
            }

            global::Xamarin.Forms.Forms.Init(this, bundle);

            UserDialogs.Init(this);
            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar);
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());
            LoadApplication(new App());
        }

        private bool IsServiceRunning(Type serviceType)
        {
            return (((ActivityManager)GetSystemService(ActivityService)).GetRunningServices(int.MaxValue).Select(service => service.Service.ShortClassName).ToList())
                   .Contains(serviceType.ToString().ToLower());
        }

        private bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                }
                else
                {
                    Finish();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void OnStart()
        {
            AppBeforeLoad();
            base.OnStart();
        }

        private void AppBeforeLoad()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                DataBase.Instance.LoadNomenclatures();
            }
            else
            {
                CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            }
        }

        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                CrossConnectivity.Current.ConnectivityChanged -= Current_ConnectivityChanged;
                DataBase.Instance.LoadNomenclatures();
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            if (intent?.Extras != null)
            {
                try
                {
                    string data = intent?.Extras.GetString("data");
                    if (data != null)
                    {
                        NotificationDTO notification = JsonConvert.DeserializeObject<NotificationDTO>(data);
                        string NotificationSentTime = intent?.Extras.GetString("NotificationSentTime");
                        notification.NotificationSentTime = DateTime.Parse(NotificationSentTime);

                        if (!DataBase.Instance.Query<NotificationDTO>().Any(x => x.NotificationID == notification.NotificationID))
                        {
                            if (notification.NotificationType == NotificationsTypesEnum.Order)
                            {
                                notification.IsOrderPending = true;
                            }

                            DataBase.Instance.Add(notification);
                        }

                        NavigateNotification(notification.NotificationType, notification.NotificationID);
                    }
                    else
                    {
                        string NotificationID = intent?.Extras.GetString("notificationID");
                        NotificationsTypesEnum NotificationType = (NotificationsTypesEnum)Enum.Parse(typeof(NotificationsTypesEnum), intent?.Extras.GetString("notificationType"), true);
                        if (!string.IsNullOrEmpty(NotificationID))
                        {
                            if (DataBase.Instance.Query<NotificationDTO>().FirstOrDefault(x => x.NotificationID == int.Parse(NotificationID)) != null)
                            {
                                string NotificationSentTime = intent?.Extras.GetString("notificationSentTime");
                                string NotificationTitle = intent?.Extras.GetString("notificationTitle");
                                string NotificationBody = intent?.Extras.GetString("notificationBody");

                                DataBase.Instance.Add(new NotificationDTO()
                                {
                                    NotificationID = int.Parse(NotificationID),
                                    NotificationBody = NotificationBody,
                                    NotificationSentTime = DateTime.Parse(NotificationSentTime),
                                    NotificationTitle = Title,
                                    NotificationType = NotificationType,
                                    IsOrderPending = NotificationType == NotificationsTypesEnum.Order ? true : false
                                });
                            }
                        }
                        NavigateNotification(NotificationType, int.Parse(NotificationID));
                    }
                }
                catch (Exception)
                {
                    //Error ?
                }
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (Intent?.Extras != null)
            {
                try
                {
                    string data = Intent?.Extras.GetString("data");
                    if (data != null)
                    {
                        Intent.RemoveExtra("data");

                        NotificationDTO notification = JsonConvert.DeserializeObject<NotificationDTO>(data);
                        string NotificationSentTime = Intent.Extras.GetString("NotificationSentTime");
                        notification.NotificationSentTime = DateTime.Parse(NotificationSentTime);

                        if (!DataBase.Instance.Query<NotificationDTO>().Any(x => x.NotificationID == notification.NotificationID))
                        {
                            if (notification.NotificationType == NotificationsTypesEnum.Order)
                            {
                                notification.IsOrderPending = true;
                            }

                            DataBase.Instance.Add(notification);
                        }

                        NavigateNotification(notification.NotificationType, notification.NotificationID);
                    }
                    else
                    {
                        string NotificationID = Intent.Extras.GetString("notificationID");
                        NotificationsTypesEnum NotificationType = (NotificationsTypesEnum)Enum.Parse(typeof(NotificationsTypesEnum), Intent.Extras.GetString("notificationType"), true);
                        if (!string.IsNullOrEmpty(NotificationID))
                        {
                            if (DataBase.Instance.Query<NotificationDTO>().FirstOrDefault(x => x.NotificationID == int.Parse(NotificationID)) != null)
                            {
                                string NotificationSentTime = Intent.Extras.GetString("notificationSentTime");
                                string NotificationTitle = Intent.Extras.GetString("notificationTitle");
                                string NotificationBody = Intent.Extras.GetString("notificationBody");

                                DataBase.Instance.Add(new NotificationDTO()
                                {
                                    NotificationID = int.Parse(NotificationID),
                                    NotificationBody = NotificationBody,
                                    NotificationSentTime = DateTime.Parse(NotificationSentTime),
                                    NotificationTitle = Title,
                                    NotificationType = NotificationType,
                                    IsOrderPending = NotificationType == NotificationsTypesEnum.Order ? true : false
                                });
                            }
                        }
                        NavigateNotification(NotificationType, int.Parse(NotificationID));
                    }
                }
                catch (Exception)
                {
                    //Exception ?
                }
            }
        }

        private async void NavigateNotification(NotificationsTypesEnum NotificationType, int NotificationID)
        {
            if (NotificationType == NotificationsTypesEnum.Chat)
            {
                //TODO    
            }
            else if (NotificationType == NotificationsTypesEnum.Cooker)
            {
                OrdersModel model = new OrdersModel();
                CookerViewModel cooker = await model.GetCooker(NotificationID);

                await PageTemplate.CurrentPage.NavigateAsync(new SingleCookerPage(cooker) { Title = cooker.Name });
            }
            else if (NotificationType == NotificationsTypesEnum.Recipe)
            {
                RecipesModel model = new RecipesModel();
                RecipeViewModel recipe = await model.GetRecipeFromID(NotificationID);

                await PageTemplate.CurrentPage.NavigateAsync(new SingleRecipePage(recipe) { Title = recipe.Title });
            }
            else
            {
                await PageTemplate.CurrentPage.NavigateAsync(Utility.PageParser(PageNavigateEnums.NotificationsPage));
            }
        }
    }
}
