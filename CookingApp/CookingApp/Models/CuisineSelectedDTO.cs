using SQLite;

namespace CookingApp.Models
{
    [Table("CUISINESELECTEDTABLE")]
    public class CuisineSelectedDTO
    {
        [PrimaryKey]
        [Column(nameof(Code))]
        public string Code { get; set; }
    }
}
