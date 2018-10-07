using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeRecoder
{
    public class TimeCounter : ITimeCounter
    {
        private DateTime startTime = DateTime.Now;
        private DateTime stopTime = DateTime.Now;

        public void StartCount()
        {
            startTime = DateTime.Now;
        }
        public void StopCount()
        {
            stopTime = DateTime.Now;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetCountTime()
        {
            TimeSpan currentTime = DateTime.Now - startTime;
            return currentTime;
        }
        public string GetCountTime(string format)
        {
            TimeSpan currentTime = GetCountTime();
            return currentTime.ToString(format);
        }
    }
}
