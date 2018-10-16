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

namespace WorkTimeRecoder
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // TimeCounter timeCounter = new TimeCounter();

        public MainWindow()
        {
            InitializeComponent();
        }

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
            TimerListPanel.Children.Add(addTimerPanel);
            TimerListPanel.RegisterName(addTimerPanel.Name, addTimerPanel);
        }

        public void DeleteTimer(string timerName)
        {
            TimerPanel delTimerPanel = (TimerPanel)TimerListPanel.FindName(timerName);
            if(delTimerPanel == null)
            {
                return;
            }
            TimerListPanel.Children.Remove(delTimerPanel);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
