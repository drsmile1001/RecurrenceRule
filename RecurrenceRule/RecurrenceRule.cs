using System;
using System.Collections.Generic;

namespace RecurrenceRule
{
    public interface IRecurrenceRule
    {
        IEnumerable<DateTimeOffset> GetOccurrences(DateTimeOffset? baseTime, DateTimeOffset? endTime);
    }
}

