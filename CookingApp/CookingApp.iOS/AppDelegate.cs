using CookingApp.Helpers;
using Foundation;
using Plugin.Connectivity;
using System;
using UIKit;

namespace CookingApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}


        private void ApplicationBeforeLoad(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
                DataBase.Instance.LoadNomenclature();
            else
                CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                CrossConnectivity.Current.ConnectivityChanged -= Current_ConnectivityChanged;
                DataBase.Instance.LoadNomenclature();
            }
        }
    }
}
