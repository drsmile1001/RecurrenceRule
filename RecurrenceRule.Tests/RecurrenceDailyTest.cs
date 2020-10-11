using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace RecurrenceRule.Tests
{
    public class RecurrenceDailyTest
    {
        [Fact]
        public void 每1日_無獲取開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences().Take(6).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1日_無獲取開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(endTime: new DateTimeOffset(2020,10,11,10,0,0,TimeSpan.FromHours(8))).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,8,0,0,TimeSpan.FromHours(8)),
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1日_獲取時間早於排程開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020,10,8,0,0,0,TimeSpan.FromHours(8)))
                .Take(6)
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1日_獲取時間早於排程開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020,10,8,0,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,10,0,0,TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1日_獲取時間晚於排程開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)))
                .Take(6)
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1日_獲取時間晚於排程開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,10,0,0,TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2日_無獲取開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences().Take(6).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2日_無獲取開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(endTime: new DateTimeOffset(2020,10,11,10,0,0,TimeSpan.FromHours(8))).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2日_獲取時間早於排程開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020,10,8,0,0,0,TimeSpan.FromHours(8)))
                .Take(6)
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2日_獲取時間早於排程開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020,10,8,0,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,11,10,0,0,TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2日_獲取時間晚於排程開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)))
                .Take(6)
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,14,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,14,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,14,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2日_獲取時間晚於排程開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceDaily(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2, new[] { 8 * 60, 12 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,16,10,0,0,TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,14,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,14,12,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,14,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,16,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }
    }
}
