using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgoNudger
{
    public class IncompleteTaskNtfTextBuilder
    {
        TaskManager _manager;

        public IncompleteTaskNtfTextBuilder(TaskManager manager)
        {
            _manager = manager;
        }

        public IList<string> GetEnumerableStrings()
        {
            var tasks = _manager.GetIncompleteTasks();

            return tasks.Select(o => o.Name).ToList();
        }

        public string GetText()
        {
            var tasks = _manager.GetIncompleteTasks();
            var sb = new StringBuilder();
            if( tasks.Count > 0)
            {
                foreach (var t in tasks)
                {
                    sb.AppendLine(t.Name);
                }

                return sb.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
