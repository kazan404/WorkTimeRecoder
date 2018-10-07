using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkTimeRecoder;

namespace WorktimeRecoderTest
{
    [TestClass]
    public class Test_TimeCounter
    {
        [TestMethod]
        public void TestMethod1()
        {
            TimeCounter timeCounter = new TimeCounter();
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            string checkStr = timeCounter.GetCountTime(@"hh\:mm\:ss");

            Console.WriteLine(checkStr);
        }

        [TestMethod]
        public void TestMethod2()
        {
            TimeCounter timeCounter = new TimeCounter();
            timeCounter.StartCount();

            System.Threading.Thread.Sleep(3000);

            TimeSpan checkTimeSpan = timeCounter.GetCountTime();

            Console.WriteLine(checkTimeSpan);
        }
    }
}
