﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgoNudger
{
    public class TaskDatabase
    {
        static object locker = new object();

        public SQLiteConnection database;

        public string path;

        public TaskDatabase(SQLiteConnection conn)
        {
            database = conn;
            // create the tables
            database.CreateTable<TaskItem>();
        }

        public IEnumerable<TaskItem> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<TaskItem>() select i).ToList();
            }
        }

        public IEnumerable<TaskItem> GetIncompleteItems()
        {
            lock (locker)
            {
                return (from i in database.Table<TaskItem>()
                        where !i.Completed
                        select i).ToList();
            }
        }

        public TaskItem GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<TaskItem>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem(TaskItem item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<TaskItem>(id);
            }
        }
    }
}
