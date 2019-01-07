using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataBaseControle;

namespace WorkTimeRecoder
{
    /// <summary>
    /// TaskListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TaskListWindow : Window
    {
        public List<TaskData> TaskDatas;
        private AddDBTask addDBTaskFunc;

        public TaskListWindow()
        {
            InitializeComponent();
        }
        public TaskListWindow(List<TaskData> taskValues, AddDBTask addFunc)
        {
            TaskDatas = taskValues;
            addDBTaskFunc = addFunc;
            InitializeComponent();
            TaskListBox.ItemsSource = TaskDatas.Select(data => data.Name);
        }

        private void TaskListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            addDBTaskFunc(TaskDatas[TaskListBox.SelectedIndex].Id);
            this.Close();
        }
    }
}
