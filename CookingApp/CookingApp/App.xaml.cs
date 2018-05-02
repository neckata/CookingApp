using CookingApp.Views.MainPage;
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
            MainPage = new PageTemplate();
        }
    }
}