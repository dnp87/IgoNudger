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
    [Activity(Label = "@string/taskDetailsTitle")]
    public class TaskDetailsActivity : Activity
    {
        TaskItem taskItem;

        Button saveButton;
        EditText nameEdit;
        EditText descriptionEdit;
        Switch completedSwitch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var id = Intent.GetIntExtra("Id", 0);
            if( id != 0)
            {
                taskItem = App.Current.TaskManager.GetTask(id);
            }
            else
            {
                taskItem = new TaskItem();
            }

            SetContentView(Resource.Layout.TaskDetails);
            GetControlsToFields();
            SetControlValues(taskItem);

            saveButton.Click += SaveButton_Click;
        }

        private void SetControlValues(TaskItem taskItem)
        {
            nameEdit.Text = taskItem.Name;
            descriptionEdit.Text = taskItem.Description;
            completedSwitch.Checked = taskItem.Completed;
        }

        private void GetControlsToFields()
        {
            saveButton = FindViewById<Button>(Resource.Id.saveButton);
            nameEdit = FindViewById<EditText>(Resource.Id.nameEdit);
            descriptionEdit = FindViewById<EditText>(Resource.Id.descriptionEdit);
            completedSwitch = FindViewById<Switch>(Resource.Id.completedSwitch);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            taskItem.Name = nameEdit.Text;
            taskItem.Description = descriptionEdit.Text;
            taskItem.Completed = completedSwitch.Checked;

            App.Current.TaskManager.SaveTask(taskItem);
            Finish();
        }
    }
}