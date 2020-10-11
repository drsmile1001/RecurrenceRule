using System;
using System.Collections.Generic;
using System.Linq;

namespace RecurrenceRule
{
    public class RecurrenceMonthly : IRecurrenceRule
    {
        private int _interval;

        private DateTimeOffset _startTime;

        private int[] _days;

        private int[] _timesInDay;

        public RecurrenceMonthly(DateTimeOffset startTime, int interval, int[]? days = null, int[]? timesInDay = null)
        {
            if (interval < 1) throw new ArgumentException($"{nameof(interval)} must >= 1", nameof(interval));
            if (days != null && !days.Any()) throw new ArgumentException($"{nameof(days)} must be null or length >= 1", nameof(days));
            if (timesInDay != null && !timesInDay.Any()) throw new ArgumentException($"{nameof(timesInDay)} must be null or length > 1");
            if (days != null && days.Any(day => day < 1 || day > 28)) throw new ArgumentException($"value in {nameof(days)} must >= 1 or <= 28", nameof(days));
            if (timesInDay != null && timesInDay.Any(time => time < 0 || time >= 24 * 60))
                throw new ArgumentException($"value in {nameof(timesInDay)} must >= 0 or < 24 * 60");
            _interval = interval;
            _startTime = startTime;
            _days = days ?? new[] { startTime.Day };
            _timesInDay = timesInDay ?? new[] { startTime.Hour * 60 + startTime.Minute };
        }

        public IEnumerable<DateTimeOffset> GetOccurrences(DateTimeOffset? baseTime = null, DateTimeOffset? endTime = null)
        {
            baseTime ??= _startTime;
            endTime ??= DateTimeOffset.MaxValue;
            var skipTimes = Math.Max(((baseTime.Value.Year - _startTime.Year) * 12 + baseTime.Value.Month - _startTime.Month) / _interval, 0);
            var now = _startTime.Date.AddDays(-_startTime.Day + 1).AddMonths(skipTimes * _interval);
            while (true)
            {
                foreach (var dayOffset in _days)
                {
                    var day = now.AddDays((int)dayOffset - 1);
                    foreach (var timeOffset in _timesInDay)
                    {
                        var time = day.AddMinutes(timeOffset);
                        if (time > endTime) yield break;
                        if (time >= _startTime && time >= baseTime) yield return time;
                    }
                }
                now = now.AddMonths(_interval);
            }
        }
    }
}