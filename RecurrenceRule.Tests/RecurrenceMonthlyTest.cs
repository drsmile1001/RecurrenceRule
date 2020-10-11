using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace RecurrenceRule.Tests
{
    public class RecurrenceMonthlyTest
    {
        [Fact]
        public void 每1月_無獲取開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences().Take(8).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,1,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,1,10,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1月_無獲取開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(endTime: new DateTimeOffset(2020, 12, 10, 8, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1月_獲取時間早於排程開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020, 8, 10, 8, 0, 0, TimeSpan.FromHours(8))
            ).Take(8).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,1,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,1,10,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1月_獲取時間早於排程開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
            , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(
                new DateTimeOffset(2020, 8, 10, 8, 0, 0, TimeSpan.FromHours(8)),
                new DateTimeOffset(2020, 12, 10, 8, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1月_獲取時間晚於排程開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
                , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020, 10, 10, 16, 0, 0, TimeSpan.FromHours(8))
            ).Take(7).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,1,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,1,10,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每1月_獲取時間晚於排程開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 1
                , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(
                new DateTimeOffset(2020, 10, 10, 16, 0, 0, TimeSpan.FromHours(8)),
                new DateTimeOffset(2020, 12, 10, 8, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,11,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2月_無獲取開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences().Take(8).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2月_無獲取開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(endTime: new DateTimeOffset(2021, 4, 10, 8, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2月_獲取時間早於排程開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020, 8, 10, 8, 0, 0, TimeSpan.FromHours(8))
            ).Take(8).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2月_獲取時間早於排程開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
            , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(
                new DateTimeOffset(2020, 8, 10, 8, 0, 0, TimeSpan.FromHours(8)),
                new DateTimeOffset(2021, 4, 10, 8, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,8,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2月_獲取時間晚於排程開始時間_無截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
                , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule.GetOccurrences(
                new DateTimeOffset(2020, 10, 10, 16, 0, 0, TimeSpan.FromHours(8))
            ).Take(7).ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,16,0,0,TimeSpan.FromHours(8))
            };
            actual.Should().BeEquivalentTo(expectations);
        }

        [Fact]
        public void 每2月_獲取時間晚於排程開始時間_有截止時間()
        {
            var recurrenceRule = new RecurrenceMonthly(new DateTimeOffset(2020, 10, 10, 0, 0, 0, TimeSpan.FromHours(8)), 2
                , new[] { 10 }, new[] { 8 * 60, 16 * 60 });
            var actual = recurrenceRule
                .GetOccurrences(
                new DateTimeOffset(2020, 10, 10, 16, 0, 0, TimeSpan.FromHours(8)),
                new DateTimeOffset(2021, 4, 10, 8, 0, 0, TimeSpan.FromHours(8)))
                .ToArray();
            var expectations = new[]
            {
                new DateTimeOffset(2020,10,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,2,10,16,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2021,4,10,8,0,0,TimeSpan.FromHours(8)),
            };
            actual.Should().BeEquivalentTo(expectations);
        }
    }
}
