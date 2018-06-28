using SQLite;
using System;

namespace CookingApp.Models
{
    [Table("CHATTABLE")]
    public class ChatDTO
    {
        [PrimaryKey]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(NotificationID))]
        public int NotificationID { get; set; }

        [Column(nameof(ClientID))]
        public int ClientID { get; set; }

        [Column(nameof(CookerID))]
        public int CookerID { get; set; }

        [Column(nameof(Message))]
        public string Message { get; set; }

        [Column(nameof(DateSend))]
        public DateTime DateSend { get; set; }
    }
}