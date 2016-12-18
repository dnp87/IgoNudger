using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgoNudger
{
    public class TaskManager
    {
        TaskRepository repository;

        public TaskManager(SQLiteConnection conn)
        {
            repository = new TaskRepository(conn);
        }

        public TaskItem GetTask(int id)
        {
            return repository.GetTask(id);
        }

        public IList<TaskItem> GetTasks()
        {
            return new List<TaskItem>(repository.GetTasks());
        }

        public int SaveTask(TaskItem item)
        {
            return repository.SaveTask(item);
        }

        public int DeleteTask(int id)
        {
            return repository.DeleteTask(id);
        }
    }
}
