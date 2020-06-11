using System;
using System.Collections.Generic;
using System.Text;

namespace BusStop.Business.Helpers
{
    public interface IDateTimeNow
    {
        DateTime Now { get; }
    }

    public class DateTimeNow : IDateTimeNow
    {
        public DateTime Now => DateTime.Now;
    }
}
