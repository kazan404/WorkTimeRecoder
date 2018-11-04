using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkScheduler
{
    class ScheduleTask : TaskElement
    {
        int idNumber;
        public int IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }

        DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        int priority;
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public ScheduleTask()
        {
            
        }
    }
}
