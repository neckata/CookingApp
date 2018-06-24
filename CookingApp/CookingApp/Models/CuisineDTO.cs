using CookingApp.Enums;
using SQLite;

namespace CookingApp.Models
{
    [Table("CUISINETABLE")]
    public class CuisineDTO
    {
        [PrimaryKey]
        [Column(nameof(Code))]
        public string Code { get; set; }

        [Column(nameof(CuisineTypeCode))]
        public CuisineTypeEnums CuisineTypeCode { get; set; }

        [Column(nameof(IsSelected))]
        public bool IsSelected { get; set; }

        [Column(nameof(Description))]
        public string Description { get; set; }
    }
}
