using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkTimeRecoder;

namespace WorktimeRecoderTest
{
    [TestClass]
    public class Test_TimeCounter
    {
        private void TimerTickFunc(Object sender, EventArgs e)
        {
        }

        [TestMethod]
        public void StartCount_GetCountTime_タイマー開始と取得が想定通りにできる()
        {
            DateTime startTime = DateTime.Now;

            TimeCounter timeCounter = new TimeCounter(TimerTickFunc);
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            DateTime endTime = DateTime.Now;

            string checkStr = timeCounter.GetCountTime(@"hh\:mm\:ss");

            string check = (endTime - startTime).ToString(@"hh\:mm\:ss");

            Console.WriteLine(checkStr);

            Assert.AreEqual(checkStr, check);
        }

        [TestMethod]
        public void StopCount_タイマーの停止が想定通り動いている()
        {
            TimeCounter timeCounter = new TimeCounter(TimerTickFunc);
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            timeCounter.StopCount();

            System.Threading.Thread.Sleep(3000);

            timeCounter.StartCount();

            System.Threading.Thread.Sleep(4000);

            timeCounter.StopCount();

            string checkStr = timeCounter.GetCountTime(@"hh\:mm\:ss");

            string check = "00:00:07";

            Console.WriteLine(checkStr);

            Assert.AreEqual(checkStr, check);
        }

        [TestMethod]
        public void ResetCount_タイマーリセットが想定通り動いている()
        {
            TimeCounter timeCounter = new TimeCounter(TimerTickFunc);
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            timeCounter.StopCount();

            timeCounter.ResetCount();

            string checkStr = timeCounter.GetCountTime(@"hh\:mm\:ss");

            string check = "00:00:00";

            Assert.AreEqual(checkStr, check);
        }

        [TestMethod]
        public void ResetCount_StartCount_タイマーリセット後のカウントが想定通り動いている()
        {
            TimeCounter timeCounter = new TimeCounter(TimerTickFunc);
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            timeCounter.StopCount();

            timeCounter.ResetCount();

            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            string checkStr = timeCounter.GetCountTime(@"hh\:mm\:ss");

            string check = "00:00:03";

            Assert.AreEqual(checkStr, check);
        }
    }
}
