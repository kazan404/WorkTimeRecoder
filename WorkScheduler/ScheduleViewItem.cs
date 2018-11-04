using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkScheduler
{
    class ScheduleViewItem
    {
        int startCell;

        public int StartCell
        {
            get { return startCell; }
            set { startCell = value; }
        }
        int endCell;

        public int EndCell
        {
            get { return endCell; }
            set { endCell = value; }
        }
        string taskName;

        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }
    }
}
