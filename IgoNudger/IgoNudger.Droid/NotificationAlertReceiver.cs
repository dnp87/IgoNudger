using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using NotificationCompat = Android.Support.V4.App.NotificationCompat;
using Android.Icu.Util;

namespace IgoNudger.Droid
{
    [BroadcastReceiver]
    public class NotificationAlertReceiver : BroadcastReceiver
    {
        private static readonly int _notificationId = 1000;

        public NotificationAlertReceiver()
        {

        }

        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock w1 = pm.NewWakeLock(WakeLockFlags.Partial, "NotificationAlertReceiver");
            w1.Acquire();
            SendNtfIfRequired(context);
            w1.Release();
        }

        public void CancelAlarm(Context context)
        {
            Intent intent = new Intent(context, this.Class);
            PendingIntent sender = PendingIntent.GetBroadcast(context, 0, intent, 0);
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(sender);
        }

        public void SetAlarm(Context context, int hh, int mm)
        {
            Intent intent = new Intent(context, this.Class);
            PendingIntent pi = PendingIntent.GetBroadcast(context, 0, intent, 0);

            Java.Util.Calendar calendar = Java.Util.Calendar.Instance;
            calendar.Set(Java.Util.CalendarField.HourOfDay, hh);
            calendar.Set(Java.Util.CalendarField.Minute, mm);
            calendar.Set(Java.Util.CalendarField.Second, 0);

            AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
            am.SetRepeating(AlarmType.RtcWakeup, calendar.TimeInMillis, AlarmManager.IntervalDay, pi);
        }

        private void SendNtfIfRequired(Context context)
        {
            var ntfTextBuilder = new IncompleteTaskNtfTextBuilder(App.Current.TaskManager);
            var strs = ntfTextBuilder.GetEnumerableStrings();

            if (strs.Count > 0)
            {
                var builder = new NotificationCompat.Builder(context)
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
                    (NotificationManager) context.GetSystemService(Context.NotificationService);
                notificationManager.Notify(_notificationId, builder.Build());
            }
        }
    }
}