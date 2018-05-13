using CookingApp.Enums;
using SQLite;

namespace CookingApp.Models
{
    [Table("SUBSCRIPTIONTABLE")]
    public class SubscriptionDTO
    {
        [PrimaryKey]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(Type))]
        public NotificationsTypesEnum Type { get; set; }
    }
}
