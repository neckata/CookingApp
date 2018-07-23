using CookingApp.Converters;
using Newtonsoft.Json;
using System;

namespace CookingApp.Models
{
    public class OrderDTO
    {
        public int ID { get; set; }

        public int CookerID { get; set; }

        public string CookerName { get; set; }

        public bool ProductsIncluded { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Date { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }

        public int? AddressID { get; set; }

        public string AddressName { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Street { get; set; } 

        public double? Rating { get; set; }
    }
}
