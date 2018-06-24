using CookingApp.Enums;
using SQLite;

namespace CookingApp.Models
{
    [Table("CUISINEFILTERTABLE")]
    public class CuisineFilterDTO
    {
        [PrimaryKey]
        [Column(nameof(Code))]
        public CuisineTypeEnums Code { get; set; }

        [Column(nameof(Description))]
        public string Description { get; set; }
    }
}
