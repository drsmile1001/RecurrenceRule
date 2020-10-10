using System;
using System.Collections.Generic;
namespace RecurrenceRule
{
    public class RecurrenceDaily : IRecurrenceRule
    {
        private int _interval;

        private DateTimeOffset _startTime;

        private int[] _timesInDay;

        public RecurrenceDaily(int interval, DateTimeOffset startTime, int[] timesInDay = null)
        {
            _interval = interval;
            _startTime = startTime;
            _timesInDay = timesInDay ?? new[] { 0 };
        }

        public IEnumerable<DateTimeOffset> GetNextOccurrences(DateTimeOffset baseTime, DateTimeOffset endTime)
        {
            var skipTimes = Math.Floor((baseTime - _startTime).TotalDays / _interval) - 1;
            var now = _startTime.AddDays(skipTimes * _interval);
            while (true)
            {
                now += TimeSpan.FromDays(_interval);
                foreach (var offset in _timesInDay)
                {
                    var time = now.AddMinutes(offset);
                    if (time > endTime) yield break;
                    if (time > _startTime && time > baseTime) yield return time;
                }
            }
        }
    }
}
