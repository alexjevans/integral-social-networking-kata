using System;
using System.Collections.Generic;
using System.Text;

namespace Integral.Time
{
    public class TimeResolver
    {
        public static string GetFormatedTimeSinceStart(DateTime start, DateTime now)
        {
            var timespan = now - start;
            var unit = "minute";
            var totalUnits = timespan.TotalMinutes;
            if(totalUnits == 0)
            {
                return string.Empty;
            }
            if (timespan.TotalSeconds < 60)
            {
                totalUnits = timespan.TotalSeconds;
                unit = "second";
            }
            if(totalUnits > 1)
            {
                unit += "s";
            }
            return totalUnits + $" {unit} ago";
        }
    }
}
