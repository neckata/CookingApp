using CookingApp.Helpers;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.OrdersPage
{
    public class OrdersPageViewModel : ObservableViewModel
    {
        public OrdersPageViewModel()
        {
            Orders = _model.GetOrders();
            NoOrders = Orders.Count == 0;
        }

        private OrdersModel _model = new OrdersModel();

        public bool NoOrders { get; set; }

        public ObservableCollection<SingleOrderViewModel> Orders { get; set; }

        public ICommand Send
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                   
                });
            }
        }

        public ICommand Delete
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                   
                });
            }
        }
    }
}
