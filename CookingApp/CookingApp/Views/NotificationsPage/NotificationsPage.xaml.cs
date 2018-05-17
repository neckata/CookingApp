using CookingApp.ViewModels.NotificationsPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.NotificationsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : ContentPage
    {
        public NotificationsPage()
        {
            InitializeComponent();
            this.BindingContext = new NotificationsPageViewModel();
        }
    }
}