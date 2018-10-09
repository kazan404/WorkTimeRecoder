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
                StartButton.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                TimeText.Text = "00:00:00";
                timeCounter.StartCount();
                StartButton.Background = new SolidColorBrush(Colors.Red);
            }
        }
        /*
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {

        }
        */
        private void TimerTickFunc(Object sender, EventArgs e)
        {
            TimeText.Text = timeCounter.GetCountTime(@"hh\:mm\:ss");
        }
    }
}
