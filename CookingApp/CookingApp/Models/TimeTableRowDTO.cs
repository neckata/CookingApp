using System.Collections.Generic;

namespace CookingApp.Models
{
    class TimeTableRowDTO
    {
        public string Day { get; set; }

        public List<TimeTableCellDTO> Hours { get; set; }
    }
}
