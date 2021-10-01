using System;
using System.Collections.Generic;
using System.Text;

namespace Integral.Time
{
    public class TimeResolver : ITimeResolver
    {
        public string GetMinutesSinceStart(DateTime start, DateTime now)
        {
            return ((int) (now - start).TotalMinutes).ToString();
        }
    }
}
