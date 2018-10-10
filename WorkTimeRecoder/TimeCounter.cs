using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WorkTimeRecoder
{
    public delegate void TimerTick(Object sender, EventArgs e);

    public class TimeCounter : ITimeCounter
    {
        private DateTime startTime = DateTime.MinValue; // MinValueを、初期化かリセット後とみなす。
        private DateTime stopTime = DateTime.MinValue;

        private DispatcherTimer timer;
        public DispatcherTimer Timer { get => timer; }

        private bool isCounting = false;
        public bool IsCounting { get => isCounting; }

        public TimeCounter()
        {

        }
        public TimeCounter(TimerTick timerTick)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timerTick);
        }

        public void StartCount()
        {
            if (isCounting == false)
            {
                isCounting = true;
                if (startTime == DateTime.MinValue)
                {
                    startTime = DateTime.Now;
                }
            }
            timer.Start();
        }
        public void StopCount()
        {
            if (isCounting == true)
            {
                isCounting = false;
                stopTime = DateTime.Now;
            }
            timer.Stop();
        }
        public void ResetCount()
        {
            startTime = DateTime.MinValue;
        }

        public TimeSpan GetCountTime()
        {
            TimeSpan currentTime = new TimeSpan();
            if(isCounting == true)
            {
                currentTime = DateTime.Now - startTime;
            }
            else
            {
                currentTime = stopTime - startTime;
            }
            return currentTime;
        }

        public string GetCountTime(string format)
        {
            TimeSpan currentTime = GetCountTime();
            return currentTime.ToString(format);
        }
    }
}
