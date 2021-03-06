﻿using CookingApp.Enums;
using CookingApp.Interfaces;
using CookingApp.Models;
using CookingApp.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CookingApp.Helpers
{
    public class DataBase
    {
        private static DataBase instance = null;
        private static readonly object padlock = new object();
        private static IFileHelper service;
        private static SQLiteConnection _connection;

        private DataBase()
        {
            service = DependencyService.Get<IFileHelper>();
            _connection = new SQLiteConnection(service.GetLocalFilePath("cooking.db"));
            CreateTables();

        }

        public static DataBase Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataBase();
                    }
                    return instance;
                }
            }
        }

        public int Update<T>(T item) where T : new()
        {
            return _connection.Update(item);
        }

        public int Delete<T>(object key) where T : new()
        {
            return _connection.Delete<T>(key);
        }

        public int Add<T>(T item) where T : new()
        {
            return _connection.Insert(item);
        }

        public TableQuery<T> Query<T>() where T : new()
        {
            return _connection.Table<T>();
        }

        private bool disposing = true;

        public void Dispose()
        {
            if (disposing)
            {
                disposing = false;
                if (_connection != null)
                {
                    _connection.Close();
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }

        private void CreateTables()
        {
            _connection.CreateTable<UserDTO>();
            _connection.CreateTable<SubscriptionDTO>();
            _connection.CreateTable<RecipeCookerDTO>();
            _connection.CreateTable<NotificationDTO>();
            _connection.CreateTable<CuisineDTO>();
            _connection.CreateTable<ChatDTO>();
            _connection.CreateTable<CuisineFilterDTO>();
            _connection.CreateTable<CuisineSelectedDTO>();
            _connection.CreateTable<UserTimeTableDTO>();
            _connection.CreateTable<AddressesDTO>();
        }

        public async void LoadNomenclatures()
        {
            try
            {
                RestfulClient client = new RestfulClient();

                List<CuisineFilterDTO> cuisineFilterDTOs = await client.GetDataAsync<List<CuisineFilterDTO>>(GetActionMethods.CuisinesFilters);
                if (cuisineFilterDTOs.Count > 0)
                {
                    foreach (CuisineFilterDTO item in Instance.Query<CuisineFilterDTO>())
                    {
                        Instance.Delete<CuisineFilterDTO>(item.Code);
                    }

                    foreach (CuisineFilterDTO item in cuisineFilterDTOs)
                    {
                        Instance.Add(item);
                    }
                }

                List<CuisineDTO> cuisineDTOs = await client.GetDataAsync<List<CuisineDTO>>(GetActionMethods.Cuisines);
                if (cuisineDTOs.Count > 0)
                {
                    List<CuisineSelectedDTO> selectedCuisines = Instance.Query<CuisineSelectedDTO>().ToList();

                    foreach (CuisineDTO item in Instance.Query<CuisineDTO>())
                    {
                        Instance.Delete<CuisineDTO>(item.Code);
                    }

                    foreach (CuisineDTO item in cuisineDTOs)
                    {
                        Instance.Add(item);

                        for (int i = 0; i < selectedCuisines.Count; i++)
                        {
                            if (selectedCuisines[i].Code == item.Code)
                            {
                                selectedCuisines.Remove(selectedCuisines[0]);
                                i--;
                            }
                        }
                    }

                    foreach (CuisineSelectedDTO item in selectedCuisines)
                    {
                        Instance.Delete<CuisineSelectedDTO>(item.Code);
                    }
                }

                UserDTO user = Instance.Query<UserDTO>().First();
                UserModel model = new UserModel();
                if (!user.IsRegistered)
                {
                    model.RegisterUser();
                }
                else
                {
                    model.FillAddresses();
                    model.UpdateFCMAgain();
                }
            }
            catch (Exception)
            {
                //Exception ??
            }
        }
    }
}
