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
        ListView _listView;
        IList<TaskItem> _tasks;

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

            var testNtfButton = FindViewById<Button>(Resource.Id.testNtfButton);
            testNtfButton.Click += TestNtfButton_Click;
		}

        private void TestNtfButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("todo");
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


