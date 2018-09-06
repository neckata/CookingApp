using CookingApp.Enums;
using System;

namespace CookingApp.ViewModels.NotificationsPage
{
    public class NotificationViewModel
    {
        public int NotificationID { get; set; }

        public NotificationsTypesEnum NotificationType { get; set; }

        public DateTime NotificationSentTime { get; set; }

        public string NotificationTitle { get; set; }

        public string NotificationBody { get; set; }
    }
}
