using CookingApp.Converters;
using Newtonsoft.Json;
using SQLite;
using System;

namespace CookingApp.Models
{
    [Table("ORDERTABLE")]
    public class OrderDTO
    {
        [PrimaryKey,AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(CookerID))]
        public int CookerID { get; set; }

        [Column(nameof(CookerName))]
        public string CookerName { get; set; }

        [Column(nameof(ProductsIncluded))]
        public bool ProductsIncluded { get; set; }

        [Column(nameof(Date))]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Date { get; set; }

        [Column(nameof(FromTime))]
        public TimeSpan FromTime { get; set; }

        [Column(nameof(ToTime))]
        public TimeSpan ToTime { get; set; }

        [Column(nameof(AddressID))]
        public int? AddressID { get; set; }

        [Column(nameof(AddressName))]
        public string AddressName { get; set; }

        [Column(nameof(City))]
        public string City { get; set; }

        [Column(nameof(Neighborhood))]
        public string Neighborhood { get; set; }

        [Column(nameof(Street))]
        public string Street { get; set; } 
    }
}
