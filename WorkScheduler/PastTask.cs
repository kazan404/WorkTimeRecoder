using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkScheduler
{
    class PastTask : TaskElement
    {
        int idNumber;
        public int IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }

        DateTime lastUpdate;
        public DateTime LastUpdate
        {
            get { return lastUpdate; }
        }

        public PastTask()
        {
            
        }
        public PastTask(int id, string name, float workVolume)
        {
            idNumber = id;
            base.TaskName = name;
            base.WorkVolume = workVolume;
            UpdateDate();
        }

        /// <summary>
        /// 更新日付を自動的に変更するためにプロパティをオーバーライドする
        /// </summary>
        public override string TaskName
        {
            get
            {
                return base.TaskName;
            }
            set
            {
                base.TaskName = value;
                UpdateDate();
            }
        }
        public override float WorkVolume
        {
            get
            {
                return base.WorkVolume;
            }
            set
            {
                base.WorkVolume = value;
                UpdateDate();
            }
        }

        /// <summary>
        /// 更新日付を更新する
        /// </summary>
        void UpdateDate()
        {
            lastUpdate = DateTime.Today;
        }
    }
}
