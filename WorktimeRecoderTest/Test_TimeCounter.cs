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
        public void TestMethod1()
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
        public void TestMethod2()
        {
            TimeCounter timeCounter = new TimeCounter(TimerTickFunc);
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            TimeSpan checkTimeSpan = timeCounter.GetCountTime();

            Console.WriteLine(checkTimeSpan);
        }


        [TestMethod]
        public void TestMethod3()
        {
            TimeCounter timeCounter = new TimeCounter(TimerTickFunc);
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            timeCounter.StopCount();
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(4000);

            timeCounter.StopCount();

            TimeSpan checkTimeSpan = timeCounter.GetCountTime();

            Console.WriteLine(checkTimeSpan);
        }
    }
}
