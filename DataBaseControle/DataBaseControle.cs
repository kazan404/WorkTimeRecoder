using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DataBaseControle
{
    class DataBaseControle
    {
        string USER_DB_NAME = "usertask.db";

        public void CreateDB()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + USER_DB_NAME))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "create table member(ID INTEGER  PRIMARY KEY AUTOINCREMENT, Name TEXT, WorkTime INTEGER)";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Insert(List<TaskData> insertDatas)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + USER_DB_NAME))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        foreach (TaskData data in insertDatas)
                        {
                            command.CommandText = "insert into member (name,workTime) values('" + data.name + "', '" + data.workTime + "')";
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                connection.Close();
            }
        }

        public List<TaskData> select(string targetName)
        {
            List<TaskData> result = new List<TaskData>();
            using (var conn = new SQLiteConnection("Data Source=" + USER_DB_NAME))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * from member WHERE 'Name'='" + targetName + "'";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"].ToString());
                            string name = reader["Name"].ToString();
                            int workTime = Convert.ToInt32(reader["WorkTime"].ToString());
                            result.Add(new TaskData(id, name, workTime));
                        }
                    }
                }
                conn.Close();
            }
            return result;
        }
    }
}
