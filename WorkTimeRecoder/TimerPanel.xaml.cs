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
    public delegate void DeleteTimer(string timerName);

    /// <summary>
    /// TimerPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class TimerPanel : UserControl
    {
        private DeleteTimer deleteTimerFunc = null;
        public DeleteTimer DeleteTimerFunc { get => deleteTimerFunc; set => deleteTimerFunc = value; }

        private BaloondSetter baloonsetter;
        public BaloondSetter Baloonsetter { get => baloonsetter; set => baloonsetter = value; }

        private TimeCounter timeCounter;

        public TimerPanel()
        {
            InitializeComponent();
            timeCounter = new TimeCounter(TimerTickFunc);
            TimeText.Text = "00:00:00";         
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            deleteTimerFunc(this.Name);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (timeCounter.IsCounting == true)
            {
                timeCounter.StopCount();
                ResetButton.IsEnabled = true;
                CloseButton.IsEnabled = true;
                StartButton.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                timeCounter.StartCount();
                ResetButton.IsEnabled = false;
                CloseButton.IsEnabled = false;
                StartButton.Background = new SolidColorBrush(Colors.Red);
            }
        }
        
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timeCounter.ResetCount();
            TimeText.Text = "00:00:00";
        }
        
        private void TimerTickFunc(Object sender, EventArgs e)
        {
            TimeText.Text = timeCounter.GetCountTime(@"hh\:mm\:ss");
            TimeSpan timeSpan = timeCounter.GetCountTime();
            if ((int)(timeSpan.TotalSeconds) % 3600 == 0)
            {
                Baloonsetter(IssueNameText.Text, TimeText.Text);
            }
        }
    }
}
