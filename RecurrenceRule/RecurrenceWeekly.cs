using System;
using System.Collections.Generic;
using System.Linq;

namespace RecurrenceRule
{
    public class RecurrenceWeekly : IRecurrenceRule
    {
        private int _interval;

        private DateTimeOffset _startTime;

        private DayOfWeek[] _weekDays;

        private int[] _timesInDay;

        public RecurrenceWeekly(DateTimeOffset startTime, int interval = 1, DayOfWeek[]? weekDays = null, int[]? timesInDay = null)
        {
            if (interval < 1) throw new ArgumentException($"{nameof(interval)} must >= 1", nameof(interval));
            if (timesInDay != null && !timesInDay.Any()) throw new ArgumentException($"{nameof(timesInDay)} must be null or length > 1");
            if (timesInDay != null && timesInDay.Any(time => time < 0 || time >= 24 * 60))
                throw new ArgumentException($"value in {nameof(timesInDay)} must >= 0 or < 24 * 60");

            _startTime = startTime;
            _interval = interval;
            _weekDays = weekDays ?? new[] { startTime.DayOfWeek };
            _timesInDay = timesInDay ?? new[] { startTime.Hour * 60 + startTime.Minute };
        }

        public IEnumerable<DateTimeOffset> GetOccurrences(DateTimeOffset? baseTime = null, DateTimeOffset? endTime = null)
        {
            baseTime ??= _startTime;
            endTime ??= DateTimeOffset.MaxValue;
            var skipTimes = Math.Floor(Math.Max((baseTime.Value - _startTime).TotalDays, 0) / (_interval * 7));
            var now = _startTime.Date.AddDays(-(int)_startTime.DayOfWeek).AddDays(skipTimes * _interval * 7);
            while (true)
            {
                foreach (var weekDayOffset in _weekDays)
                {
                    var day = now.AddDays((int)weekDayOffset);
                    foreach (var timeOffset in _timesInDay)
                    {
                        var time = day.AddMinutes(timeOffset);
                        if (time > endTime) yield break;
                        if (time >= _startTime && time >= baseTime) yield return time;
                    }
                }
                now += TimeSpan.FromDays(_interval * 7);
            }
        }
    }
}
