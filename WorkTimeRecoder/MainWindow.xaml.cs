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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataBaseControle;

namespace WorkTimeRecoder
{
    public delegate void BaloondSetter(string baloonStr1, string baloonStr2);
    public delegate void AddDBTask(int id);

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // TimeCounter timeCounter = new TimeCounter();

        private BaloondSetter baloonsetter;
        public BaloondSetter Baloonsetter
        {
            get => baloonsetter;
            set => baloonsetter = value;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// タイマーの追加ボタンを押したときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerPlusButton_Click(object sender, RoutedEventArgs e)
        {
            if(TimerListPanel.Children.Count >= 10)
            {
                return;
            }
            TimerPanel addTimerPanel = new TimerPanel();
            string newTimerName = "";
            int i = 0;
            while(true)
            {
                newTimerName = "Timer" + i;
                if (TimerListPanel.FindName(newTimerName) == null)
                {
                    break;
                }
                i++;
            }
            addTimerPanel.Name = newTimerName;
            addTimerPanel.DeleteTimerFunc = DeleteTimer;
            addTimerPanel.Baloonsetter = this.baloonsetter;
            TimerListPanel.Children.Add(addTimerPanel);
            TimerListPanel.RegisterName(addTimerPanel.Name, addTimerPanel);
        }

        public void AddDefinedIssueTimer(string issueName, string idNumber)
        {
            if (string.IsNullOrEmpty(issueName) || string.IsNullOrEmpty(idNumber))
            {
                return;
            }
            if (TimerListPanel.Children.Count >= 10)
            {
                return;
            }
            TimerPlusButton_Click(null, null);
            TimerPanel timerPanel = TimerListPanel.Children[TimerListPanel.Children.Count - 1] as TimerPanel;
            timerPanel.SetApperanceData(issueName, idNumber);
        }

        /// <summary>
        /// すでに追加済みのタイマーを削除する。
        /// </summary>
        /// <param name="timerName"></param>
        public void DeleteTimer(string timerName)
        {
            TimerPanel delTimerPanel = (TimerPanel)TimerListPanel.FindName(timerName);
            if(delTimerPanel == null)
            {
                return;
            }
            TimerListPanel.Children.Remove(delTimerPanel);
        }

        /// <summary>
        /// ウィンドウを閉じる命令を受け取るイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // ウィンドウの閉じるボタンでアプリ自体を終了させず、タスクトレイで動き続けるようにする。
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        ///　Loadボタンを押したときのイベント
        /// </summary>
        /// <remarks>メインウィンドウのLoadボタン</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadTimerButton_Click(object sender, RoutedEventArgs e)
        {
            List<TaskData> taskDatas = DataBaseControle.DataBaseControle.Select();
            TaskListWindow taskWindow = new TaskListWindow(taskDatas, AddTask);
            taskWindow.Owner = GetWindow(this);
            taskWindow.ShowDialog();
            return;
        }

        private void AddTask(int id)
        {
            TaskData taskdata = DataBaseControle.DataBaseControle.Select(id);
            TimerPlusButton_Click(null, null);
            TimerPanel lastPanel = ((TimerPanel)(TimerListPanel.Children[TimerListPanel.Children.Count - 1]));
            lastPanel.IDNumberLabel.Content = taskdata.Id.ToString();
            lastPanel.IssueNameText.Text = taskdata.Name.ToString();
            return;
        }
    }
}
