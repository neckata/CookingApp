using Acr.UserDialogs;
using CookingApp.Models;
using CookingApp.Resources;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.AddressesPage
{
    public class AddressesPageViewModel : ObservableViewModel
    {
        private UserModel _model = new UserModel();

        public AddressesPageViewModel()
        {
            Addresses = new ObservableCollection<AddressViewModel>();
            FillData();
        }

        public ObservableCollection<AddressViewModel> Addresses { get; set; }

        public ICommand AddAddress
        {
            get
            {
                return new Command(() =>
                {
                    AddressViewModel addressViewModel = new AddressViewModel();
                    addressViewModel.ID = Addresses.Last().ID + 1;
                    Addresses.Add(addressViewModel);
                    OnPropertyChangedModel(nameof(Addresses));
                });
            }
        }

        public ICommand Save
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    AddressViewModel address = Addresses.FirstOrDefault(x => x.ID == para);
                    AddressesDTO addressDTO = new AddressesDTO()
                    {
                        Id = address.IDInBase,
                        City = address.City,
                        AddressName = address.Name,
                        Neighborhood = address.Neighborhood,
                        Street = address.Street
                    };

                    int? id = await _model.SaveAddress(addressDTO);

                    if (id.HasValue)
                    {
                        address.IDInBase = id;
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblSaveSuccess"));
                    }
                });
            }
        }

        public ICommand Delete
        {
            get
            {
                return new Command<int>(async (para) =>
                {
                    AddressViewModel address = Addresses.FirstOrDefault(x => x.ID == para);
                    bool isDeleted = true;

                    if (address.IDInBase.HasValue)
                        isDeleted = await _model.DeleteAddress(address.IDInBase.Value);

                    if (isDeleted)
                    {
                        Addresses.Remove(address);

                        if (Addresses.Count == 0)
                            Addresses.Add(new AddressViewModel());

                        OnPropertyChangedModel(nameof(Addresses));

                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblDeleteSuccess"));
                    }
                });
            }
        }

        public async void FillData()
        {
            Addresses = await _model.GetAddresses();
            OnPropertyChangedModel(nameof(Addresses));
        }
    }
}
