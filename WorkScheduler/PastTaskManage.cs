using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseControle;

namespace WorkScheduler
{
    class PastTaskManage
    {
        List<PastTask> taskList;
        internal List<PastTask> TaskList
        {
            get { return taskList; }
            set { taskList = value; }
        }

        public PastTaskManage()
        {
            taskList = new List<PastTask>();
        }

        /// <summary>
        /// 過去の作業実績一覧にタスクを追加する
        /// </summary>
        public void AddTask()
        {
            PastTask tempTask = new PastTask();
            tempTask.TaskName = "new";
            tempTask.WorkVolume = 0.0f;
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
        public void AddTask(string name, float volume)
        {
            PastTask tempTask = new PastTask();
            tempTask.TaskName = name;
            tempTask.WorkVolume = volume;
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
        public void AddTask(int id, string name, float volume)
        {
            PastTask tempTask = new PastTask();
            tempTask.IdNumber = id;
            tempTask.TaskName = name;
            tempTask.WorkVolume = volume;
            taskList.Add(tempTask);
        }
        public void AddTaskFromDB()
        {
            List<PastTask> pastTasks = LoadTaskFromDB();
            foreach(PastTask pastTask in pastTasks)
            {
                // DBのデータは秒単位で記録されている。
                // そのため、VolumeはHour単位に直し、かつ、最低1時間とする。
                float DBTaskVolume = (float)Math.Floor((pastTask.WorkVolume / 3600) + 1.0f);
                AddTask(pastTask.IdNumber, pastTask.TaskName, DBTaskVolume);
            }
        }
        /// <summary>
        /// 過去の作業実績一覧のタスクを削除する
        /// </summary>
        public void DeleteTask(int id)
        {
            int index = taskList.FindIndex(data => data.IdNumber == id);
            if (index != -1)
            {
                taskList.RemoveAt(index);
            }
        }
        /// <summary>
        /// タスクの内容を編集する
        /// </summary>
        public void EditTask(string name, float volume, int id)
        {
            int index = taskList.FindIndex(data => data.IdNumber == id);
            taskList[index].TaskName = name;
            taskList[index].WorkVolume = volume;
        }

        /// <summary>
        /// ファイルから過去の作業実績一覧を読み込む
        /// </summary>
        public void LoadTask()
        {
        }
        /// <summary>
        /// ファイルにタスク一覧を保存する
        /// </summary>
        public void SaveTask()
        {
        }
        /// <summary>
        /// DBから過去の作業実績を読み込む
        /// </summary>
        public void LoadTaskFromDB(int id)
        {
            TaskData loadData = DataBaseControle.DataBaseControle.Select(id);
        }
        public List<PastTask> LoadTaskFromDB()
        {
            List<TaskData> loadDatas = DataBaseControle.DataBaseControle.Select();
            List<PastTask> pastTasks = new List<PastTask>();
            foreach(TaskData taskData in loadDatas)
            {
                PastTask pastTask = new PastTask();
                pastTask.IdNumber = taskData.Id;
                pastTask.TaskName = taskData.Name;
                pastTask.WorkVolume = taskData.WorkTime;
                pastTasks.Add(pastTask);
            }
            return pastTasks;
        }
        /// <summary>
        /// DBに過去の作業実績を掻き込む
        /// </summary>
        public void SaveTaskFromDB(PastTask saveTask)
        {

        }
        /// <summary>
        /// DBの過去の作業実績を更新する
        /// </summary>
        public void UpdateTaskFromDB(PastTask UpdateTask)
        {

        }

    }
}
