using SQLite;

namespace CookingApp.Models
{
    [Table("ADDRESSTABLE")]
    public class AddressesDTO
    {
        [PrimaryKey,AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(Address))]
        public string Address { get; set; }
    }
}
