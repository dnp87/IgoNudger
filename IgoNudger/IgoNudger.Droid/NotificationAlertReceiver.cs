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
    public class NotificationAlertReceiver : BroadcastReceiver
    {
        private static readonly int _notificationId = 1000;

        public NotificationAlertReceiver()
        {

        }

        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock w1 = pm.NewWakeLock(WakeLockFlags.Partial, "NotificationReceiver");
            w1.Acquire();
            var nMgr = (NotificationManager)context.GetSystemService(Context.NotificationService);
            var notification = new Notification(Resource.Drawable.Icon, "Arrival");
            var pendingIntent = PendingIntent.GetActivity(context, 0, new Intent(context, typeof(MainActivity)), 0);
            SendNtfIfRequired(context);
            w1.Release(); ;
        }

        public void CancelAlarm(Context context)
        {
            Intent intent = new Intent(context, this.Class);
            PendingIntent sender = PendingIntent.GetBroadcast(context, 0, intent, 0);
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(sender);
        }

        public void SetAlarm(Context context, int alertTimeSeconds)
        {
            long now = SystemClock.ElapsedRealtime();
            AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
            Intent intent = new Intent(context, this.Class);
            PendingIntent pi = PendingIntent.GetBroadcast(context, 0, intent, 0);
            am.Set(AlarmType.ElapsedRealtimeWakeup, now + ((long)(alertTimeSeconds * 1000)), pi);
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