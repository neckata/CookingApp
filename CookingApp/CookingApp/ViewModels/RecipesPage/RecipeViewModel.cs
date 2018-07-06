using System.Collections.Generic;

namespace CookingApp.ViewModels.RecipesPage
{
    public class RecipeViewModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public double TimeToCook { get; set; }

        public double Portions { get; set; }

        public List<string> NecessaryIngredients { get; set; }

        public string HowToCook { get; set; }
    }
}
