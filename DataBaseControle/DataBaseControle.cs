using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace DataBaseControle
{
    static public class DataBaseControle
    {
        static public string USER_DB_NAME = "usertask.db";
        static public string DB_PATH = "";

        /// <summary>
        /// データベースを作成する
        /// </summary>
        static public void CreateDB()
        {
            if(File.Exists(USER_DB_NAME) == true)
            {
                return;
            }
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

        /// <summary>
        /// データべ―スを削除する
        /// </summary>
        static public void DeleteDB()
        {
            if (File.Exists(USER_DB_NAME) == true)
            {
                File.Delete(USER_DB_NAME);
            }
            return;
        }

        /// <summary>
        /// データベースにレコードを追加する(Insertコマンド)
        /// </summary>
        /// <param name="insertDatas"></param>
        static public void Insert(List<TaskData> insertDatas)
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

        /// <summary>
        /// データベースから目的のデータを見つける
        /// </summary>
        /// <param name="targetName">探したいタスクのタスク名</param>
        /// <returns></returns>
        static public List<TaskData> Select(string targetName)
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
        /// <summary>
        /// データベースから目的のデータを見つける
        /// </summary>
        /// <param name="targetID">探したいタスクのID</param>
        /// <returns></returns>
        static public List<TaskData> Select(int targetID)
        {
            List<TaskData> result = new List<TaskData>();
            using (var conn = new SQLiteConnection("Data Source=" + USER_DB_NAME))
            {
                conn.Open();
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * from member WHERE 'ID'='" + targetID.ToString() + "'";
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

        static public void Update(TaskData targetData)
        {

        }
    }
}
