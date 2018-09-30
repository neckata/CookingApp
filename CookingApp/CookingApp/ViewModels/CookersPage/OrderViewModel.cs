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
using System.Collections.ObjectModel;
using System.Globalization;

namespace CookingApp.ViewModels.CookersPage
{
    public class OrderViewModel : ObservableViewModel
    {
        public OrderViewModel(int cookerID, string cookerName)
        {
            _cookerID = cookerID;
            _cookerName = cookerName;
            FromDate = Utility.FirstDateOfWeek(DateTime.Today.Year, Utility.GetWeekOfYear(DateTime.Now), CultureInfo.CurrentCulture);
            ToDate = FromDate.AddDays(6);
            LoadData();
        }

        private int _cookerID;

        private string _selectedAddress, _cookerName;

        private List<AddressesDTO> addresses;

        private CookersModel _model = new CookersModel();

        public bool IsBusy { get; set; }

        public bool NameValidation { get; set; }

        public bool PhoneValidation { get; set; }

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

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public DateTime Date { get; set; }

        public DateTime MinimumDate { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string SelectedAddress
        {
            get
            {
                return _selectedAddress;
            }
            set
            {
                _selectedAddress = value;
                var address = addresses.FirstOrDefault(x => x.AddressName == _selectedAddress);
                if (address != null)
                {
                    AddressName = address.AddressName;
                    Neighborhood = address.Neighborhood;
                    City = address.City;
                    Street = address.Street;
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

        public ObservableCollection<string> Addresses { get; set; }

        public List<TimeTableRowViewModel> TimeTable { get; set; }

        public ICommand LeftDate
        {
            get
            {
                return new Command(() =>
                {
                    GetTimeTable(-7);
                });
            }
        }

        public ICommand RightDate
        {
            get
            {
                return new Command(() =>
                {
                    GetTimeTable(7);
                });
            }
        }

        public ICommand Order
        {
            get
            {
                return new Command(async () =>
                {
                    if (!NameValidation && !PhoneValidation)
                    {

                        IsBusy = true;
                        OnPropertyChangedModel(nameof(IsBusy));

                        //TODO time validation
                        //https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/src/Forms/XLabs.Forms/Controls/ExtendedTimePicker.cs
                        var address = addresses.FirstOrDefault(x => x.AddressName == _selectedAddress);

                        var order = new OrderDTO()
                        {
                            Date = Date,
                            FromTime = FromTime,
                            ToTime = ToTime,
                            ProductsIncluded = ProductsIncluded,
                            CookerID = _cookerID,
                            CookerName = _cookerName
                        };
                        if (address != null)
                            order.AddressID = address.Id;
                        else
                        {
                            order.AddressName = AddressName;
                            order.City = City;
                            order.Neighborhood = Neighborhood;
                            order.Street = Street;
                        }

                        bool isSend = await _model.MakeOrder(order);
                        if (isSend)
                        {
                            await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblOrderSuccess"));
                            await PageTemplate.CurrentPage.NavigateBack();
                        }
                        else
                            await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblOrderFail"));

                        IsBusy = false;
                        OnPropertyChangedModel(nameof(IsBusy));
                    }
                    else
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblUpdateData"));
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
            MinimumDate = DateTime.Today.AddDays(1);

            UserDTO user = DataBase.Instance.Query<UserDTO>().First();
            Name = user.Name;
            Family = user.Family;
            Phone = user.Phone;
            Email = user.Email;

            if (string.IsNullOrEmpty(Name))
            {
                NameValidation = true;
                OnPropertyChangedModel(nameof(NameValidation));
            }

            if (string.IsNullOrEmpty(Phone))
            {
                PhoneValidation = true;
                OnPropertyChangedModel(nameof(PhoneValidation));
            }

            OnPropertyChangedModel(nameof(IsAddresEnabled));
            OnPropertyChangedModel(nameof(Name));
            OnPropertyChangedModel(nameof(Family));
            OnPropertyChangedModel(nameof(Phone));
            OnPropertyChangedModel(nameof(Email));

            Addresses = new ObservableCollection<string>();
            Addresses.Add(AppResources.ResourceManager.GetString("newAddress"));
            addresses = DataBase.Instance.Query<AddressesDTO>().ToList();
            foreach (var item in addresses)
                Addresses.Add(item.AddressName);

            OnPropertyChangedModel(nameof(Addresses));

            GetTimeTable(0);
        }

        private async void GetTimeTable(int weekChange)
        {
            IsBusy = true;
            OnPropertyChangedModel(nameof(IsBusy));

            FromDate = FromDate.AddDays(weekChange);
            ToDate = ToDate.AddDays(weekChange);
            TimeTable = await _model.GetTimeTable(_cookerID, FromDate);

            OnPropertyChangedModel(nameof(TimeTable));
            OnPropertyChangedModel(nameof(FromDate));
            OnPropertyChangedModel(nameof(ToDate));

            IsBusy = false;
            OnPropertyChangedModel(nameof(IsBusy));
        }
    }
}
