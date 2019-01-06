using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseControle
{
    public class TaskData
    {
        private int id;
        public int Id { get => id; set => id = value; }
        
        private string name;
        public string Name { get => name; set => name = value; }

        private int workTime;
        public int WorkTime { get => workTime; set => workTime = value; }

        public TaskData()
        {
            return;
        }

        public TaskData(int ID, string Name, int WorkTime)
        {
            id = ID;
            name = Name;
            workTime = WorkTime;
            return;
        }

        public TaskData(string Name, int WorkTime)
        {
            name = Name;
            workTime = WorkTime;
            return;
        }
    }
}
