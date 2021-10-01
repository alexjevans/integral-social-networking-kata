using System;
using System.Collections.Generic;
using System.Text;

namespace Integral.Time
{
    public class TimeResolver : ITimeResolver
    {
        private DateTime start;

        public void SetStartTime(DateTime start)
        {
            this.start = start;
        }

        public int GetMinutesSinceStart(DateTime now)
        {
            return 1;
        }
    }
}
