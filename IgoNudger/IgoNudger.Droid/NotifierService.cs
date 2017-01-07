using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NotificationCompat = Android.Support.V4.App.NotificationCompat;

namespace IgoNudger.Droid
{
    [Service]
    public class NotifierService : IntentService
    {
        private static readonly int _notificationId = 1000;

        //todo: add some time settings
        public NotifierService() : base("NotifierService")
        {
            SetTimer();
        }

        private void SetTimer()
        {
            var intent = new Intent("NotifierService");
            var alarmManager = (AlarmManager) GetSystemService(AlarmService);
            var pendingIntent = PendingIntent.GetService(this, 0, intent, PendingIntentFlags.CancelCurrent);
            alarmManager.SetRepeating(AlarmType.ElapsedRealtime, 10000, 10000, pendingIntent);
        }

        protected override void OnHandleIntent(Intent intent)
        {
            var ntfTextBuilder = new IncompleteTaskNtfTextBuilder(App.Current.TaskManager);
            var strs = ntfTextBuilder.GetEnumerableStrings();

            if (strs.Count > 0)
            {
                var builder = new NotificationCompat.Builder(this)
                .SetAutoCancel(true)  // Dismiss from the notif. area when clicked
                .SetContentTitle("TODO")
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetContentText("TODO"); // The message to display.

                var inboxStyle = new NotificationCompat.InboxStyle();
                foreach (var str in strs)
                {
                    inboxStyle.AddLine(str);
                }
                builder.SetStyle(inboxStyle);

                // Finally, publish the notification:
                NotificationManager notificationManager =
                    (NotificationManager)GetSystemService(Context.NotificationService);
                notificationManager.Notify(_notificationId, builder.Build());
            }
        }
    }
}