using CookingApp.Enums;
using SQLite;

namespace CookingApp.Models
{
    [Table("USERTABLE")]
    public class UserDTO
    {
        [PrimaryKey, AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(Name))]
        public string Name { get; set; }

        [Column(nameof(UserType)]
        public UserTypesEnum UserType { get; set; }
    }
 }