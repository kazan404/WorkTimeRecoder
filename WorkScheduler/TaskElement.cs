using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkScheduler
{
    class TaskElement
    {
        string taskName;
        public virtual string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        float workVolume;
        public virtual float WorkVolume
        {
            get { return workVolume; }
            set { workVolume = value; }
        }

        public TaskElement()
        {
            taskName = "Empty";
            workVolume = 0.0f;
        }
    }
}
