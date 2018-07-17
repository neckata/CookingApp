﻿using Acr.UserDialogs;
using CookingApp.Helpers;
using CookingApp.Models;
using CookingApp.Resources;
using CookingApp.Services;
using CookingApp.ViewModels.MainPage;
using System.Collections.Generic;
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

            var addressesDTO = DataBase.Instance.Query<AddressesDTO>().ToList();
            if (addressesDTO.Count() == 0)
                Addresses.Add(new AddressViewModel());
            else
            {
                int counter = 0;
                List<AddressViewModel> list = new List<AddressViewModel>();
                foreach (var item in addressesDTO)
                {
                    Addresses.Add(new AddressViewModel()
                    {
                        City = item.City,
                        Neighborhood = item.Neighborhood,
                        Street = item.Street,
                        IDInBase = item.ID,
                        Name = item.AddressName,
                        ID = counter
                    });
                    counter++;
                }
            }
        }

        public ObservableCollection<AddressViewModel> Addresses { get; set; }

        public ICommand AddAddress
        {
            get
            {
                return new Command(() =>
                {
                    AddressViewModel addressViewModel = new AddressViewModel();
                    addressViewModel.ID = Addresses.Count;
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
                        ID = address.IDInBase,
                        City = address.City,
                        AddressName = address.Name,
                        Neighborhood = address.Neighborhood,
                        Street = address.Street
                    };

                    bool isSuccess = await _model.SaveAddress(addressDTO);

                    if (isSuccess)
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblSaveSuccess"));
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

                    Addresses.Remove(address);

                    if (Addresses.Count == 0)
                        Addresses.Add(new AddressViewModel());

                    OnPropertyChangedModel(nameof(Addresses));

                    bool isDeleted = true;

                    if (address.IDInBase > 0)
                        isDeleted = await _model.DeleteAddress(address.IDInBase);

                    if (isDeleted)
                        await UserDialogs.Instance.AlertAsync(AppResources.ResourceManager.GetString("lblDeleteSuccess"));
                });
            }
        }
    }
}
