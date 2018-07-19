using CookingApp.Enums;
using SQLite;
using System.Collections.Generic;

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

        [Column(nameof(Password))]
        public string Password { get; set; }

        [Column(nameof(UserType))]
        public UserTypesEnum UserType { get; set; }

        [Column(nameof(IMEI))]
        public string IMEI { get; set; }

        [Column(nameof(FCM))]
        public string FCM { get; set; }

        //Да се тегли всеки път
        [Column(nameof(Rating))]
        public double Rating { get; set; }

        [Column(nameof(Name))]
        public string Name { get; set; }

        [Column(nameof(Family))]
        public string Family { get; set; }

        [Column(nameof(Email))]
        public string Email { get; set; }

        [Column(nameof(Phone))]
        public string Phone { get; set; }

        //Само за готвач полета, той си ги променя

        [Column(nameof(Description))]
        public string Description { get; set; }

        [Column(nameof(Image))]
        public string Image { get; set; }

        [Column(nameof(OrdersCount))]
        public int OrdersCount { get; set; }

        [Column(nameof(HoursPricing))]
        public double HoursPricing { get; set; }

        [Column(nameof(IsWorking))]
        public bool IsWorking { get; set; }

        [Column(nameof(IsRegistered))]
        public bool IsRegistered { get; set; }

        [Ignore]
        public List<string> Cuisines { get; set; }

        [Ignore]
        public List<UserTimeTableDTO> TimeTable { get; set; }
    }
 }
