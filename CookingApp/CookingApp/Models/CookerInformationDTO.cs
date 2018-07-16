using System.Collections.Generic;

namespace CookingApp.Models
{
    public class CookerInformationDTO
    {
        public List<RecipeDTO> Receipts { get; set; }

        public List<string> Cuisines { get; set; }

        public List<TimeTableRowDTO> Schedule { get; set; }
    }
}
