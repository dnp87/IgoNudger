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
using Android.Preferences;

namespace IgoNudger.Droid
{
    [Activity(Label = "@string/settings_name")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            using (var fm = FragmentManager.BeginTransaction())
            {
                var sf = new SettingsFragment();

                fm.Add(Android.Resource.Id.Content, sf);
                fm.Commit();
            }
        }
    }

    public class SettingsFragment: PreferenceFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AddPreferencesFromResource(Resource.Xml.preferences);
        }
    }
}