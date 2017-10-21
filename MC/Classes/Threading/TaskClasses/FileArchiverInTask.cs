﻿using System;
using MC.Abstract_and_Parent_Classes;

namespace MC.Classes.Threading.TaskClasses
{
    class FileArchiverInTask : FileArchiver
    {
        public FileArchiverInTask(string sourcePathOfFile) : base(sourcePathOfFile)
        {
        }

        public override void Archive()
        {
            var tasks = new System.Threading.Tasks.Task[filesQueue.Length];
            for (int i = 0; i < filesQueue.Length; i++)
            {
                var queue = filesQueue[i];
                tasks[i] = System.Threading.Tasks.Task.Run(() =>
                {
                    for (int j = 0; j < queue.Count; j++)
                    {
                        ArchiveFileInEntry(queue.Dequeue());
                    }
                });
            }
            System.Threading.Tasks.Task.Run(() =>
            {
                if (tasks.IsComplite())
                {
                    archive.Dispose();
                    GC.Collect(2);
                    GC.WaitForPendingFinalizers();
                }
            });
        }

        internal override void Closing()
        {
            //TO DO: 
        }
    }
}