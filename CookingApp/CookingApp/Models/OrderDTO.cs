using SQLite;

namespace CookingApp.Models
{
    [Table("ORDERTABLE")]
    public class OrderDTO
    {
        [PrimaryKey,AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(CookerID))]
        public int CookerID { get; set; }

        [Column(nameof(ClientID))]
        public int ClientID { get; set; }

        [Column(nameof(AddressID))]
        public int AddressID { get; set; }

        [Column(nameof(ProductsIncluded))]
        public bool ProductsIncluded { get; set; }
    }
}
