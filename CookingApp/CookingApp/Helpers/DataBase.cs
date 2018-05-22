using CookingApp.Interfaces;
using CookingApp.Models;
using SQLite;
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
            _connection.CreateTable<PhoneDTO>();
            _connection.CreateTable<AddressesDTO>();
            _connection.CreateTable<EmailDTO>();
            _connection.CreateTable<OrderDTO>();
            _connection.CreateTable<NotificationDTO>();
            _connection.CreateTable<CuisineDTO>();
            _connection.CreateTable<ChatDTO>();
        }
    }
}
