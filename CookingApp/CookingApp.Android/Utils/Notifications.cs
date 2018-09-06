using Android.App;
using Android.Content;
using Android.Media;
using Firebase.Messaging;

namespace CookingApp.Droid.Utils
{
    [Service(Exported = false, Name = "com.companyname.CookingApp.firebasemessagingservice")]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class CookingAppFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            Intent intent = new Intent(this, typeof(MainActivity));

            var enumerator = message.Data.GetEnumerator();

            string data = "{";

            if (enumerator != null)
                while (enumerator.MoveNext())
                    data += "\"" + enumerator.Current.Key + "\":\"" + enumerator.Current.Value + "\",";

            intent.PutExtra("NotificationTitle", message.Data["NotificationTitle"]);
            intent.PutExtra("NotificationBody", message.Data["NotificationBody"]);
            data += "}";
            intent.PutExtra("data", data);
            intent.PutExtra("NotificationSentTime", message.SentTime);

            SendNotification(intent, message.Data["NotificationTitle"], message.Data["NotificationBody"], message.SentTime);
        }


        private void SendNotification(Intent intent, string messageTitle, string messageBody, long sendTime, RingtoneType ringtoneType = RingtoneType.Alarm)
        {
            intent.AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.Cook)
                .SetContentTitle(messageTitle)
                .SetContentText(messageBody)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                .SetWhen(sendTime)
                .SetVisibility(NotificationVisibility.Public)
                .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                .SetSound(RingtoneManager.GetDefaultUri(ringtoneType));

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}