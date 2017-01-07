using System;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using NotificationCompat = Android.Support.V4.App.NotificationCompat;

namespace IgoNudger.Droid
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        ListView _listView;
        IList<TaskItem> _tasks;

        private static readonly int ButtonClickNotificationId = 1000;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            _listView = FindViewById<ListView>(Resource.Id.taskList);
            _listView.ItemClick += ListView_ItemClick;

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.addTaskButton);
            button.Click += delegate
            {
                var intent = new Intent(this, typeof(TaskDetailsActivity));
                intent.PutExtra("Id", 0);

                StartActivity(intent);
            };

            var service = new NotifierService();
		}

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(TaskDetailsActivity));
            intent.PutExtra("Id", _tasks[e.Position].Id);

            StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();

            _tasks = App.Current.TaskManager.GetTasks();
            _listView.Adapter = new TaskItemListAdapter(this, _tasks);
        }
	}
}


