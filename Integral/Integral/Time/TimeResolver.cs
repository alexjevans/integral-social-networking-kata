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
            var unit = "minute";
            if(timespan.TotalMinutes != 1)
            {
                unit += "s";
            }
            return timespan.TotalMinutes + $" {unit} ago";
        }
    }
}
