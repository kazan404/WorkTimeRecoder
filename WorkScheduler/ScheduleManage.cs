using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkScheduler
{
    class ScheduleManage
    {
        List<ScheduleTask> taskList;
        internal List<ScheduleTask> TaskList
        {
            get { return taskList; }
        }

        DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public ScheduleManage()
        {
            taskList = new List<ScheduleTask>();
            startDate = DateTime.Today;
            //CreateSample();
        }

        private void CreateSample()
        {
            taskList = new List<ScheduleTask>
            {
                new ScheduleTask{TaskName = "test1", WorkVolume = 2, Priority = 1, IdNumber=1},
                new ScheduleTask{TaskName = "test2", WorkVolume = 3, Priority = 2, IdNumber=4},
                new ScheduleTask{TaskName = "test3", WorkVolume = 5, Priority = 4, IdNumber=5},
                new ScheduleTask{TaskName = "test4", WorkVolume = 8, Priority = 3, IdNumber=13}
            };
        }

        /// <summary>
        /// スケジュールの再作成を行う
        /// </summary>
        public void RefreshSchedule()
        {
            DateTime beforeDate = startDate;

            //優先度順にスケジュールを埋めていく。
            //優先度は数字が小さいものを高としている。
            //優先度の最大数は登録されたタスクの数だけ存在するため、ループは保持しているタスクの数となる
            for (int i = 1; i < taskList.Count; i++)
            {
                int index = taskList.FindIndex(data => data.Priority == i);
                taskList[index].StartDate = beforeDate;
                taskList[index].EndDate = beforeDate.AddDays((double)taskList[index].WorkVolume);
                beforeDate = taskList[index].EndDate;
            }
        }
        /// <summary>
        /// スケジュール上からタスクを削除する
        /// </summary>
        public void DeleteTask(int id)
        {
            int index = taskList.FindIndex(data => data.IdNumber == id);
            int deletePriority = taskList[index].Priority;
            taskList.RemoveAt(index);
            //優先度の付け直しを行う
            foreach (ScheduleTask task in taskList)
            {
                if (task.Priority > deletePriority)
                {
                    task.Priority--;
                }
            }
        }
        /// <summary>
        /// スケジュールにタスクを追加する
        /// </summary>
        /// <param name="name">タスク名</param>
        /// <param name="volume">タスクの作業量</param>
        /// <param name="priority">優先度</param>
        public void AddTask(string name, float volume, int priority)
        {
            ScheduleTask tempTask = new ScheduleTask();
            tempTask.TaskName = name;
            tempTask.WorkVolume = volume;
            tempTask.Priority = priority;
            if (taskList.Count != 0)
            {
                tempTask.IdNumber = taskList.Select(data => data.IdNumber).Max() + 1;
                //優先度の付け直しを行う
                foreach (ScheduleTask task in taskList)
                {
                    if (task.Priority >= priority)
                    {
                        task.Priority++;
                    }
                }
            }
            else
            {
                tempTask.IdNumber = 1;
            }
            taskList.Add(tempTask);
        }
        /// <summary>
        /// ファイルからロードしたスケジュールのタスクを追加する
        /// </summary>
        /// <remarks>優先度の付け直しを行わない</remarks>
        /// <param name="name">タスク名</param>
        /// <param name="volume">タスクの作業量</param>
        /// <param name="priority">優先度</param>
        public void AddTaskForLoad(string name, float volume, int priority)
        {
            ScheduleTask tempTask = new ScheduleTask();
            tempTask.TaskName = name;
            tempTask.WorkVolume = volume;
            tempTask.Priority = priority;
            if (taskList.Count != 0)
            {
                tempTask.IdNumber = taskList.Select(data => data.IdNumber).Max() + 1;
            }
            else
            {
                tempTask.IdNumber = 1;
            }
            taskList.Add(tempTask);
        }
        /// <summary>
        /// タスクの内容を編集する
        /// </summary>
        public void EditTask(string name, float volume, int priority, int id)
        {
            int index = taskList.FindIndex(data => data.IdNumber == id);
            taskList[index].TaskName = name;
            taskList[index].WorkVolume = volume;
            int beforePriority = taskList[index].Priority;
            //優先度を上げた場合
            if (taskList[index].Priority > priority)
            {
                foreach (ScheduleTask task in taskList)
                {
                    if (task.Priority >= priority && task.Priority < beforePriority)
                    {
                        task.Priority++;
                    }
                }
                taskList[index].Priority = priority;
            }
            //優先度を下げた場合
            if (taskList[index].Priority < priority)
            {
                foreach (ScheduleTask task in taskList)
                {
                    if (task.Priority <= priority && task.Priority > beforePriority)
                    {
                        task.Priority--;
                    }
                }
                taskList[index].Priority = priority;
            }
        }
        /// <summary>
        /// 対象日時が休日か否かを判断する
        /// </summary>
        /// <remarks>休日：土曜日、日曜日、祝日</remarks>
        /// <param name="scheduleDate"></param>
        /// <returns>true=休日、false=平日</returns>
        public bool IsHoliday(DateTime scheduleDate)
        {
            int weekNumber = (int)scheduleDate.DayOfWeek;

            if (weekNumber == 0 || weekNumber == 6)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// スケジュールをファイルから読み込む
        /// </summary>
        public void LoadSchedule()
        {
        }
        /// <summary>
        /// スケジュールをファイルに保存する
        /// </summary>
        public void SaveSchedule()
        {
        }
    }
}
