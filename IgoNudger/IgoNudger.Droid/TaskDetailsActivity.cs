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
        TaskItem _taskItem;

        Button _saveButton;
        EditText _nameEdit;
        EditText _descriptionEdit;
        Switch _completedSwitch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var id = Intent.GetIntExtra("Id", 0);
            if( id != 0)
            {
                _taskItem = App.Current.TaskManager.GetTask(id);
            }
            else
            {
                _taskItem = new TaskItem();
            }

            SetContentView(Resource.Layout.TaskDetails);
            GetControlsToFields();
            SetControlValues(_taskItem);

            _saveButton.Click += SaveButton_Click;
        }

        private void SetControlValues(TaskItem taskItem)
        {
            _nameEdit.Text = taskItem.Name;
            _descriptionEdit.Text = taskItem.Description;
            _completedSwitch.Checked = taskItem.Completed;
        }

        private void GetControlsToFields()
        {
            _saveButton = FindViewById<Button>(Resource.Id.saveButton);
            _nameEdit = FindViewById<EditText>(Resource.Id.nameEdit);
            _descriptionEdit = FindViewById<EditText>(Resource.Id.descriptionEdit);
            _completedSwitch = FindViewById<Switch>(Resource.Id.completedSwitch);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _taskItem.Name = _nameEdit.Text;
            _taskItem.Description = _descriptionEdit.Text;
            _taskItem.Completed = _completedSwitch.Checked;

            App.Current.TaskManager.SaveTask(_taskItem);
            Finish();
        }
    }
}