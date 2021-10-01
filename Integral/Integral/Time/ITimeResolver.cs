using System;
using System.Collections.Generic;
using System.Text;

namespace Integral.Time
{
    public interface ITimeResolver
    {
        void SetStartTime(DateTime start);

        int GetMinutesSinceStart(DateTime now);
    }
}
