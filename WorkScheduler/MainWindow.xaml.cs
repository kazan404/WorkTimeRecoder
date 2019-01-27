using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace WorkScheduler
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly int SPRIT_DAY_TARM = 7;

        static string headerColumnName = "Header";

        ScheduleManage scheduleManage;
        PastTaskManage pastTaskManage;

        ScheduleViewManage scheduleViewManage;

        DataTable ViewTable;

        int editID;

        string editViewName;
        int selectRowOnPastTask;
        int selectRowOnSchedule;

        public MainWindow()
        {
            InitializeComponent();
            scheduleManage = new ScheduleManage();
            pastTaskManage = new PastTaskManage();
            scheduleViewManage = new ScheduleViewManage();
            ViewTable = new DataTable();
            DatePicker_StartDate.SelectedDate = scheduleManage.StartDate;
            editID = -1;
            selectRowOnPastTask = -1;
            selectRowOnSchedule = -1;
            pastTaskManage.AddTaskFromDB();
            RefreshPastTask();
        }
        /// <summary>
        /// メニュー「ファイル-読み込み-作業実績」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_PastTask_Load_Click(object sender, RoutedEventArgs e)
        {
            LoadPastTaskDB();
        }
        /// <summary>
        /// メニュー「ファイル-読み込み-スケジュール」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Schedule_Load_Click(object sender, RoutedEventArgs e)
        {
            LoadSchedule();
        }
        /// <summary>
        /// メニュー「ファイル-保存-作業実績」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_PastTask_Save_Click(object sender, RoutedEventArgs e)
        {
            SavePastTask();
        }
        /// <summary>
        /// メニュー「ファイル-保存-スケジュール」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Schedule_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveSchedule();
        }
        /// <summary>
        /// メニュー「ファイル-終了」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// メニュー「編集―過去のタスク―新規作成」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_PastTask_New_Click(object sender, RoutedEventArgs e)
        {
            AddPastTask();
        }
        /// <summary>
        /// メニュー「編集―過去のタスク―スケジュールに移動」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_PastTask_AddSchedule_Click(object sender, RoutedEventArgs e)
        {
            AddScheduleFromPastTask();
        }
        /// <summary>
        /// メニュー「編集―過去のタスク―削除」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_PastTask_Delete_Click(object sender, RoutedEventArgs e)
        {
            RemovePastTask();
        }
        /// <summary>
        /// メニュー「編集―スケジュールのタスク―優先度を上げる」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Schedule_UpPriority_Click(object sender, RoutedEventArgs e)
        {
            UpPriority();
        }
        /// <summary>
        /// メニュー「編集―スケジュールのタスク―優先度を下げる」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Schedule_DownPriority_Click(object sender, RoutedEventArgs e)
        {
            downPriority();
        }
        /// <summary>
        /// メニュー「編集―スケジュールのタスク―削除」をクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Schedule_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteScheduleTask();
        }
        /// <summary>
        /// 過去の作業実績一覧にあるタスクをダブルクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PastTask_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddScheduleFromPastTask();
        }
        /// <summary>
        /// 過去の作業実績一覧の新規作成ボタンが押された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_New_Click(object sender, RoutedEventArgs e)
        {
            AddPastTask();
        }
        /// <summary>
        /// 過去の作業実績一覧の削除ボタンが押された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            RemovePastTask();
        }

        /// <summary>
        /// 過去の作業実績一覧でタスクの選択状態が変更された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PastTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // タスク一覧に選択が移ったので、スケジュールの選択を解除しておく
            Grid_Schedule.UnselectAll();

            if (ListBox_PastTask.SelectedIndex == -1)
            {
                return;
            }
            editViewName = "PastTask";
            selectRowOnPastTask = ListBox_PastTask.SelectedIndex;
            Text_Name.IsEnabled = true;
            Text_Name.Text = pastTaskManage.TaskList[ListBox_PastTask.SelectedIndex].TaskName;
            Text_WorkVolume.IsEnabled = true;
            Text_WorkVolume.Text = pastTaskManage.TaskList[ListBox_PastTask.SelectedIndex].WorkVolume.ToString();
            editID = pastTaskManage.TaskList[ListBox_PastTask.SelectedIndex].IdNumber;
            Text_Priority.IsEnabled = false;
            Text_Priority.Text = "";
            Label_StartDateValue.Content = "";
            Label_EndDateValue.Content = "";
        }

        /// <summary>
        /// スケジュールの選択セルが変わったときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Schedule_CurrentCellChanged(object sender, EventArgs e)
        {
            // スケジュールに選択が移ったので、タスク一覧の選択を解除しておく
            ListBox_PastTask.UnselectAll();

            if (Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) == -1)
            {
                return;
            }
            editViewName = "Schedule";
            selectRowOnSchedule = Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem);
            // 行インデックス＝優先度
            Text_Name.IsEnabled = true;
            Text_Name.Text = scheduleManage.TaskList.Find(data
                => data.Priority == Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) + 1).TaskName;
            Text_WorkVolume.IsEnabled = true;
            Text_WorkVolume.Text = scheduleManage.TaskList.Find(data
                => data.Priority == Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) + 1).WorkVolume.ToString();
            Text_Priority.IsEnabled = true;
            Text_Priority.Text = scheduleManage.TaskList.Find(data
                => data.Priority == Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) + 1).Priority.ToString();
            Label_StartDateValue.Content = scheduleManage.TaskList.Find(data
                => data.Priority == Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) + 1).StartDate.ToShortDateString();
            Label_EndDateValue.Content = scheduleManage.TaskList.Find(data
                => data.Priority == Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) + 1).EndDate.ToShortDateString();
            editID = scheduleManage.TaskList.Find(data
                => data.Priority == Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) + 1).IdNumber;
        }

        /// <summary>
        /// 詳細情報のタスク名欄からキーボードの入力フォーカスが外れた時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_Name_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            switch(editViewName)
            {
                case "PastTask":
                    if (selectRowOnPastTask == -1)
                    {
                        return;
                    }
                    pastTaskManage.EditTask(Text_Name.Text, float.Parse(Text_WorkVolume.Text), editID);
                    //ListBox_PastTask.Items[ListBox_PastTask.SelectedIndex] = Text_Name.Text;
                    ListBox_PastTask.Items[selectRowOnPastTask] = Text_Name.Text;
                    break;

                case "Schedule":
                    //　DataGridで現在選択中のセルの行インデックスを取得
                    if (selectRowOnSchedule == -1)
                    {
                        return;
                    }
                    scheduleManage.EditTask(Text_Name.Text, float.Parse(Text_WorkVolume.Text), int.Parse(Text_Priority.Text), editID);
                    ReScheduleData();
                    RefreshScheduleTask();
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// 詳細情報の作業量欄からキーボードの入力フォーカスが外れた時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_WorkVolume_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                // 不正な入力は例外発生で検知する
                float.Parse(Text_WorkVolume.Text);
            }
            catch(Exception)
            {
                // 変換できない値が入れられた場合は0に強制書き換え
                Text_WorkVolume.Text = "0";
            }
            switch (editViewName)
            {
                case "PastTask":
                    if (selectRowOnPastTask == -1)
                    {
                        return;
                    }
                    pastTaskManage.EditTask(Text_Name.Text, float.Parse(Text_WorkVolume.Text), editID);
                    break;

                case "Schedule":
                    //　DataGridで現在選択中のセルの行インデックスを取得
                    if (selectRowOnSchedule == -1)
                    {
                        return;
                    }
                    scheduleManage.EditTask(Text_Name.Text, float.Parse(Text_WorkVolume.Text), int.Parse(Text_Priority.Text), editID);
                    ReScheduleData();
                    RefreshScheduleTask();
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// 詳細情報の優先度欄からキーボードの入力フォーカスが外れた時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_Priority_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                // 不正な入力は例外発生で検知する
                int.Parse(Text_Priority.Text);
            }
            catch (Exception)
            {
                // 変換できない値が入れられた場合は0に強制書き換え
                Text_Priority.Text = "1";
            }
            switch (editViewName)
            {
                case "Schedule":
                    if (selectRowOnSchedule == -1)
                    {
                        return;
                    }

                    Text_Priority.Text = int.Parse(Text_Priority.Text) > scheduleManage.TaskList.Count ?
                        scheduleManage.TaskList.Count.ToString() : Text_Priority.Text;

                    Text_Priority.Text = int.Parse(Text_Priority.Text) < 1 ?
                        "1" : Text_Priority.Text;

                    scheduleManage.EditTask(Text_Name.Text, float.Parse(Text_WorkVolume.Text), int.Parse(Text_Priority.Text), editID);
                    ReScheduleData();
                    RefreshScheduleTask();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// スケジュール開始日を表記したボタンをクリックした時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_StartDate_Click(object sender, RoutedEventArgs e)
        {
            SelectStartSchedule();
            ReScheduleData();
            RefreshScheduleTask();
        }
        /// <summary>
        /// スケジュール開始日を設定するカレンダーが閉じられた時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_StartDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            SelectStartSchedule();
            ReScheduleData();
            RefreshScheduleTask();
        }
        /// <summary>
        /// スケジュールにカラムを作成する時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Schedule_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == headerColumnName)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 過去の作業実績一覧にタスクを追加し表示する。
        /// </summary>
        private void AddPastTask()
        {
            pastTaskManage.AddTask();
            RefreshPastTask();
        }
        /// <summary>
        /// 過去の作業実績一覧からタスクを削除し表示する。
        /// </summary>
        private void RemovePastTask()
        {
            if (editID == -1 || pastTaskManage.TaskList.Count == 0)
            {
                return;
            }
            pastTaskManage.DeleteTask(editID);
            RefreshPastTask();
            ListBox_PastTask.UnselectAll();
            editID = -1;
        }
        /// <summary>
        /// 過去の作業実績一覧の表示を作り直す
        /// </summary>
        private void RefreshPastTask()
        {
            ListBox_PastTask.Items.Clear();
            foreach (PastTask task in pastTaskManage.TaskList)
            {
                ListBox_PastTask.Items.Add(task.TaskName);
            }
        }

        /// <summary>
        /// スケジュールのデータから表示用データを作る
        /// </summary>
        private void ReScheduleData()
        {
            scheduleManage.RefreshSchedule();

            int drawStartCell = 1;
            DateTime currentStartDate = scheduleManage.StartDate;
            scheduleViewManage.ScheduleViewItemList.Clear();
            for (int i = 0; i < scheduleManage.TaskList.Count; i++)
            {
                ScheduleTask tempScheduleTask = scheduleManage.TaskList.Find(data => data.Priority == i+1);
                /*
                scheduleViewManage.ScheduleViewItemList.Add(new ScheduleViewItem {TaskName = tempScheduleTask.TaskName,
                                                                                  StartCell = drawStartCell,
                                                                                  EndCell = drawStartCell + (int)Math.Ceiling(tempScheduleTask.WorkVolume * 2) - 1
                });
                */
                scheduleViewManage.ScheduleViewItemList.Add(new ScheduleViewItem
                {
                    TaskName = tempScheduleTask.TaskName,
                    StartCell = drawStartCell,
                    EndCell = drawStartCell + (int)Math.Ceiling(tempScheduleTask.WorkVolume - 1)
                });
                drawStartCell = drawStartCell + (int)Math.Ceiling(tempScheduleTask.WorkVolume);
            }
        }

        /// <summary>
        /// スケジュールの表示を作り直す
        /// </summary>
        private void RefreshScheduleTask()
        {
            ViewTable.Rows.Clear();
            ViewTable.Columns.Clear();
            
            if (scheduleViewManage.ScheduleViewItemList.Count == 0)
            {
                return;
            }
            // 列の追加処理
            int MaxDate = scheduleViewManage.ScheduleViewItemList.Select(data => data.EndCell).Max();
            ViewTable.Columns.Add(headerColumnName);
            int dayOffsetHoliday = 0;
            for (int i = 0; i <= MaxDate; i++)
            {
                /*
                while (scheduleManage.IsHoliday(scheduleManage.StartDate.AddDays((int)(i / 2 + dayOffsetHoliday))) == true)
                {
                    dayOffsetHoliday++;
                }
                */
                
                while (scheduleManage.IsHoliday(scheduleManage.StartDate.AddDays((int)(i / SPRIT_DAY_TARM + dayOffsetHoliday))) == true)
                {
                    dayOffsetHoliday++;
                }


                // 列ヘッダにスラッシュ"/"が使えないため、書式設定で変更する
                /*
                ViewTable.Columns.Add(scheduleManage.StartDate.AddDays((int)(i / 2) + dayOffsetHoliday).ToString("MM月dd日\n") +
                        (i % 2 == 0 ? "AM" : "PM"));
                */
                string hourString = ((i % SPRIT_DAY_TARM) + 1).ToString();
                /*
                if (i % SPRIT_DAY_TARM == 0)
                {
                    ViewTable.Columns.Add(scheduleManage.StartDate.AddDays((int)(i / SPRIT_DAY_TARM) + dayOffsetHoliday).ToString("MM月dd日\n") + hourString);
                }
                else
                {
                    ViewTable.Columns.Add(hourString);
                }
                */
                ViewTable.Columns.Add(scheduleManage.StartDate.AddDays((int)(i / SPRIT_DAY_TARM) + dayOffsetHoliday).ToString("MM月dd日\n") + hourString);
            }

            // 行の追加処理
            foreach (ScheduleViewItem item in scheduleViewManage.ScheduleViewItemList)
            {
                DataRow tempRow = ViewTable.NewRow();
                tempRow[0]=item.TaskName;
                for (int i = 0; i < ViewTable.Columns.Count-1; i++)
                {
                    if (item.StartCell <= i + 1 && i + 1 <= item.EndCell)
                    {
                        tempRow[i + 1] = "■";
                    }
                }
                ViewTable.Rows.Add(tempRow);
            }
            this.DataContext = null;
            this.DataContext = ViewTable;          
        }
        /// <summary>
        /// 過去の作業実績一覧からスケジュールにタスクを加える
        /// </summary>
        private void AddScheduleFromPastTask()
        {
            if (ListBox_PastTask.SelectedIndex == -1)
            {
                return;
            }
            int priorityForAdd = 1;
            if (scheduleManage.TaskList.Count != 0)
            {
                priorityForAdd = scheduleManage.TaskList.Select(data => data.Priority).Max() + 1;
            }

            // 優先度は最も低い(=数字が大きい)状態で追加する
            scheduleManage.AddTask(pastTaskManage.TaskList[ListBox_PastTask.SelectedIndex].TaskName,
                                    pastTaskManage.TaskList[ListBox_PastTask.SelectedIndex].WorkVolume,
                                    priorityForAdd);
            ReScheduleData();
            RefreshScheduleTask();
        }
        /// <summary>
        /// スケジュール内タスクの優先度を１つ上げる
        /// </summary>
        private void UpPriority()
        {
            if (Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) == -1)
            {
                return;
            }

            Text_Priority.Text = (int.Parse(Text_Priority.Text) - 1).ToString();

            Text_Priority.Text = int.Parse(Text_Priority.Text) < 1 ?
                "1" : Text_Priority.Text;

            scheduleManage.EditTask(Text_Name.Text, float.Parse(Text_WorkVolume.Text), int.Parse(Text_Priority.Text), editID);
            ReScheduleData();
            RefreshScheduleTask();
        }
        /// <summary>
        /// スケジュール内タスクの優先度を１つ下げる
        /// </summary>
        private void downPriority()
        {
            if (Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) == -1)
            {
                return;
            }

            Text_Priority.Text = (int.Parse(Text_Priority.Text) + 1).ToString();

            Text_Priority.Text = int.Parse(Text_Priority.Text) > scheduleManage.TaskList.Count ?
                scheduleManage.TaskList.Count.ToString() : Text_Priority.Text;

            scheduleManage.EditTask(Text_Name.Text, float.Parse(Text_WorkVolume.Text), int.Parse(Text_Priority.Text), editID);
            ReScheduleData();
            RefreshScheduleTask();
        }
        /// <summary>
        /// スケジュール内のタスクを削除する
        /// </summary>
        private void DeleteScheduleTask()
        {
            if (Grid_Schedule.Items.IndexOf(Grid_Schedule.CurrentItem) == -1)
            {
                return;
            }
            scheduleManage.DeleteTask(editID);
            ReScheduleData();
            RefreshScheduleTask();
            editID = -1;
        }
        /// <summary>
        /// 過去の作業実績をファイルに保存する
        /// </summary>
        private void SavePastTask()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveDialog.Title = "作業実績の保存";
            saveDialog.Filter = "CSVファイル(*.csv)|*.csv";
            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveDialog.FileName, false, System.Text.Encoding.UTF8))
                    {
                        foreach (PastTask task in pastTaskManage.TaskList)
                        {
                            sw.WriteLine(task.IdNumber + "," + task.TaskName + "," + task.WorkVolume + "," + task.LastUpdate);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                }
            }
        }
        /// <summary>
        /// 過去の作業実績をファイルから読み込む
        /// </summary>
        private void LoadPastTask()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openDialog.Title = "作業実績の読み込み";
            openDialog.Filter = "CSVファイル(*.csv)|*.csv";
            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openDialog.FileName, Encoding.UTF8))
                    {
                        while (sr.EndOfStream == false)
                        {
                            string readPastTask = sr.ReadLine();
                            string[] pastTaskData = readPastTask.Split(',');
                            pastTaskManage.AddTask(pastTaskData[1], float.Parse(pastTaskData[2]));
                        }
                    }
                    RefreshPastTask();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                }
            }
        }
        private void LoadPastTaskDB()
        {
            try
            {
                pastTaskManage.DeleteAlltask();
                pastTaskManage.AddTaskFromDB();
                RefreshPastTask();
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }
        /// <summary>
        /// スケジュールをファイルに保存する
        /// </summary>
        private void SaveSchedule()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveDialog.Title = "スケジュールの保存";
            saveDialog.Filter = "CSVファイル(*.csv)|*.csv";
            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveDialog.FileName, false, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(scheduleManage.StartDate);
                        foreach(ScheduleTask task in scheduleManage.TaskList)
                        {
                            sw.WriteLine(task.IdNumber + "," + task.TaskName + "," + task.WorkVolume + "," +
                                         task.Priority + "," + task.StartDate + "," + task.EndDate);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                }
            }
        }
        /// <summary>
        /// スケジュールをファイルから読み込む
        /// </summary>
        private void LoadSchedule()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openDialog.Title = "スケジュールの読み込み";
            openDialog.Filter = "CSVファイル(*.csv)|*.csv";
            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openDialog.FileName, Encoding.UTF8))
                    {
                        // スケジュールは現在のスケジュールを消して作り直す
                        while (scheduleManage.TaskList.Count != 0)
                        {
                            scheduleManage.DeleteTask(scheduleManage.TaskList[0].IdNumber);
                        }
                        // 一行目はスケジュール全体の開始日
                        if (sr.EndOfStream == false)
                        {
                            string readSchedule = sr.ReadLine();
                            scheduleManage.StartDate = DateTime.Parse(readSchedule);
                        }
                        while (sr.EndOfStream == false)
                        {
                            string readSchedule = sr.ReadLine();
                            string[] scheduleData = readSchedule.Split(',');
                            scheduleManage.AddTaskForLoad(scheduleData[1], float.Parse(scheduleData[2]), int.Parse(scheduleData[3]));
                        }
                    }
                    ReScheduleData();
                    RefreshScheduleTask();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// スケジュール全体の開始日の表示を更新する
        /// </summary>
        private void SelectStartSchedule()
        {
            if (DatePicker_StartDate.SelectedDate is DateTime)
            {
                scheduleManage.StartDate = (DateTime)DatePicker_StartDate.SelectedDate;
            }
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
    }
}
