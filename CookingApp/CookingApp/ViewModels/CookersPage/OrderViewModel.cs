using CookingApp.Helpers;
using CookingApp.Models;
using Acr.UserDialogs;
using CookingApp.Resources;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using CookingApp.Views.MainPage;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace CookingApp.ViewModels.CookersPage
{
    public class OrderViewModel : ObservableViewModel
    {
        public OrderViewModel(int cookerID,string cookerName)
        {
            LoadData();
            _cookerID = cookerID;
            _cookerName = cookerName;
        }

        private int _cookerID;

        private string _selectedAddress ,_cookerName;

        private CookersModel _model = new CookersModel();

        public bool NameValidation { get; set; }

        public bool EmailValidation { get; set; }

        public bool NameEmailVisibility { get; set; }

        public bool IsAddresEnabled { get; set; }

        public bool ProductsIncluded { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string AddressName { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime Date { get; set; }

        public DateTime MinimumDate { get; set; }

        public string SelectedAddress
        {
            get
            {
                return _selectedAddress;
            }
            set
            {
                _selectedAddress = value;
                var address = DataBase.Instance.Query<AddressesDTO>().FirstOrDefault(x => x.AddressName == _selectedAddress);
                if (address != null)
                {
                    AddressName = address.AddressName;
                    Neighborhood = address.Neighborhood;
                    City = address.City;
                    Street = address.City;
                    IsAddresEnabled = false;
                }
                else
                {
                    AddressName = string.Empty;
                    Neighborhood = string.Empty;
                    City = string.Empty;
                    Street = string.Empty;
                    IsAddresEnabled = true;
                }

                NameEmailVisibility = true;

                OnPropertyChangedModel(nameof(IsAddresEnabled));
                OnPropertyChangedModel(nameof(NameEmailVisibility));
                OnPropertyChangedModel(nameof(AddressName));
                OnPropertyChangedModel(nameof(Neighborhood));
                OnPropertyChangedModel(nameof(City));
                OnPropertyChangedModel(nameof(Street));
            }
        }

        public List<string> Addresses { get; set; }

        public ICommand Order
        {
            get
            {
                return new Command(async () =>
                {
                    var address = DataBase.Instance.Query<AddressesDTO>().FirstOrDefault(x => x.AddressName == _selectedAddress);
                    
                    var order = new OrderDTO()
                    {
                        Date = Date,
                        Time = Time, ProductsIncluded = ProductsIncluded,
                        CookerID = _cookerID,
                        CookerName = _cookerName
                    };
                    if (address != null)
                        order.AddressID = address.ID;
                    else
                    {
                        order.AddressName = AddressName;
                        order.City = City;
                        order.Neighborhood = Neighborhood;
                        order.Street = Street;
                    }

                    bool isSend = _model.MakeOrder(order);
                    if (isSend)
                    {
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblOrderSuccess"));
                        await PageTemplate.CurrentPage.NavigateBack();
                    }
                    else
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblOrderFail"));
                });
            }
        }

        public ICommand ChangeProductsIncluded
        {
            get
            {
                return new Command(() =>
                {
                    ProductsIncluded = !ProductsIncluded;
                    OnPropertyChangedModel(nameof(ProductsIncluded));
                });
            }
        }

        private void LoadData()
        {
            IsAddresEnabled = true;
            MinimumDate = DateTime.Now;

            UserDTO user = DataBase.Instance.Query<UserDTO>().First();
            Name = user.Name;
            Family = user.Family;
            Phone = user.Phone;
            Email = user.Email;

            OnPropertyChangedModel(nameof(IsAddresEnabled));
            OnPropertyChangedModel(nameof(Name));
            OnPropertyChangedModel(nameof(Family));
            OnPropertyChangedModel(nameof(Phone));
            OnPropertyChangedModel(nameof(Email));

            Addresses = new List<string>();
            Addresses.Add(AppResources.ResourceManager.GetString("newAddress"));
            foreach (var item in DataBase.Instance.Query<AddressesDTO>())
                Addresses.Add(item.AddressName);
        }
    }
}
