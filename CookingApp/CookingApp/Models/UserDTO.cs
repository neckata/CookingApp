﻿using CookingApp.Enums;
using SQLite;

namespace CookingApp.Models
{
    [Table("USERTABLE")]
    public class UserDTO
    {
        [PrimaryKey, AutoIncrement]
        [Column(nameof(ID))]
        public int ID { get; set; }

        [Column(nameof(UserName))]
        public string UserName { get; set; }

        [Column(nameof(UserType))]
        public UserTypesEnum UserType { get; set; }

        //Да се тегли всеки път
        [Column(nameof(Rating))]
        public double Rating { get; set; }

        [Column(nameof(Name))]
        public string Name { get; set; }

        //Само за готвач полета, той си ги променя

        [Column(nameof(Description))]
        public string Description { get; set; }

        [Column(nameof(OrdersCount))]
        public int OrdersCount { get; set; }

        [Column(nameof(HoursPricing))]
        public double HoursPricing { get; set; }

        [Column(nameof(IsWorking))]
        public bool IsWorking { get; set; }
    }
 }