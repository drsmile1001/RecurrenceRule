using System;
using System.Collections.Generic;
using System.Linq;

namespace RecurrenceRule
{
    public class RecurrenceDaily : IRecurrenceRule
    {
        private int _interval;

        private DateTimeOffset _startTime;

        private int[] _timesInDay;

        public RecurrenceDaily(DateTimeOffset startTime, int interval = 1, int[]? timesInDay = null)
        {
            if (interval < 1) throw new ArgumentException($"{nameof(interval)} must >= 1", nameof(interval));
            if (timesInDay != null && !timesInDay.Any()) throw new ArgumentException($"{nameof(timesInDay)} must be null or length > 1");
            if (timesInDay != null && timesInDay.Any(time => time < 0 || time >= 24 * 60))
                throw new ArgumentException($"value in {nameof(timesInDay)} must >= 0 or < 24 * 60");

            _startTime = startTime;
            _interval = interval;
            _timesInDay = timesInDay ?? new[] { startTime.Hour * 60 + startTime.Minute };
        }

        public IEnumerable<DateTimeOffset> GetOccurrences(DateTimeOffset? baseTime = null, DateTimeOffset? endTime = null)
        {
            if (baseTime.HasValue && endTime.HasValue && endTime < baseTime)
                throw new ArgumentException($"{nameof(endTime)} must >= baseTime");
            if (endTime < _startTime) yield break;
            baseTime ??= _startTime;
            endTime ??= DateTimeOffset.MaxValue;
            var skipTimes = Math.Floor(Math.Max((baseTime.Value.Date - _startTime.Date).TotalDays, 0) / _interval);
            var now = _startTime.Date.AddDays(skipTimes * _interval);
            while (true)
            {
                foreach (var offset in _timesInDay)
                {
                    var time = now.AddMinutes(offset);
                    if (time > endTime) yield break;
                    if (time >= _startTime && time >= baseTime) yield return time;
                }
                now += TimeSpan.FromDays(_interval);
            }
        }
    }
}
