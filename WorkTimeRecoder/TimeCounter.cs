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
        private TimeSpan stackSpan = TimeSpan.Zero;

        private DispatcherTimer timer;
        public DispatcherTimer Timer { get => timer; }

        private bool isCounting = false;
        public bool IsCounting { get => isCounting; }

        public TimeCounter()
        {

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="timerTick">タイマーが1秒ごとに実行するメソッド</param>
        public TimeCounter(TimerTick timerTick)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timerTick);
        }

        /// <summary>
        /// タイマーを開始する
        /// </summary>
        public void StartCount()
        {
            startTime = DateTime.Now;
            timer.Start();
            isCounting = timer.IsEnabled;
        }
        /// <summary>
        /// タイマーを止める
        /// </summary>
        public void StopCount()
        {
            stopTime = DateTime.Now;
            stackSpan = stackSpan.Add((stopTime - startTime));
            timer.Stop();
            isCounting = timer.IsEnabled;
        }
        /// <summary>
        /// タイマーをリセットする
        /// </summary>
        public void ResetCount()
        {
            startTime = DateTime.MinValue;
            stackSpan = TimeSpan.Zero;
        }
        /// <summary>
        /// 現在のタイマー値を取得する
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetCountTime()
        {
            TimeSpan currentSpan = TimeSpan.Zero;
            if (isCounting == true)
            {
                currentSpan = (DateTime.Now - startTime) + stackSpan;
            }
            else
            {
                currentSpan = stackSpan;
            }
            return currentSpan;
        }
        /// <summary>
        /// 現在のタイマー値を取得する
        /// </summary>
        /// <param name="format">取得するときのTimeSpan型のフォーマット</param>
        /// <returns></returns>
        public string GetCountTime(string format)
        {
            TimeSpan currentTime = GetCountTime();
            return currentTime.ToString(format);
        }
    }
}
