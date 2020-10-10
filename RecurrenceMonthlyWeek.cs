using System;
using System.Collections.Generic;

namespace RecurrenceRule
{
    public class RecurrenceMonthlyWeek : IRecurrenceRule
    {
        private int _interval;

        private DateTimeOffset _startTime;

        private int[] _days;

        private int[] _timesInDay;

        public RecurrenceMonthlyWeek(int interval, DateTimeOffset startTime, int[] days = null, int[] timesInDay = null)
        {
            _interval = interval;
            _startTime = startTime;
            _days = days ?? new[] { startTime.Day };
            _timesInDay = timesInDay ?? new[] { startTime.Hour * 60 + startTime.Minute };
        }

        public IEnumerable<DateTimeOffset> GetNextOccurrences(DateTimeOffset baseTime, DateTimeOffset endTime)
        {
            var skipTimes = ((_startTime.Year - baseTime.Year) * 12 + _startTime.Month - baseTime.Month) / _interval;
            var now = _startTime.Date.AddDays(-_startTime.Day).AddMonths(skipTimes * _interval);
            while (true)
            {
                foreach (var dayOffset in _days)
                {
                    var day = now.AddDays((int)dayOffset);
                    foreach (var timeOffset in _timesInDay)
                    {
                        var time = day.AddMinutes(timeOffset);
                        if (time > endTime) yield break;
                        if (time > _startTime && time > baseTime) yield return time;
                    }
                }
                now = now.AddMonths(_interval);
            }
        }
    }
}