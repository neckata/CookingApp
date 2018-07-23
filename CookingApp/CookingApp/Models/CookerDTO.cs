namespace CookingApp.Models
{
    public class CookerDTO
    { 
        public int ID { get; set; }

        public double Rating { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int OrdersCount { get; set; }

        public double HoursPricing { get; set; }
    }
 }