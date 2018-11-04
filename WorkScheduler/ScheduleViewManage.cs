using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkScheduler
{
    class ScheduleViewManage
    {
        List<ScheduleViewItem> scheduleViewItemList = new List<ScheduleViewItem>();
        internal List<ScheduleViewItem> ScheduleViewItemList
        {
            get { return scheduleViewItemList; }
            set { scheduleViewItemList = value; }
        }

    }
}
