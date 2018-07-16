using CookingApp.Enums;
using CookingApp.ViewModels.MainPage;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookingApp.ViewModels.UserPage
{
    public class UserTimeTableViewModel : ObservableViewModel
    {
        public WeekDaysEnum Code { get; set; }

        public string Day { get; set; }

        public bool IsWorking { get; set; }

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        public ICommand ChangeIsWorking
        {
            get
            {
                return new Command(() =>
                {
                    IsWorking = !IsWorking;
                    OnPropertyChangedModel(nameof(IsWorking));
                });
            }
        }
    }
}
