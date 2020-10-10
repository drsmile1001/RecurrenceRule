using System;
using System.Collections.Generic;

namespace RecurrenceRule
{
    public class RecurrenceWeekly : IRecurrenceRule
    {
        private int _interval;

        private DateTimeOffset _startTime;

        private DayOfWeek[] _weekDays;

        private int[] _timesInDay;

        public RecurrenceWeekly(int interval, DateTimeOffset startTime, DayOfWeek[] weekDays = null, int[] timesInDay = null)
        {
            _interval = interval;
            _startTime = startTime;
            _weekDays = weekDays ?? new[] { startTime.DayOfWeek };
            _timesInDay = timesInDay ?? new[] { startTime.Hour * 60 + startTime.Minute };
        }

        public IEnumerable<DateTimeOffset> GetNextOccurrences(DateTimeOffset baseTime, DateTimeOffset endTime)
        {
            var skipTimes = Math.Floor((baseTime - _startTime).TotalDays / (_interval * 7));
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
                        if (time > _startTime && time > baseTime) yield return time;
                    }
                }
                now += TimeSpan.FromDays(_interval * 7);
            }
        }
    }
}
