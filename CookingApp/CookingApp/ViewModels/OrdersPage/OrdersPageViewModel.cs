using CookingApp.Services;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.MainPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.OrdersPage
{
    public class OrdersPageViewModel : ObservableViewModel
    {
        public OrdersPageViewModel()
        {
            FillData();
        }

        private OrdersModel _model = new OrdersModel();

        public bool NoOrders { get; set; }

        public ObservableCollection<SingleOrderViewModel> Orders { get; set; }

        public ICommand OpenCooker
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    CookerViewModel cooker = _model.GetCooker(para);
                    await PageTemplate.CurrentPage.NavigateAsync(new SingleCookerPage(cooker) { Title = cooker.Name });
                });
            }
        }

        public async void FillData()
        {
            Orders = await _model.GetOrders();
            NoOrders = Orders.Count == 0;

            OnPropertyChangedModel(nameof(Orders));
            OnPropertyChangedModel(nameof(NoOrders));
        }
    }
}
