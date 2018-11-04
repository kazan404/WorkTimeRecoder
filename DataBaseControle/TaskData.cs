using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseControle
{
    class TaskData
    {
        public int id;
        public string name;
        public int workTime;

        public TaskData(int ID, string Name, int WorkTime)
        {
            id = ID;
            name = Name;
            workTime = WorkTime;
        }

        public TaskData(string Name, int WorkTime)
        {
            name = Name;
            workTime = WorkTime;
        }
    }
}
