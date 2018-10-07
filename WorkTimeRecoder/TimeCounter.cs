using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeRecoder
{
    interface ITimeCounter
    {
        void StartCount();

        void StopCount();

        TimeSpan GetCountTime();
    }
}
