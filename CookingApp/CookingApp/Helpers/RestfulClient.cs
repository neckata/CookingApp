using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Threading;
using CookingApp.Enums;
using CookingApp.Models;
using CookingApp.Resources;
using Xamarin.Forms;
using CookingApp.Interfaces;
using System.Linq;
using Plugin.Connectivity;

namespace CookingApp.Helpers
{
    public partial class RestfulClient : IDisposable
    {
        private const string _baseUrlAddress = "http://109.160.42.216:8080/TheCookingApp/mobile/";

        private HttpClient Client
        {
            get
            {
                return GetOrCreateHttpClientInstance();
            }
        }
        private WeakReference<HttpClient> weakReferenceClient;

        public RestfulClient()
        {
            weakReferenceClient = new WeakReference<HttpClient>(GetInstance());
        }

        public async Task<T> GetDataAsync<T>(GetActionMethods method, string parameter = null) where T : new()
        {
            Uri requestUrl = BuildGetURL(method, parameter);
            T obj = new T();
            if (CrossConnectivity.Current.IsConnected)
            {
                if (await CrossConnectivity.Current.IsRemoteReachable(requestUrl.Host, requestUrl.Port, 5000))
                {
                    var cts = new CancellationTokenSource();
                    cts.CancelAfter(Client.Timeout);
                    try
                    {
                        HttpResponseMessage response = await Client.GetAsync(requestUrl, cts.Token);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseStr = await response.Content.ReadAsStringAsync();
                            obj = JsonConvert.DeserializeObject<T>(responseStr);
                        }
                        else
                            UserDialogs.Instance.Toast(string.Format(AppResources.ResourceManager.GetString("notSuccessfulMethod"), response.StatusCode), new TimeSpan(0, 0, 3));
                    }
                    catch (TaskCanceledException ex)
                    {
                        UserDialogs.Instance.Toast(string.Format(AppResources.ResourceManager.GetString("errorMethod"), ex.Message), new TimeSpan(0, 0, 3));
                    }
                }
                else
                {
                    UserDialogs.Instance.Toast(AppResources.ResourceManager.GetString("noServer"), new TimeSpan(0, 0, 3));
                }
            }
            else
            {
                UserDialogs.Instance.Toast(AppResources.ResourceManager.GetString("noInternet"), new TimeSpan(0, 0, 3));
            }

            return obj;
        }


        public async Task<ResponseModel> PostDataAsync<T>(PostActionMethods method, T data, bool showSuccess = false)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Uri requestUrl = BuildPostURL(method);
                if (await CrossConnectivity.Current.IsRemoteReachable(requestUrl.Host, requestUrl.Port, 5000))
                {
                    PostRequestModel model = BuildRequestDataModel(data);
                    string json = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var cts = new CancellationTokenSource();
                    cts.CancelAfter(Client.Timeout);
                    try
                    {
                        using (HttpResponseMessage response = await Client.PostAsync(requestUrl, content, cts.Token))
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            if (!response.IsSuccessStatusCode)
                                UserDialogs.Instance.Toast(string.Format(AppResources.ResourceManager.GetString("notSuccessfulMethod"), response.StatusCode), new TimeSpan(0, 0, 3));
                            return new ResponseModel(responseContent, response.StatusCode, response.IsSuccessStatusCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.Toast(string.Format(AppResources.ResourceManager.GetString("errorMethod"), ex.Message), new TimeSpan(0, 0, 3));
                    }
                }
                else
                {
                    UserDialogs.Instance.Toast(AppResources.ResourceManager.GetString("noServer"), new TimeSpan(0, 0, 3));
                }
            }
            else
            {
                UserDialogs.Instance.Toast(AppResources.ResourceManager.GetString("noInternet"), new TimeSpan(0, 0, 3));
            }

            return new ResponseModel(null, System.Net.HttpStatusCode.BadRequest);
        }

        private Uri BuildPostURL(PostActionMethods method)
        {
            string actionUrl = FindPostUrl(method);
            Uri uri = new Uri(_baseUrlAddress + actionUrl);
            return uri;
        }

        private Uri BuildGetURL(GetActionMethods method, string parameter = null)
        {
            string actionUrl = FindGetUrl(method);
            if (actionUrl.Contains("{0}") && parameter != null)
            {
                actionUrl = string.Format(actionUrl, parameter);
            }

            Uri uri = new Uri(_baseUrlAddress + actionUrl);
            return uri;
        }

        private HttpClient GetOrCreateHttpClientInstance()
        {
            HttpClient client;
            if (!weakReferenceClient.TryGetTarget(out client))
            {
                client = GetInstance();
                weakReferenceClient.SetTarget(client);
            }
            return client;
        }

        private HttpClient GetInstance()
        {
            HttpClient _client = new HttpClient() { Timeout = new TimeSpan(0, 0, 30), MaxResponseContentBufferSize = 256000 };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return _client;
        }

        private PostRequestModel BuildRequestDataModel<T>(T data)
        {
            if (data != null)
            {
                string json = JsonConvert.SerializeObject(data);

                string base64JSON = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

                UserDTO user = DataBase.Instance.Query<UserDTO>().Single();

                PostRequestModel model = new PostRequestModel
                {
                    IMEI = DependencyService.Get<IDevice>().GetIdentifier(),
                    Password = user.Password,
                    UserName = user.UserName,
                    Data = base64JSON
                };
                return model;
            }
            return null;
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    HttpClient client;
                    if (weakReferenceClient.TryGetTarget(out client))
                    {
                        client.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
