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
        TaskDatabase _db = null;

        public TaskRepository(SQLiteConnection conn)
        {
            _db = new TaskDatabase(conn);
        }

        public TaskItem GetTask(int id)
        {
            return _db.GetItem(id);
        }

        public IEnumerable<TaskItem> GetTasks()
        {
            return _db.GetItems();
        }

        public IEnumerable<TaskItem> GetIncompleteTasks()
        {
            return _db.GetIncompleteItems();
        }

        public int SaveTask(TaskItem item)
        {
            return _db.SaveItem(item);
        }

        public int DeleteTask(int id)
        {
            return _db.DeleteItem(id);
        }
    }
}
