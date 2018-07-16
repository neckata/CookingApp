using CookingApp.Enums;
using SQLite;
using System;

namespace CookingApp.Models
{
    [Table("USERTIMETABLE")]
    public class UserTimeTableDTO
    {
        [PrimaryKey]
        [Column(nameof(Code))]
        public WeekDaysEnum Code { get; set; }

        [Column(nameof(Day))]
        public string Day { get; set; }

        [Column(nameof(IsWorking))]
        public bool IsWorking { get; set; }

        [Column(nameof(From))]
        public TimeSpan From { get; set; }

        [Column(nameof(To))]
        public TimeSpan To { get; set; }
    }
}
