using System.Collections.Generic;

namespace CookingApp.Models
{
    public class RecipeDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public int TimeToCook { get; set; }

        public int Portions { get; set; }

        public List<string> NecessaryIngredients { get; set; }

        public string HowToCook { get; set; }
    }
}
