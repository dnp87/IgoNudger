using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace IgoNudger.Droid
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        ListView listView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            listView = FindViewById<ListView>(Resource.Id.taskList);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.addTaskButton);
            button.Click += delegate
            {
                var intent = new Intent(this, typeof(TaskDetailsActivity));
                intent.PutExtra("Id", 0);

                StartActivity(intent);
            };
		}

        protected override void OnResume()
        {
            base.OnResume();

            var tasks = App.Current.TaskManager.GetTasks();

            listView.Adapter = new TaskItemListAdapter(this, tasks);
        }
	}
}


