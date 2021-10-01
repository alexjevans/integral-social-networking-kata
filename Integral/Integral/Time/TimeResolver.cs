using System;
using System.Collections.Generic;
using System.Text;

namespace Integral.Time
{
    public class TimeResolver : ITimeResolver
    {
        public void SetStartTime(DateTime start)
        {

        }

        public int GetMinutesSinceStart()
        {
            return 1;
        }

        public void SetCurrentTime(DateTime now)
        {
            
        }
    }
}
