using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace IgoNudger.Droid
{
	[Activity (Label = "IgoNudger.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.addTaskButton);
            button.Click += delegate
            {
                var intent = new Intent(this, typeof(TaskDetailsActivity));

                //todo: set intent data when available
                StartActivity(intent);
            };
		}
	}
}


