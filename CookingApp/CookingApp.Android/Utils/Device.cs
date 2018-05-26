using CookingApp.Droid.Utils;
using CookingApp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceIMei))]
namespace CookingApp.Droid.Utils
{
    public class DeviceIMei : IDevice
    {
        public string GetIdentifier()
        {
            Android.Telephony.TelephonyManager mTelephonyMgr;
            mTelephonyMgr = (Android.Telephony.TelephonyManager)Forms.Context.GetSystemService(Android.Content.Context.TelephonyService);
            return mTelephonyMgr.DeviceId;
        }
    }
}