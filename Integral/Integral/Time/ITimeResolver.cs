using System;
using System.Collections.Generic;
using System.Text;

namespace Integral.Time
{
    public interface ITimeResolver
    {
        string GetMinutesSinceStart(DateTime start, DateTime now);
    }
}
