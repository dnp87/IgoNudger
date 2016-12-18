using System;
using Android.App;
using System.IO;
using SQLite;

namespace IgoNudger.Droid
{
    [Application]
    public class App : Application
    {
        public static App Current { get; private set; }

        public TaskManager TaskManager { get; set; }
        SQLiteConnection conn;

        public App(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var sqliteFilename = "IgoNudgerDB.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new SQLiteConnection(path);

            TaskManager = new TaskManager(conn);
        }
    }
}