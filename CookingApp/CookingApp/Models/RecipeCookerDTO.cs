using SQLite;

namespace CookingApp.Models
{
    //Запазват се само рецептите на готвача който е вписан, изпозлва се за parse На JSON
    [Table("RECIPETTABLE")]
    public class RecipeCookerDTO
    {
        [PrimaryKey,AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(CookerID))]
        public int CookerID { get; set; }

        [Column(nameof(Picture))]
        public string Picture { get; set; }

        [Column(nameof(Description))]
        public string Description { get; set; }

        [Column(nameof(CuisineCode))]
        public string CuisineCode { get; set; }
    }
}
