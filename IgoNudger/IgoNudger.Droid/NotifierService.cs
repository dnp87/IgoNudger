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
        Context _context;

        public NotifierService() : base("NotifierService")
        {
            //make stubs happy
        }

        //todo: add some time settings
        public NotifierService(Context context) : base("NotifierService")
        {
            _context = context;
            SetTimer();
        }

        public void SetTimer()
        {
            var intent = new Intent("NotifierService");
            var sysAlarmService = _context.GetSystemService(Context.AlarmService);
            var alarmManager = (AlarmManager)sysAlarmService;
            var pendingIntent = PendingIntent.GetService(_context, 0, intent, PendingIntentFlags.CancelCurrent);
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