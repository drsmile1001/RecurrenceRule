using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace RecurrenceRule.Tests
{
    public class RecurrenceWeeklyTest
    {
        [Fact]
        public void 每1週_無獲取開始時間_無截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences().Take(10).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1週_無獲取開始時間_有截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(endTime: new DateTimeOffset(2020, 10, 17, 16, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1週_獲取時間早於排程開始時間_無截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(new DateTimeOffset(2020, 9, 10, 8, 0, 0, TimeSpan.FromHours(8)))
                .Take(10).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1週_獲取時間早於排程開始時間_有截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(
                    new DateTimeOffset(2020, 9, 10, 8, 0, 0, TimeSpan.FromHours(8)),
                    new DateTimeOffset(2020, 10, 17, 16, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1週_獲取時間晚於排程開始時間_無截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(new DateTimeOffset(2020, 10, 10, 16, 0, 0, TimeSpan.FromHours(8)))
                .Take(9).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1週_獲取時間晚於排程開始時間_有截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(
                    new DateTimeOffset(2020, 10, 10, 8, 0, 0, TimeSpan.FromHours(8)),
                    new DateTimeOffset(2020, 10, 17, 16, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,12,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,17,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2週_無獲取開始時間_無截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences().Take(10).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2週_無獲取開始時間_有截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(endTime: new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2週_獲取時間早於排程開始時間_無截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(new DateTimeOffset(2020,9,10,8,0,0,TimeSpan.FromHours(8)))
                .Take(10).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2週_獲取時間早於排程開始時間_有截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(
                    new DateTimeOffset(2020,9,10,8,0,0,TimeSpan.FromHours(8)),
                    new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2週_獲取時間晚於排程開始時間_無截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)))
                .Take(9).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2週_獲取時間晚於排程開始時間_有截止時間()
        {

            var recurrenceRule = new RecurrenceWeekly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { DayOfWeek.Monday, DayOfWeek.Saturday }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(
                    new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                    new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,19,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,24,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,2,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,7,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }
    }
}
