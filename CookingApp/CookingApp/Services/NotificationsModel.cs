using CookingApp.Helpers;
using CookingApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CookingApp.Services
{
    public class NotificationsModel
    {
        private RestfulClient _rc = new RestfulClient();

        public async Task<bool> Confirm(int orderID)
        {
            ResponseModel response = await _rc.PostDataAsync(Enums.PostActionMethods.ReplyOrder, new ReplyOrderDTO { Answer = true, OrderID = orderID });

            if (response.IsSuccessStatusCode)
            {
                ReponseDTO data = JsonConvert.DeserializeObject<ReponseDTO>(response.ResponseContent);
                if (data.Response)
                {
                    NotificationDTO item = DataBase.Instance.Query<NotificationDTO>().First(x => x.NotificationID == orderID);
                    item.IsOrderPending = false;
                    item.IsOrderAccepted = true;
                    DataBase.Instance.Update(item);
                }

                return data.Response;
            }
            else
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> Reject(int orderID)
        {
            ResponseModel response = await _rc.PostDataAsync(Enums.PostActionMethods.ReplyOrder, new ReplyOrderDTO { Answer = false, OrderID = orderID });

            if (response.IsSuccessStatusCode)
            {
                ReponseDTO data = JsonConvert.DeserializeObject<ReponseDTO>(response.ResponseContent);
                if (data.Response)
                {
                    NotificationDTO item = DataBase.Instance.Query<NotificationDTO>().First(x => x.NotificationID == orderID);
                    item.IsOrderPending = false;
                    item.IsOrderRejected = true;
                    DataBase.Instance.Update(item);
                }

                return data.Response;
            }
            else
            {
                return response.IsSuccessStatusCode;
            }
        }
    }
}
