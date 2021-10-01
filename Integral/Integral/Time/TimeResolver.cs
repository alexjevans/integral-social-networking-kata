using System;
using System.Collections.Generic;
using System.Text;

namespace Integral.Time
{
    public class TimeResolver : ITimeResolver
    {
        public string GetMinutesSinceStart(DateTime start, DateTime now)
        {
            var timespan = now - start;
            return timespan.TotalMinutes + " minute ago";
        }
    }
}
