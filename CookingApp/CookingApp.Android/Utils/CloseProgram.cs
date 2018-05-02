using Android.Content;
using CookingApp.Interfaces;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(CloseProgram))]
namespace CookingApp.Droid.Utils
{
    public class CloseProgram : ICloseProgram
    {
        public void CloseApp()
        {
            Intent startMain = new Intent(Intent.ActionMain);
            startMain.AddCategory(Intent.CategoryHome);
            CrossCurrentActivity.Current.Activity.StartActivity(startMain);
        }
    }
}