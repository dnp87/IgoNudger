using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgoNudger
{
    public class TaskRepository
    {
        TaskDatabase db = null;

        public TaskRepository(SQLiteConnection conn)
        {
            db = new TaskDatabase(conn);
        }

        public TaskItem GetTask(int id)
        {
            return db.GetItem(id);
        }

        public IEnumerable<TaskItem> GetTasks()
        {
            return db.GetItems();
        }

        public int SaveTask(TaskItem item)
        {
            return db.SaveItem(item);
        }

        public int DeleteTask(int id)
        {
            return db.DeleteItem(id);
        }
    }
}
