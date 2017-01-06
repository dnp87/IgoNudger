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
        TaskRepository _repository;

        public TaskManager(SQLiteConnection conn)
        {
            _repository = new TaskRepository(conn);
        }

        public TaskItem GetTask(int id)
        {
            return _repository.GetTask(id);
        }

        internal IList<TaskItem> GetIncompleteTasks()
        {
            return new List<TaskItem>(_repository.GetIncompleteTasks());
        }

        public IList<TaskItem> GetTasks()
        {
            return new List<TaskItem>(_repository.GetTasks());
        }

        public int SaveTask(TaskItem item)
        {
            return _repository.SaveTask(item);
        }

        public int DeleteTask(int id)
        {
            return _repository.DeleteTask(id);
        }
    }
}
