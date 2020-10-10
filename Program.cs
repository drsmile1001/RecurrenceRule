using System;

namespace RecurrenceRule
{
    class Program
    {
        static void Main(string[] args)
        {
            IRecurrenceRule RecurrenceRule;
            
            // RecurrenceRule = new RecurrenceDaily(
            //     2,
            //     new DateTimeOffset(2020,10,10,0,0,0,TimeSpan.FromHours(8)),
            //     new[]{8*60,12*60,16*60}
            //     );
            // RecurrenceRule = new RecurrenceWeekly(
            //     2,
            //     new DateTimeOffset(2020,10,10,0,0,0,TimeSpan.FromHours(8)),
            //     new[]{DayOfWeek.Monday,DayOfWeek.Wednesday,DayOfWeek.Saturday},
            //     new[]{8*60,12*60,16*60}
            // );

            RecurrenceRule = new RecurrenceMonthly(
                2,
                new DateTimeOffset(2020,10,10,0,0,0,TimeSpan.FromHours(8)),
                new[]{1,9,17,25},
                new[]{8*60,12*60,16*60}
            );

            foreach (var item in RecurrenceRule.GetNextOccurrences(
                new DateTimeOffset(2020,10,17,8,0,0,TimeSpan.FromHours(8)),
                new DateTimeOffset(2020,12,20,8,0,0,TimeSpan.FromHours(8))
                ))
            {
                Console.WriteLine(item.ToString("o"));
            }
        }
    }
}
