using CookingApp.Enums;
using CookingApp.ViewModels.MainPage;
using System;
using Xamarin.Forms;

namespace CookingApp.ViewModels.NotificationsPage
{
    public class NotificationViewModel : ObservableViewModel
    {
        public int NotificationID { get; set; }

        public NotificationsTypesEnum NotificationType { get; set; }

        public DateTime NotificationSentTime { get; set; }

        public string NotificationTitle { get; set; }

        public string NotificationBody { get; set; }

        public bool IsOrderPending { get; set; }

        public bool IsOrderProcessed { get; set; }

        public Color NotificationColor { get; set; }

        public bool IsOrderRejected { get; set; }

        public bool IsOrderAccepted { get; set; }

        public void ChangeColor(Color color)
        {
            NotificationColor = color;
            OnPropertyChangedModel(nameof(NotificationColor));

            if (color == Color.Red)
            {
                IsOrderRejected = true;
                OnPropertyChangedModel(nameof(IsOrderRejected));
            }
            else
            {
                IsOrderAccepted = true;
                OnPropertyChangedModel(nameof(IsOrderAccepted));
            }

            IsOrderPending = false;
            IsOrderProcessed = true;

            OnPropertyChangedModel(nameof(IsOrderPending));
            OnPropertyChangedModel(nameof(IsOrderProcessed));
        }
    }
}
