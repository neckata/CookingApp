using CookingApp.Resources;
using CookingApp.Views.MainPage;
using Plugin.Multilingual;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CookingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            MainPage = new PageTemplate();
        }
    }
}