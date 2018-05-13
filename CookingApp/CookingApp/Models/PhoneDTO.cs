using SQLite;

namespace CookingApp.Models
{
    [Table("PHONETABLE")]
    public class PhoneDTO
    {
        [PrimaryKey,AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(Phone))]
        public string Phone { get; set; }
    }
}
