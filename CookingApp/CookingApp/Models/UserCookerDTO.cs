using System.Collections.Generic;

namespace CookingApp.Models
{
    public class UserCookerDTO
    {
        public string Description { get; set; }

        public string Image { get; set; }

        public double HoursPricing { get; set; }

        public List<string> Cuisines { get; set; }

        public List<UserTimeTableDTO> TimeTable { get; set; }
    }
}
