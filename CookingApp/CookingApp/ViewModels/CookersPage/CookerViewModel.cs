namespace CookingApp.ViewModels.CookersPage
{
    public class CookerViewModel 
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Rating { get; set; }

        public int OrdersCount { get; set; }

        public double HoursPricing { get; set; }
    }
}
