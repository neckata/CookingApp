using Acr.UserDialogs;
using CookingApp.Resources;
using CookingApp.Services;
using CookingApp.ViewModels.CookersPage;
using CookingApp.ViewModels.MainPage;
using CookingApp.Views.CookersPage;
using CookingApp.Views.MainPage;
using System.Collections.ObjectModel;
using System.Linq;
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

        public bool IsBusy { get; set; }

        public ObservableCollection<SingleOrderViewModel> Orders { get; set; }

        public ICommand OpenCooker
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    CookerViewModel cooker = await _model.GetCooker(para);
                    await PageTemplate.CurrentPage.NavigateAsync(new SingleCookerPage(cooker) { Title = cooker.Name });
                });
            }
        }

        public ICommand Rate
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    IsBusy = true;
                    OnPropertyChangedModel(nameof(IsBusy));

                    SingleOrderViewModel order = Orders.First(x => x.ID == para);
                    bool isVoted = await _model.VoteOrder(order.ID, order.Rating);
                    if (isVoted)
                    {
                        order.UpdateIsRated(order.Rating);

                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblRateSuccess"));
                    }
                    else
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblRateNotSuccess"));

                    IsBusy = false;
                    OnPropertyChangedModel(nameof(IsBusy));
                });
            }
        }

        private async void FillData()
        {
            IsBusy = true;
            OnPropertyChangedModel(nameof(IsBusy));

            Orders = await _model.GetOrders();
            NoOrders = Orders.Count == 0;

            OnPropertyChangedModel(nameof(Orders));
            OnPropertyChangedModel(nameof(NoOrders));

            IsBusy = false;
            OnPropertyChangedModel(nameof(IsBusy));
        }
    }
}
