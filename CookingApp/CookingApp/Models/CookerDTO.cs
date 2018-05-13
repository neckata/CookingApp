namespace CookingApp.Models
{
    public class CookerDTO
    { 
        public int ID { get; set; }

        public double Rating { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int OrdersCount { get; set; }

        public double HoursPricing { get; set; }

        public bool IsWorking { get; set; }
    }
 }