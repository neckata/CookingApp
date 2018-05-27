using CookingApp.ViewModels.CookersPage;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp.Views.CookersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleCookerPage : ContentPage
    {
        public SingleCookerPage(CookerViewModel model)
        {
            InitializeComponent();
            this.BindingContext = new SingleCookerViewModel(model);
        }
    }
}