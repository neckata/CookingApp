using CookingApp.Enums;
using SQLite;
using System;

namespace CookingApp.Models
{
    [Table("NOTIFICATIONTABLE")]
    public class NotificationDTO
    {
        [PrimaryKey, AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        //Да се тегли като номенклатура ?
        [Column(nameof(Type))]
        public NotificationsTypesEnum Type { get; set; }

        [Column(nameof(DeviceID))]
        public int DeviceID { get; set; }

        [Column(nameof(DateSend))]
        public DateTime DateSend { get; set; }

        [Column(nameof(DateDelivered))]
        public DateTime DateDelivered { get; set; }

        [Column(nameof(DateRead))]
        public DateTime DateRead { get; set; }
    }
}
