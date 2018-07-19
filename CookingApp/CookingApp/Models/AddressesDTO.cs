using SQLite;

namespace CookingApp.Models
{
    [Table("ADDRESSTABLE")]
    public class AddressesDTO
    {
        [PrimaryKey]
        [Column(nameof(Id))]
        public int? Id { get; set; }

        [Column(nameof(AddressName))]
        public string AddressName { get; set; }

        [Column(nameof(City))]
        public string City { get; set; }

        [Column(nameof(Neighborhood)]
        public string Neighborhood { get; set; }

        [Column(nameof(Street))]
        public string Street { get; set; }
    }
}