using System;

namespace CookingApp.Models
{
    public class ReceipDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int Description { get; set; }

        public int Image { get; set; }

        public DateTime TimeToCook { get; set; }
    }
}
