using CookingApp.Enums;
using System;

namespace CookingApp.Models
{
    public class CommentDTO
    {
        public int ID { get; set; }

        public int CookerID { get; set; }

        public int ClientID { get; set; }

        public string Comment { get; set; }
        
        public double Rating { get; set; }

        public DateTime Date { get; set; }

        public CommentTypesEnums CommentType { get; set; }
    }
}
