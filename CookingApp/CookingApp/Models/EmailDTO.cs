using SQLite;

namespace CookingApp.Models
{
    [Table("EMAILTABLE")]
    public class EmailDTO
    {
        [PrimaryKey,AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(Email))]
        public string Email { get; set; }
    }
}
