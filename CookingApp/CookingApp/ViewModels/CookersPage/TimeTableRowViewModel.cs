using System.Collections.Generic;

namespace CookingApp.ViewModels.CookersPage
{
    public class TimeTableRowViewModel
    {
        public string Day { get; set; }

        public List<TimeTableCellViewModel> Hours { get; set; }
    }
}
