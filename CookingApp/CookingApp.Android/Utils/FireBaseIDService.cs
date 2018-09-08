using Android.App;
using Firebase.Iid;
using CookingApp.Helpers;
using CookingApp.Models;

namespace CookingApp.Droid.Utils
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FireBaseIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            using (var client = new RestfulClient())
            {
                UserDTO user = DataBase.Instance.Query<UserDTO>().FirstOrDefault();
                user.FCM = FirebaseInstanceId.Instance.Token;
                DataBase.Instance.Update(user);
            }
        }
    }
}
