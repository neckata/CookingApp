using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CookingApp.Helpers;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Plugin.CurrentActivity;

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

            global::Xamarin.Forms.Forms.Init(this, bundle);

            UserDialogs.Init(this);
            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar);
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());      
           
            LoadApplication(new App());
        }

        protected override void OnStart()
        {
            AppBeforeLoad();
            base.OnStart();
        }

        private void AppBeforeLoad()
        {
            if (CrossConnectivity.Current.IsConnected)
                DataBase.Instance.LoadNomenclatures();
            else
                CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                CrossConnectivity.Current.ConnectivityChanged -= Current_ConnectivityChanged;
                DataBase.Instance.LoadNomenclatures();
            }
        }
    }
}