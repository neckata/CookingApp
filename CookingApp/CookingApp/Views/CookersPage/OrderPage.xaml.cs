using CookingApp.ViewModels.CookersPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.CookersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage(int cookerID,string cookerName)
        {
            InitializeComponent();
            this.BindingContext = new OrderViewModel(cookerID, cookerName);
        }
    }
}