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

namespace IgoNudger.Droid
{
    public class TaskItemListAdapter : BaseAdapter<TaskItem>
    {
        Activity context;
        IList<TaskItem> items;

        public TaskItemListAdapter(Activity context, IList<TaskItem> items) : base ()
		{
            this.context = context;
            this.items = items;
        }

        public override TaskItem this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if( view == null )
            {
                //recreate when required
                view = context.LayoutInflater.Inflate(Resource.Layout.TaskListItem, null);
            }

            view.FindViewById<TextView>(Resource.Id.Text1).Text = items[position].Name;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = "";//todo
            view.FindViewById<CheckBox>(Resource.Id.Check1).Checked = items[position].Completed;
            return view;
        }
    }
}