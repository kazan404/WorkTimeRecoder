using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    }
}
