using System;
using System.Collections.Generic;

namespace RecurrenceRule
{
    public interface IRecurrenceRule
    {
        IEnumerable<DateTimeOffset> GetNextOccurrences(DateTimeOffset baseTime, DateTimeOffset endTime);
    }
}

