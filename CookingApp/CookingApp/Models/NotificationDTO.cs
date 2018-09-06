using CookingApp.Enums;
using SQLite;
using System;

namespace CookingApp.Models
{
    [Table("NOTIFICATIONTABLE")]
    public class NotificationDTO
    {
        [PrimaryKey]
        [Column(nameof(NotificationID))]
        public int NotificationID { get; set; }

        [Column(nameof(NotificationType))]
        public NotificationsTypesEnum NotificationType { get; set; }

        [Column(nameof(NotificationSentTime))]
        public DateTime NotificationSentTime { get; set; }

        [Column(nameof(NotificationTitle))]
        public string NotificationTitle { get; set; }

        [Column(nameof(NotificationBody))]
        public string NotificationBody { get; set; }
    }
}
