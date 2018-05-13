using SQLite;
using System;

namespace CookingApp.Models
{
    [Table("MOBILEDEVICEDTABLE")]
    public class MobileDeviceDTO
    {
        [PrimaryKey]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(IMEI))]
        public string IMEI { get; set; }

        [Column(nameof(FCM))]
        public string FCM { get; set; }
    }
}
