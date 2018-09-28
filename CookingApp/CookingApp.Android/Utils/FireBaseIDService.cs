using Android.App;
using Firebase.Iid;
using CookingApp.Services;

namespace CookingApp.Droid.Utils
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FireBaseIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            UserModel model = new UserModel();
            model.UpdateUserFCM(FirebaseInstanceId.Instance.Token);
        }
    }
}
